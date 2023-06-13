using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using StocksApp.Core.Domain.IdentityEntities;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.ServiceContracts.AccountBalanceServices;
using StocksApp.Core.ServiceContracts.FinnhubServices;
using StocksApp.Core.ServiceContracts.StocksServices;
using StocksApp.Core.ServiceContracts.UserStockServices;
using StocksApp.Core.Services.AccountBalanceServices;
using StocksApp.Core.Services.FinnhubServices;
using StocksApp.Core.Services.StocksServices;
using StocksApp.Core.Services.UserStockServices;
using StocksApp.Infrastructure.AppDbContext;
using StocksApp.Infrastructure.Repositories;
using StocksApp.UI.Middlewares;

namespace StocksApp.UI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment) 
        {
            services.AddControllersWithViews();
            services.Configure<TradingOptions>(configuration.GetSection("TradingOptions"));
            services.AddHttpClient();

            // Add services
            services.AddScoped<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
            services.AddScoped<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
            services.AddScoped<IFinnhubStocksService, FinnhubStocksService>();
            services.AddScoped<IFinnhubSearchStocksService, FinnhubSearchStocksService>();
            services.AddScoped<IFinnhubRepository, FinnhubRepository>();

            services.AddScoped<IBuyOrdersService, BuyOrdersService>();
            services.AddScoped<ISellOrdersService, SellOrdersService>();
            services.AddScoped<IStocksRepository, StocksRepository>();

            services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();
            services.AddScoped<IAccountBalanceCreateService, AccountBalanceCreateService>();
            services.AddScoped<IAccountBalanceGetService, AccountBalanceGetService>();

            services.AddScoped<IAccountBalanceDepositService, AccountBalanceDepositService>();
            services.AddScoped<IAccountBalanceWithdrawService, AccountBalanceWithdrawService>();

            services.AddScoped<IUserStockRepository, UserStockRepository>();
            services.AddScoped<IUserStockAddService, UserStockAddService>();
            services.AddScoped<IUserStockDecreaseService, UserStockDecreaseService>();
            services.AddScoped<IUserStockGetService, UserStockGetService>();

            // Application Db context
            services.AddDbContext<ApplicationDbContext>(options => {
                if (environment.IsProduction())
                {
                    options.UseSqlServer(configuration.GetConnectionString("AzureDbConnection"), sqlServerOptions => {
                        sqlServerOptions.EnableRetryOnFailure();
                    });
                } 
                else 
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
            });

            // Add identity service
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
            })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            services.AddAuthorization(options => {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // enforces authorization policy for all action method
            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Account/Login";
            });

            services.AddHttpLogging(options => {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties
                                      | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders;
            });

            services.AddScoped<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}