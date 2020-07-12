using Microsoft.Extensions.DependencyInjection;
using CosmosDBFundSweepingSampleApp.Core.Services;
using CosmosDBFundSweepingSampleApp.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDBFundSweepingSampleApp.Core.Logger.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Logger;

namespace CosmosDBFundSweepingSampleApp.Web.DependencyInjection
{
    public static class BankServiceCollectionExtensions
    {
        public static IServiceCollection AddBankServices(this IServiceCollection services)
        {
            services.AddScoped<IBankManager, BankManager>();
            services.AddScoped<ILogger, FileLogger>();
            return services;
        }
    }
}
