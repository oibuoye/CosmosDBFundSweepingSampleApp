using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.Configuration
{
    public interface IJwtSettings
    {
        int ExpirePeriod { get; set; }
        string Key { get; set; }
    }

    public class JwtSettings : IJwtSettings
    {
        public string Key { get; set; }
        /// <summary>
        /// Expire period in minutes
        /// </summary>
        public int ExpirePeriod { get; set; }
    }
}
