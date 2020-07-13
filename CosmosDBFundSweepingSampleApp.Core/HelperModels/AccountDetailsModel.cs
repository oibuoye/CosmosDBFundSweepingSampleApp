using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Core.HelperModels
{
    public class AccountDetailsModel
    {
        public string ID { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string BankId { get; set; }

        public string BankName { get; set; }

        public string DateCreated { get; set; }
    }
}
