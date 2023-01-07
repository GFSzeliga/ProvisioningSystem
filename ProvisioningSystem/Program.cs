
using ProvisioningSystem.Models;
using System.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using ProvisioningSystem.Utils;

namespace ProvisioningSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var wireTransferDocument = XmlDeserializerHelper.XmlDeserializer<WireTransfers>(ApplicationSettings.WireTransferFilePath);

            var structure = new XmlDeserializerHelper(ApplicationSettings.StructureFilePath);
            structure.GetAllParticipants();
            var participantsList = structure.Participants;
            
            var provisionCalculator = new ProvisionCalculator(wireTransferDocument, participantsList);
            provisionCalculator.ParticipantProvisionCalculation();
            
            var bestowSystemLevel = new SystemLevelBestower(participantsList);
            bestowSystemLevel.BestowSystemLevel();       
            
            var subordinatesCalculator = new SubordinatesCalculator(participantsList);
            subordinatesCalculator.CalculateSubordinatesWithoutSubordinates();
            
            ResultDisplayer(participantsList);

        }

        private static void ResultDisplayer(List<Participant> participants)
        {
            participants.Sort((x, y) => x.ParticipantId.CompareTo(y.ParticipantId));
            foreach (var participant in participants)
            {
                Console.WriteLine("{0} {1} {2} {3}", 
                    participant.ParticipantId, 
                    participant.SystemLevel, 
                    participant.SubordinatesWithoutSubordinatesCount, 
                    participant.Amount);
            }
        }
    }
}
