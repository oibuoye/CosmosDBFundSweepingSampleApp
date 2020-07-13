using CosmosDBFundSweepingSampleApp.Core.CoreServices.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Exceptions;
using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.Logger;
using CosmosDBFundSweepingSampleApp.Core.Logger.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Models;
using CosmosDBFundSweepingSampleApp.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Core.CoreServices
{
    public class CoreAccount : ICoreAccount
    {
        private readonly IAccountManager _accountManager;
        private static ILogger _log;

        public CoreAccount(IAccountManager accountManager, ILogger log)
        {
            _accountManager = accountManager;
            _log = log;
        }

        public async Task<APIResponse> CreateAccount(AccountDetail model)
        {
            try
            {
                //Confirn the existence of the account
                var checkAccountNumber = await _accountManager.Exists(model.AccountNumber, model.AccountName);
                if (checkAccountNumber)
                {
                    throw new AccountDetailsAlreadyExistException($"Account details already exist");
                }

                await _accountManager.CreateAccount(model);

                return new APIResponse { ResponseObject = model };
            }
            catch (AccountDetailsAlreadyExistException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AccountDetailsModel>> GetAccountDetails()
        {
            return await _accountManager.GetAccounts();
        }

        public async Task<AccountDetailsVM> GetAccountDetails(int page, int take)
        {
            int skip = page < 2 ? 0 : (take * page) - take;

            AccountDetailsVM vM = new AccountDetailsVM();
            vM.accounts = _accountManager.GetAccounts(skip, take).Result.Select(x =>
             new AccountDetailsModel()
             {
                 ID = x.Id,
                 AccountName = x.AccountName,
                 AccountNumber = x.AccountNumber,
                 BankName = x.BankDetail.Name,
                 DateCreated = x.CreatedAtUtc.ToString("dd MMM yyyy")
             });

            var datasize = _accountManager.GetAggregate().Result.RowCount;

            double pageSize = ((double)datasize / (double)take);
            int pages = 0;

            if (pageSize < 1 && datasize >= 1) { pages = 1; }
            else { pages = (int)Math.Ceiling(pageSize); }

            vM.DataSize = datasize;
            vM.PageSize = pages;

            return await Task.FromResult(vM);
        }

        public async Task<AccountDetailsModel> GetAccountDetail(string accountId)
        {
            return await _accountManager.GetAccountDetail(accountId);
        }

        public async Task<bool> UpdateAccount(AccountDetailsModel model, BankDetail bankDetail)
        {
            try
            {
                //Confirm the existence of the account number
                var checkAccountNumber = await _accountManager.ExistsAccount(model.AccountNumber, model.ID);
                if (checkAccountNumber)
                {
                    throw new AccountDetailsAlreadyExistException($"Account number { model.AccountNumber } already exist");
                }

                AccountDetail accountDetails = await _accountManager.GetAccountById(model.ID);
                _log.Warn($"Old account details::: {JsonConvert.SerializeObject(accountDetails)}");
                accountDetails.Id = model.ID;
                accountDetails.AccountName = model.AccountName;
                accountDetails.AccountNumber = model.AccountNumber;
                accountDetails.BankDetail = new BankDetail { Name = bankDetail.Name, Code = bankDetail.Code, Id = model.BankId };
                accountDetails.UpdatedAtUtc = DateTime.Now;
                await _accountManager.UpdateAccount(accountDetails);
                _log.Warn($"New account details::: {JsonConvert.SerializeObject(accountDetails)}");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GetAccountsCosmos()
        {
            await _accountManager.GetAccountsCosmos();
        }
    }
}
