using CosmosDBFundSweepingSampleApp.Core.CoreServices.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Exceptions;
using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.Logger;
using CosmosDBFundSweepingSampleApp.Core.Logger.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Models;
using CosmosDBFundSweepingSampleApp.Core.Services.Contracts;
using CosmosDBFundSweepingSampleApp.Web.Controllers.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Web.Controllers.Handlers
{
    public class AccountRequestHandler : IAccountRequestHandler
    {
        private readonly ICoreAccount _coreAccount;
        private readonly ILogger _log;
        private readonly IBankManager _bankManager;

        public AccountRequestHandler(ICoreAccount coreAccount, IBankManager bankManager, ILogger log)
        {
            _coreAccount = coreAccount;// ?? throw new ArgumentNullException(nameof(coreAccount));
            _bankManager = bankManager;
            _log = log;
        }

        public async Task<CreateAccountModel> CreateAccount(CreateAccountModel model)
        {
            try
            {
                if (model == null)
                {
                    _log.Warn("passed account model is null");
                    throw new ModelIsNullException("request model is null");
                }

                var response = await _bankManager.GetBank(model.BankId);
                var accountDetails = new AccountDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountName = model.AccountName,
                    AccountNumber = model.AccountNumber,
                    BankDetail = new BankDetail { Code = response.Code, Name = response.Name, Id = model.BankId },
                    CreatedAtUtc = DateTime.Now,
                    UpdatedAtUtc = DateTime.Now
                };
                _log.Info("about to create account");
                await _coreAccount.CreateAccount(accountDetails);
                return model;

            }
            catch (AccountDetailsAlreadyExistException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AccountDetailsModel> GetAccount(string accountId)
        {
            return await _coreAccount.GetAccountDetail(accountId);
        }

        public async Task<IEnumerable<AccountDetailsModel>> GetAccounts()
        {
            return await _coreAccount.GetAccountDetails();
        }

        public async Task<AccountDetailsVM> GetAccounts(int page, int take)
        {
            //await _coreAccount.GetAccountsCosmos();
            return await _coreAccount.GetAccountDetails(page, take);
        }

        public async Task<bool> UpdateAccount(AccountDetailsModel model)
        {
            var response = await _bankManager.GetBank(model.BankId);
            return await _coreAccount.UpdateAccount(model, response);
        }
    }
}
