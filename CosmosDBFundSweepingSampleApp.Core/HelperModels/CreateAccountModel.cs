using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Core.HelperModels
{
    public class CreateAccountModel
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string BankId { get; set; }
    }
}
