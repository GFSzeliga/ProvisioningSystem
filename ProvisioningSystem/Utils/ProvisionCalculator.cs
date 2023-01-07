using ProvisioningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningSystem.Utils
{
    public class ProvisionCalculator
    {
        private readonly WireTransfers wireTransfers;
        private readonly List<Participant> participants;
        public List<Participant> ParticipantsWithProvision { get; set; }

        public ProvisionCalculator(WireTransfers wireTransfers, List<Participant> participants)
        {
            this.wireTransfers = wireTransfers;
            this.participants = participants.ToList();
        }

        public void ParticipantProvisionCalculation()
        {
            foreach (var transfer in wireTransfers.WireTransfer)
            {
                var hierarchyList = CurrentHierarchy(transfer.FromId);

                if (hierarchyList.Count() <= 2)
                {
                    hierarchyList.First().Amount += transfer.Amount;
                }
                else
                {
                    hierarchyList.RemoveAt(hierarchyList.Count-1);
                    var amount = transfer.Amount;
                    var count = 0;

                    hierarchyList.ForEach(child => 
                    {                         
                        count++;
                        var amountToBestow = Convert.ToInt32(Math.Floor((double)amount / 2));

                        if (count == hierarchyList.Count())
                        {
                            child.Amount += amount;
                        }
                        else
                        {
                            child.Amount += amountToBestow;
                        }
                        amount -= amountToBestow; 
                        
                    }) ;
                }
            }
        }

        private List<Participant> CurrentHierarchy(int participantId)
        {
            List<Participant> list = new List<Participant>();
            int? participantIdHolder = participantId;
            var currentParticipant = participants.Where(p => p.ParticipantId == participantIdHolder).First();
            list.Add(currentParticipant);

            while (currentParticipant.DirectSupervisor != null)
            {
                participantIdHolder = currentParticipant.DirectSupervisor;
                currentParticipant = participants.Where(p => p.ParticipantId == participantIdHolder).First();
                list.Insert(0,currentParticipant);
            }

            return list;
        }

    }
}
