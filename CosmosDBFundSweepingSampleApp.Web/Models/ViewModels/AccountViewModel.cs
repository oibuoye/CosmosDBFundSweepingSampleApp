using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBFundSweepingSampleApp.Web.Models.ViewModels
{
    public class AccountViewModel
    {
        public string Id { get; set; }
        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string BankName { get; set; }

        public int BankId { get; set; }

        public DateTime DateCreated { get; set; }



    }

    public class CreateAccountViewModel
    {
        [BindRequired]
        public string AccountName { get; set; }

        [BindRequired]
        public string AccountNumber { get; set; }

        [BindRequired]
        public string BankId { get; set; }
    }

}
