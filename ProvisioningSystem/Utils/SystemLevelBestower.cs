using ProvisioningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningSystem.Utils
{
    public class SystemLevelBestower
    {
        private readonly List<Participant> participants;

        public SystemLevelBestower(List<Participant> participants)
        {
            this.participants = participants;
        }

        private Participant GetFounder()
        {
            return participants.Where(p => p.DirectSupervisor == null).First();
        }

        public void BestowSystemLevel()
        {
            var level = 0;
            var founder = GetFounder();
            founder.SystemLevel = level;
            List<Participant> tempParticipantList = GetSubordinates(founder);

            while (tempParticipantList.Count != 0)
            {
                level++;
                tempParticipantList.ForEach(p => p.SystemLevel = level);
                tempParticipantList = tempParticipantList.SelectMany(GetSubordinates).ToList();
            } 
        }

        private List<Participant> GetSubordinates(Participant participant)
        {
            return participant.DirectSubordinateIds.SelectMany(s => participants.Where(p => p.ParticipantId == s)).ToList();
        }

    }
}
