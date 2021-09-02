using System;
using System.IO;
using System.Xml;
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
            WorkWithXml();
        }

        static void WorkWithText()
        {
            string textFile = Combine(CurrentDirectory, "stream.txt");
            StreamWriter text = File.CreateText(textFile);
            
            foreach(string item in callsigns)
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
            string xmlFile = Combine(CurrentDirectory, "stream.xml");
            FileStream xmlFileStream = File.Create(xmlFile);

            XmlWriter xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

            xml.WriteStartDocument();
            xml.WriteStartElement("callsigns");

            foreach(string item in callsigns)
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
    }
}
