﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;

namespace Packt.Shared
{
    public static class Protector
    {
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");
        private static readonly int iterations = 2000;

        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);

            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cryptoText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);

            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }
            return Encoding.Unicode.GetString(plainBytes);
        }

        private static Dictionary<string, User> Users = new Dictionary<string, User>();
        public static User Register(string username, string password, string[] roles = null)
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);

            var saltedHashedPassword = SaltAndHashPassword(password, saltText);

            var user = new User
            {
                Name = username,
                Salt = saltText,
                SaltedHashedPassword = saltedHashedPassword,
                Roles = roles
            };

            Users.Add(user.Name, user);

            return user;
        }

        public static bool CheckPassword(string username, string password)
        {
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            var user = Users[username];

            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);

            return (saltedhashedPassword == user.SaltedHashedPassword);
        }

        private static string SaltAndHashPassword(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }

        public static string PublicKey;

        public static string ToXmlStringExt(this RSA rsa, bool includePrivateParameters)
        {
            var p = rsa.ExportParameters(includePrivateParameters);
            XElement xml;
            if (includePrivateParameters)
            {
                xml = new XElement("RSAKeyValue",
                    new XElement("Modulus", Convert.ToBase64String(p.Modulus)),
                    new XElement("Exponent", Convert.ToBase64String(p.Exponent)),
                    new XElement("P", Convert.ToBase64String(p.P)),
                    new XElement("Q", Convert.ToBase64String(p.Q)),
                    new XElement("DP", Convert.ToBase64String(p.DP)),
                    new XElement("DQ", Convert.ToBase64String(p.DQ)),
                    new XElement("InverseQ", Convert.ToBase64String(p.InverseQ)));
            }
            else
            {
                xml = new XElement("RSAKeyValue",
                        new XElement("Modulus", Convert.ToBase64String(p.Modulus)),
                        new XElement("Exponent", Convert.ToBase64String(p.Exponent)));
            }
            return xml?.ToString();
        }

        public static void FromXMlStringExt(this RSA rsa, string parameterAsXml)
        {
            var xml = XDocument.Parse(parameterAsXml);
            var root = xml.Element("RSAKeyValue");
            var p = new RSAParameters()
            {
                Modulus = Convert.FromBase64String(root.Element("Modulus").Value),
                Exponent = Convert.FromBase64String(root.Element("Exponent").Value)
            };

            if (root.Element("p") != null)
            {
                p.P = Convert.FromBase64String(root.Element("P").Value);
                p.Q = Convert.FromBase64String(root.Element("Q").Value);
                p.DP = Convert.FromBase64String(root.Element("DP").Value);
                p.DQ = Convert.FromBase64String(root.Element("DQ").Value);
                p.InverseQ = Convert.FromBase64String(root.Element("InverseQ").Value);
            }
            rsa.ImportParameters(p);
        }

        public static string GenerateSignature(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            var sha = SHA256.Create();
            var hashedData = sha.ComputeHash(dataBytes);
            var rsa = RSA.Create();
            PublicKey = rsa.ToXmlStringExt(false);

            return Convert.ToBase64String(rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }

        public static bool ValidateSignature(string data, string signature)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            var sha = SHA256.Create();
            var hashedData = sha.ComputeHash(dataBytes);
            byte[] signatureBytes = Convert.FromBase64String(signature);
            var rsa = RSA.Create();
            rsa.FromXMlStringExt(PublicKey);

            return rsa.VerifyHash(hashedData, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public static void LogIn(string username, string password)
        {
            if (CheckPassword(username, password))
            {
                var identity = new GenericIdentity(username, "PackAuth");
                var principal = new GenericPrincipal(identity, Users[username].Roles);
                System.Threading.Thread.CurrentPrincipal = principal;
            }
        }
    }
}
