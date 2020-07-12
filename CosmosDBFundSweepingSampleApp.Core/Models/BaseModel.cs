using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Models
{
    public class BaseModel
    {


        private DateTime _createdUtc;
        [JsonProperty(PropertyName = "createdAtUtc")]
        public virtual DateTime CreatedAtUtc
        {
            get
            {
                if (_createdUtc == DateTime.MinValue) { _createdUtc = DateTime.Now.ToLocalTime(); }
                return _createdUtc;
            }
            set
            {
                if (_createdUtc == DateTime.MinValue) { _createdUtc = value; }
            }
        }

        private DateTime? _updatedAtUtc;
        [JsonProperty(PropertyName = "updatedAtUtc")]
        public virtual DateTime? UpdatedAtUtc
        {
            get
            {
                if (_updatedAtUtc == null) { _updatedAtUtc = DateTime.Now.ToLocalTime(); }
                return _updatedAtUtc;
            }
            set
            {
                _updatedAtUtc = value;
            }
        }
    }
}
