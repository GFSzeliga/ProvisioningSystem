using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProvisioningSystem.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public IEnumerable<int>? DirectSubordinateIds { get; set; }
        public int? DirectSupervisor { get; set; }
        public int? Amount { get; set; }
        public int SystemLevel { get; set; }
        public int SubordinatesWithoutSubordinatesCount { get; set; }
    }
}
