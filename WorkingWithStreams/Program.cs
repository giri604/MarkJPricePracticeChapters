using System;
using System.IO;
using System.Xml;
using System.IO.Compression;
using static System.Console;
using static System.Environment;
using static System.IO.Path;

namespace WorkingWithStreams
{
    class Program
    {
        static string[] callsigns = new string[] {
                                    "Husker", "Starbuck", "Apollo", "Boomer",
                                    "Bulldog", "Athena", "Helo", "Racetrack"};
        static void Main(string[] args)
        {
            //WorkWithText();
            //WorkWithXml();
            WorkWithCompression();
            WorkWithCompression(useBrotli: false);
        }

        static void WorkWithText()
        {
            string textFile = Combine(CurrentDirectory, "stream.txt");
            StreamWriter text = File.CreateText(textFile);

            foreach (string item in callsigns)
            {
                text.WriteLine(item);
            }
            text.Close();

            WriteLine("{0} contains {1:N0} bytes.",
                        arg0: textFile,
                        arg1: new FileInfo(textFile).Length);

            WriteLine(File.ReadAllText(textFile));
        }

        static void WorkWithXml()
        {
            FileStream xmlFileStream = null;
            XmlWriter xml = null;
            try
            {
                string xmlFile = Combine(CurrentDirectory, "stream.xml");
                xmlFileStream = File.Create(xmlFile);
                xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });


                xml.WriteStartDocument();
                xml.WriteStartElement("callsigns");

                foreach (string item in callsigns)
                {
                    xml.WriteElementString("callsign", item);
                }
                xml.WriteEndElement();

                xml.Close();
                xmlFileStream.Close();

                WriteLine("{0} contains {1:N0} bytes.",
                            arg0: xmlFile,
                            arg1: new FileInfo(xmlFile).Length);

                WriteLine(File.ReadAllText(xmlFile));
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.GetType()} say {ex.Message}");
            }
            finally
            {
                if(xml != null)
                {
                    xml.Dispose();
                    WriteLine("The xml writer's unmanaged resources have been disposed.");
                }
                if(xmlFileStream != null)
                {
                    xmlFileStream.Dispose();
                    WriteLine("The file stream's unmanaged resources have been disposed.");
                }
            }

          

           

        }

        static void WorkWithCompression(bool useBrotli = true)
        {
            string fileExt = useBrotli ? "brotli" : "gzip";
            //string gzipFilePath = Combine(CurrentDirectory, "streams.gzip");
            string gzipFilePath = Combine(CurrentDirectory, $"streams.{fileExt}");

            FileStream gzipFile = File.Create(gzipFilePath);

            Stream compressor;
            
            if(useBrotli)
            {
                compressor = new BrotliStream(gzipFile, CompressionMode.Compress);
            }
            else
            {
                compressor = new GZipStream(gzipFile, CompressionMode.Compress);
            }

            using (compressor)
            {
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor))
                {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");

                    foreach(string item in callsigns)
                    {
                        xmlGzip.WriteElementString("callsign", item);
                    }
                }
            }

            WriteLine("{0} contains {1:N0} bytes.",
                        arg0: gzipFilePath,
                        arg1: new FileInfo(gzipFilePath).Length);

            WriteLine("The compressed contents:");
            WriteLine(File.ReadAllText(gzipFilePath));

            WriteLine("Reading the compressed XML file:");
            gzipFile = File.Open(gzipFilePath, FileMode.Open);

            Stream decompressor;
            if (useBrotli)
            {
                decompressor = new BrotliStream(gzipFile, CompressionMode.Decompress);
            }
            else
            {
                decompressor = new GZipStream(gzipFile, CompressionMode.Decompress);
            }

            using (decompressor)
            {
                using (XmlReader reader = XmlReader.Create(decompressor))
                {
                    while(reader.Read())
                    {
                        if((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                        {
                            reader.Read();
                            WriteLine($"{ reader.Value}");
                        }
                    }
                }
            }
        }
    }
}
