using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProvisioningSystem.Models
{
    [XmlRoot("struktura")]
    public class Structure
    {
        [XmlElement("uczestnik")]
        public List<Participant> Participants { get; set; }
    }
}
