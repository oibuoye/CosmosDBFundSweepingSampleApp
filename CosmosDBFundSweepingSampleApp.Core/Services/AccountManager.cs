using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
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
    public class AccountManager : IAccountManager
    {
        private static ISessionManager _cosmosSession;
        private static ILogger _log;
        private readonly IBankManager _bankManager;

        public AccountManager(ILogger log, ISessionManager cosmosSession, IBankManager bankManager)
        {
            _cosmosSession = cosmosSession;
            _bankManager = bankManager;
            _log = log;
        }

        public async Task<bool> CreateAccount(AccountDetail model)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var response = await container.CreateItemAsync(model, new PartitionKey(model.AccountNumber));
            _log.ChargeInfo($"Create new account: {response.RequestCharge} RUs");
            return true;
        }

        public async Task<bool> Exists(string accountNumber)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var sql = $"SELECT c.Id FROM c WHERE c.accountNumber='{accountNumber}'";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);
            var page = await iterator.ReadNextAsync();
            _log.ChargeInfo($"Check if account number exist: {page.RequestCharge} RUs");
            return page.Count() > 0;
        }

        public async Task<bool> ExistsAccount(string accountNumber, string accountId)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var sql = $"SELECT c.Id FROM c WHERE c.accountNumber='{accountNumber}' AND c.id != '{accountId}'";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);
            var page = await iterator.ReadNextAsync();
            _log.ChargeInfo($"Check if account number exist but not for a specified account: {page.RequestCharge} RUs");
            return page.Count() > 0;
        }

        public async Task<bool> Exists(string accountNumber, string accountName)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var sql = $"SELECT c.Id FROM c WHERE c.accountNumber='{accountNumber}' AND c.accountName='{accountName}'";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);
            var page = await iterator.ReadNextAsync();
            _log.ChargeInfo($"Check account existence using account name and account number: {page.RequestCharge} RUs");
            return page.Count() > 0;

            //var clientObject = _cosmosSession.GetSession();
            //var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            //return await (from acct in container.GetItemLinqQueryable<AccountDetail>(allowSynchronousQueryExecution: true)
            //        where acct.AccountNumber == accountNumber && acct.AccountName == accountName
            //        select acct).CountAsync() > 0;
        }

        /// <summary>
        /// Get an account details using the account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public async Task<AccountDetail> GetAccount(string accountNumber)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var sql = "SELECT * FROM c WHERE c.AccountNumber ="+ accountNumber;
            var iterator = container.GetItemQueryIterator<AccountDetail>(sql);
            var page = await iterator.ReadNextAsync();

            foreach (var item in page)
            {
                return item;
            }
            return new AccountDetail { };
        }

        /// <summary>
        /// Get account details
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<AccountDetailsModel> GetAccountDetail(string accountId)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var response = from acct in container.GetItemLinqQueryable<AccountDetail>(allowSynchronousQueryExecution: true)
                    where acct.Id == accountId
                    select new AccountDetailsModel
                    {
                        ID = acct.Id,
                        AccountName = acct.AccountName,
                        AccountNumber = acct.AccountNumber,
                        BankName = acct.BankDetail.Name,
                    };

            return await Task.FromResult(response.FirstOrDefault());
        }

        public async Task<AccountDetail> GetAccountById(string accountId)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var sql = "SELECT * FROM c WHERE c.Id =" + accountId;
            var iterator = container.GetItemQueryIterator<AccountDetail>(sql);
            var page = await iterator.ReadNextAsync();

            foreach (var item in page)
            {
                return item;
            }
            return new AccountDetail { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AccountDetailsModel>> GetAccounts()
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var response = from acct in container.GetItemLinqQueryable<AccountDetail>(allowSynchronousQueryExecution: true)
                           select new AccountDetailsModel
                           {
                               ID = acct.Id,
                               AccountName = acct.AccountName,
                               AccountNumber = acct.AccountNumber,
                               BankName = acct.BankDetail.Name,
                               DateCreated = acct.CreatedAtUtc.ToString("dd MMM yyyy")
                           };

            return await Task.FromResult(response.ToList());
        }

        public async Task<IEnumerable<AccountDetail>> GetAccounts(int skip, int take)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var sql = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<AccountDetail>(sql);
            var page =  await iterator.ReadNextAsync();

            return page.Resource;
        }

        public async Task GetAccountsCosmos()
        {
            try
            {
                var clientObject = _cosmosSession.GetSession();
                var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
                var sql = "SELECT * FROM c WHERE c.AccountNumber = '0035509419'";
                var iterator = container.GetItemQueryIterator<dynamic>(sql);
                var page = await iterator.ReadNextAsync();

                foreach(var item in page)
                {
                    var bankResponse = await _bankManager.GetBank(item.BankId.ToString());
                    item.BankDetail.Name = bankResponse.Name;
                    item.BankDetail.Code = bankResponse.Code;
                    var response = await container.ReplaceItemAsync<dynamic>(item, item.Id);
                    _log.ChargeInfo($"Create new account: {response.RequestCharge} RUs");
                }
            }
            catch (Exception ex)
            {
                string mesg = ex.Message;
            }
        }

        public async Task<RecordStats> GetAggregate()
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            var sql = "SELECT c.Id FROM c";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);
            var page = await iterator.ReadNextAsync();
            return new RecordStats { RowCount = page.Count() };
        }

        public async Task<bool> UpdateAccount(AccountDetail model)
        {
            var clientObject = _cosmosSession.GetSession();
            var container = clientObject.Client.GetContainer(clientObject.DBName, typeof(AccountDetail).Name);
            await container.ReplaceItemAsync<dynamic>(model, model.Id);

            return true;
        }
    }

}
