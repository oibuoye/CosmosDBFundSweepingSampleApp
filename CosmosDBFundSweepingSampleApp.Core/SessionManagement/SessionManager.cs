using Microsoft.Azure.Cosmos;
using CosmosDBFundSweepingSampleApp.Core.Configuration;
using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.SessionManagement.Contracts;
using System;
using CosmosDBFundSweepingSampleApp.Core.HelperModels;

namespace CosmosDBFundSweepingSampleApp.Core.SessionManagement
{
    public class SessionManager : ISessionManager
    {
        public static CosmosClient Client { get; set; }
        private readonly ICosmosDBSettings _cosmosDBSettings;

        public SessionManager(ICosmosDBSettings cosmosDBSettings)
        {
            _cosmosDBSettings = cosmosDBSettings;
        }

        /// <summary>
        /// This returns CosmosClientObject object containing cosmos client session and database Id
        /// </summary>
        /// <returns>CosmosClientObject</returns>
        public CosmosClientObject GetSession()
        {
            if (Client == null)
            {
                try
                {
                    Client = new CosmosClient(_cosmosDBSettings.Endpoint, _cosmosDBSettings.MasterKey);
                    return new CosmosClientObject { Client = Client, DBName = _cosmosDBSettings.DBName };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return new CosmosClientObject { Client = Client, DBName = _cosmosDBSettings.DBName };
        }
    }
}
