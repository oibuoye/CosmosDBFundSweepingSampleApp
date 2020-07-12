using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Configuration
{
    public class CosmosDBSettings : ICosmosDBSettings
    {
        public string Endpoint { get; set; }

        public string MasterKey { get; set; }

        public string DBName { get; set; }
    }

    public interface ICosmosDBSettings
    {
        string Endpoint { get; set; }

        string MasterKey { get; set; }

        string DBName { get; set; }
    }

}
