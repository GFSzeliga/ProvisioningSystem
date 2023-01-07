using ProvisioningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace ProvisioningSystem.Utils
{
    public class XmlDeserializerHelper
    {
        public List<Participant> Participants;
        private IEnumerable<XElement> Nodes;
        private readonly string _filePath;
        private const string IdAttribute = "id";
        private XElement RootElement { get { return DocumentRootElement(_filePath); } }

        public XmlDeserializerHelper(string filePath)
        {
            _filePath = filePath;
        }

        private XDocument XmlDocument(string filePath)
        {
            return XDocument.Load(filePath);
        }

        private XElement DocumentRootElement(string filePath)
        {
            return XmlDocument(filePath).Root;
        }

        private bool CurrentNodeChildExist(XElement xElement)
        {
            return xElement.Elements().Any();
        }

        private IEnumerable<XElement> CurrentNodeChildElements(XElement xElement)
        {
            return xElement.Elements();
        }

        private void AllNodes(XElement xElement)
        {
            if (CurrentNodeChildExist(xElement))
            {
                var childNodesElements = CurrentNodeChildElements(xElement);
                Nodes = (Nodes ?? Enumerable.Empty<XElement>()).Concat(childNodesElements ?? Enumerable.Empty<XElement>());

                foreach (var node in childNodesElements)
                {
                    AllNodes(node);
                }
            }
        }

        private void AllNodes()
        {
            AllNodes(RootElement);
        }

        public void GetAllParticipants()
        {
            AllNodes();
            Participants = Nodes.Select(GetChildNodesAsParticipants).ToList();
        }

        private Participant GetChildNodesAsParticipants(XElement xElement)
        {
            return new Participant()
            {
                ParticipantId = Convert.ToInt32(xElement.Attribute(IdAttribute).Value),
                DirectSupervisor = xElement.Parent.HasAttributes ? Convert.ToInt32(xElement.Parent.Attribute(IdAttribute).Value) : null,
                DirectSubordinateIds = xElement.Elements().Select(m => Convert.ToInt32(m.Attribute(IdAttribute).Value)),
                Amount = 0
            };
        }

       public static T XmlDeserializer<T>(string filePath)
       {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T xmlDeserializer;

            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                xmlDeserializer = (T) serializer.Deserialize(reader);
            }
            return xmlDeserializer;
       }
    }
}
