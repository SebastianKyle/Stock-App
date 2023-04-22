using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.ServiceContracts.FinnhubServices;

namespace StocksApp.Core.Services.FinnhubServices
{
    public class FinnhubStocksService : IFinnhubStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FinnhubStocksService> _logger;

        public FinnhubStocksService(IFinnhubRepository finnhubRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<FinnhubStocksService> logger)
        {
            _finnhubRepository = finnhubRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            _logger.LogInformation($"{nameof(GetStocks)} method of {nameof(FinnhubStocksService)}");

            // Use serilog timings to measure the time taken for retrieving the stocks from server
            List<Dictionary<string, string>>? responseDictionary;
            using (Operation.Time("Time for retrieving all stocks from Finnhub server"))
            {
                responseDictionary = await _finnhubRepository.GetStocks();
            }

            return responseDictionary;
        } 
    }
}