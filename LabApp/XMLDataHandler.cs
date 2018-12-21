using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LabApp
{
    class XMLDataHandler
    {
        private XDocument xmlDoc;

        private string path = @"C:\temp\data.xml";
        public XMLDataHandler()
        {
            try
            {
                xmlDoc = XDocument.Load(path);
            }
            catch
            {
                xmlDoc = new XDocument();
            }
        }

        public void XMLWriteErrorCode(string _errorCode)
        {
            if (!System.IO.File.Exists(path)) //Decides if the player has a xml file already
            {
                XDeclaration _obj = new XDeclaration("1.0", "utf-8", "");
                XNamespace ErrorCodeList = "ErrorCodeList";
                XElement file = new XElement("Root", new XElement("ErrorCodeList"));

                file.Save(path);
            }

            //Get data from existing
            xmlDoc = XDocument.Load(path);
            xmlDoc.Root.Element("ErrorCodeList").Add(
                new XElement("Error", 
                new XAttribute("Code", _errorCode),
                new XAttribute("Type", "ZAP"),
                new XElement("Description", "Rebote"), 
                new XElement("Procedure", "No action")));
            xmlDoc.Save(path);

        }

        public void  XmlReaderErrorCode(string _errorCode)
        {
            XDocument document = XDocument.Load(path);
            var selectErrorCode = from r in document.Descendants("Error").Where (r => (string)r.Attribute("Code") == _errorCode)
                             select new
                             {
                                 Code = r.Attribute("Code").Value,
                                 ErrorType = r.Attribute("Type").Value,
                                 Description = r.Element("Description").Value,
                                 Procedure = r.Element("Procedure").Value
                             };

            foreach (var r in selectErrorCode)
            {

            }
        }



    }
}
