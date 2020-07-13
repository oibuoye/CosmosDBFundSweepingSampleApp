using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Core.CoreServices.Contracts
{
    public interface ICoreAccount
    {
        /// <summary>
        /// Create new account details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Task<APIResponse></returns>
        Task<APIResponse> CreateAccount(AccountDetail model);

        /// <summary>
        /// Get list of account details
        /// </summary>
        /// <returns>Task<IEnumerable<AccountDetailsModel>></returns>
        Task<IEnumerable<AccountDetailsModel>> GetAccountDetails();

        /// <summary>
        /// Get paginated account details
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns>Task<AccountDetailsVM></returns>
        Task<AccountDetailsVM> GetAccountDetails(int page, int take);

        /// <summary>
        /// Get account details using account Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<AccountDetailsModel> GetAccountDetail(string accountId);

        /// <summary>
        /// Update account details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bankDetail"></param>
        /// <returns></returns>
        Task<bool> UpdateAccount(AccountDetailsModel model, BankDetail bankDetail);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task GetAccountsCosmos();

    }
}
