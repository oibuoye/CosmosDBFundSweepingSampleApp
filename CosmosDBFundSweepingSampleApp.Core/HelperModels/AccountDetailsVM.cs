using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.HelperModels
{
    public class AccountDetailsVM
    {
        public Int64 DataSize { get; set; }

        public Int64 PageSize { get; set; }

        public IEnumerable<AccountDetailsModel> accounts { get; set; }
    }
}
