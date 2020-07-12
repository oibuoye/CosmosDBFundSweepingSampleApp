using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Web.Controllers.Handlers.Contracts
{
    public interface IAccountRequestHandler
    {
        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CreateAccountModel</returns>
        Task<CreateAccountModel> CreateAccount(CreateAccountModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AccountDetailsModel>> GetAccounts();

        /// <summary>
        /// Get account list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<AccountDetailsVM> GetAccounts(int page, int take);

        /// <summary>
        /// Get an account details
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<AccountDetailsModel> GetAccount(string accountId);

        /// <summary>
        /// Update account details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateAccount(AccountDetailsModel model);

    }
}
