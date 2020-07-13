using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Core.Services.Contracts
{
    public interface IBankManager
    {
        IEnumerable<BankVM> GetCollection();

        Task<BankDetail> GetBank(string bankId);
    }

}
