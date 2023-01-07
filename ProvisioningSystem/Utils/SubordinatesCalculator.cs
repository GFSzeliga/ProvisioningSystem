using ProvisioningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningSystem.Utils
{
    public class SubordinatesCalculator
    {
        private readonly List<Participant> participants;

        public SubordinatesCalculator(List<Participant> participants)
        {
            this.participants = participants;
        }

        public void CalculateSubordinatesWithoutSubordinates()
        {
            foreach (var participant in participants)
            {
                var count = 0;

                List<Participant> tempParticipantList = GetSubordinates(participant);

                while (tempParticipantList.Count != 0)
                {
                    count += tempParticipantList.Where(p => p.DirectSubordinateIds.Count() == 0).Count();
                    tempParticipantList = tempParticipantList.SelectMany(GetSubordinates).ToList();
                }
                participant.SubordinatesWithoutSubordinatesCount= count;
            }
        }

        private List<Participant> GetSubordinates(Participant participant)
        {
            return participant.DirectSubordinateIds.SelectMany(s => participants.Where(p => p.ParticipantId == s)).ToList();
        }

    }
}
