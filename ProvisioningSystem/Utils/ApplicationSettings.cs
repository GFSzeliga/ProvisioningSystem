using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningSystem.Utils
{
    public class ApplicationSettings
    {
        private const string WireTransferFileName = "przelewy";
        private const string StructureFileName = "struktura";

        public static string WireTransferFilePath 
        { 
            get 
            { 
                return ConfigurationManager.AppSettings[WireTransferFileName]; 
            } 
        }

        public static string StructureFilePath 
        { 
            get 
            { 
                return ConfigurationManager.AppSettings[StructureFileName]; 
            } 
        }
    }
}
