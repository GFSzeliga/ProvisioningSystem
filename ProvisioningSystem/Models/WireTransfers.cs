using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProvisioningSystem.Models
{
    [XmlRoot("przelewy")]
    public class WireTransfers
    {
        [XmlElement("przelew")]
        public List<WireTransfer> WireTransfer { get; set; }
    }
}
