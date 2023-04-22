using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.ServiceContracts.FinnhubServices;
using StocksApp.Core.ServiceContracts.StocksServices;
using StocksApp.Core.Services.FinnhubServices;
using StocksApp.Core.Services.StocksServices;
using StocksApp.Infrastructure.AppDbContext;
using StocksApp.Infrastructure.Repositories;
using StocksApp.UI.Middlewares;

namespace StocksApp.UI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddControllersWithViews();
            services.Configure<TradingOptions>(configuration.GetSection("TradingOptions"));
            services.AddHttpClient();

            // Add services
            services.AddTransient<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
            services.AddTransient<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
            services.AddTransient<IFinnhubStocksService, FinnhubStocksService>();
            services.AddTransient<IFinnhubSearchStocksService, FinnhubSearchStocksService>();
            services.AddTransient<IFinnhubRepository, FinnhubRepository>();
            services.AddTransient<IBuyOrdersService, BuyOrdersService>();
            services.AddTransient<ISellOrdersService, SellOrdersService>();
            services.AddTransient<IStocksRepository, StocksRepository>();

            // Application Db context
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpLogging(options => {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties
                                      | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders;
            });

            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}