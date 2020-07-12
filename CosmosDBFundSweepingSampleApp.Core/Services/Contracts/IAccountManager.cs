using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Core.Services.Contracts
{
    public interface IAccountManager
    {
        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> CreateAccount(AccountDetail model);

        /// <summary>
        /// Update an account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateAccount(AccountDetail model);

        /// <summary>
        /// Get a list of Account Details
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AccountDetailsModel>> GetAccounts();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IEnumerable<AccountDetail>> GetAccounts(int skip, int take);

        /// <summary>
        /// Get an account details using the account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        Task<AccountDetail> GetAccount(string accountNumber);

        /// <summary>
        /// Get an account using Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AccountDetail> GetAccountById(string accountId);

        /// <summary>
        /// check if an account exist using the account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        Task<bool> Exists(string accountNumber);

        /// <summary>
        /// check if an account exist other than this account Id using the account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<bool> ExistsAccount(string accountNumber, string accountId);

        /// <summary>
        /// check if an account exist using the account number or account name
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountName"></param>
        /// <returns></returns>
        Task<bool> Exists(string accountNumber, string accountName);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<RecordStats> GetAggregate();

        /// <summary>
        /// Get account details
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<AccountDetailsModel> GetAccountDetail(string accountId);

        /// <summary>
        /// 
        /// </summary>
        Task GetAccountsCosmos();

    }
}
