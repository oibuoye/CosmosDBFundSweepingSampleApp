using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.HelperModels
{
    public class BankVM
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public virtual string Name { get; set; }
    }
}
