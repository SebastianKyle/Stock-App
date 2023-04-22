using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.ServiceContracts.FinnhubServices;

namespace StocksApp.Core.Services.FinnhubServices
{
    public class FinnhubSearchStocksService : IFinnhubSearchStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FinnhubSearchStocksService> _logger;

        public FinnhubSearchStocksService(IFinnhubRepository finnhubRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<FinnhubSearchStocksService> logger)
        {
            _finnhubRepository = finnhubRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            _logger.LogInformation($"{nameof(SearchStocks)} method of {nameof(FinnhubSearchStocksService)}");

            Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);

            return responseDictionary;
        } 
    }
}