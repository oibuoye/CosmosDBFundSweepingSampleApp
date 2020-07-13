using Microsoft.Extensions.DependencyInjection;
using CosmosDBFundSweepingSampleApp.Web.Controllers.Handlers;
using CosmosDBFundSweepingSampleApp.Web.Controllers.Handlers.Contracts;
using CosmosDBFundSweepingSampleApp.Core.CoreServices;
using CosmosDBFundSweepingSampleApp.Core.CoreServices.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Models;
using CosmosDBFundSweepingSampleApp.Core.Services;
using CosmosDBFundSweepingSampleApp.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDBFundSweepingSampleApp.Core.SessionManagement.Contracts;
using CosmosDBFundSweepingSampleApp.Core.SessionManagement;

namespace CosmosDBFundSweepingSampleApp.Web.DependencyInjection
{
    public static class AccountServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountRequestHandler, AccountRequestHandler>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<ICoreAccount, CoreAccount>();
            services.AddScoped<ISessionManager, SessionManager>();
            return services;
        }
    }
}
