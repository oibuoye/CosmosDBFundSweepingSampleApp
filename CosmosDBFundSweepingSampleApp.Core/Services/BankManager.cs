using Microsoft.AspNetCore.Hosting;
using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.Logger;
using CosmosDBFundSweepingSampleApp.Core.Logger.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Models;
using CosmosDBFundSweepingSampleApp.Core.Services.Contracts;
using CosmosDBFundSweepingSampleApp.Core.SessionManagement.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Core.Services
{
    public class BankManager : IBankManager
    {
        private static ISessionManager _cosmosSession;
        private static ILogger _log;

        public BankManager(ILogger log, ISessionManager cosmosSession)
        {
            _cosmosSession = cosmosSession;
            _log = log;
        }

        public async Task<BankDetail> GetBank(string bankId)
        {
            try
            {
                var clientObject = _cosmosSession.GetSession();
                var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(Bank).Name);
                var sql = $"SELECT c.name, c.code FROM c WHERE c.id ='{bankId}'";
                var iterator = container.GetItemQueryIterator<BankDetail>(sql);
                var page = await iterator.ReadNextAsync();
                return page.Resource.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<BankVM> GetCollection()
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(Bank).Name);
            var sql = "SELECT c.id, c.name FROM c";
            var iterator = container.GetItemQueryIterator<BankVM>(sql);
            var page = iterator.ReadNextAsync().Result;
            _log.ChargeInfo($"Get list of all banks: {page.RequestCharge} RUs");
            return page.Resource;
        }

    }
}
