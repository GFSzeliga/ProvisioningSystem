using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProvisioningSystem.Models
{
    [XmlRoot("przelew")]
    public class WireTransfer
    {
        [XmlAttribute("od")]
        public int FromId { get; set; }
        [XmlAttribute("kwota")]
        public int Amount { get; set; }
    }
}
