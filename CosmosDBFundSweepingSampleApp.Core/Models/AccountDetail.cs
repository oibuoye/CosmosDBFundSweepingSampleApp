using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Models
{
    public class AccountDetail : BaseModel
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }

        [JsonProperty(PropertyName = "accountName")]
        public virtual string AccountName { get; set; }

        [JsonProperty(PropertyName = "accountNumber")]
        public virtual string AccountNumber { get; set; }

        [JsonProperty(PropertyName = "bankDetail")]
        public virtual BankDetail BankDetail { get; set; }

    }
}
