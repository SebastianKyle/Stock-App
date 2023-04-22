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
    public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
    {
        private readonly IFinnhubRepository _finnhubRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FinnhubStockPriceQuoteService> _logger;

        public FinnhubStockPriceQuoteService(IFinnhubRepository finnhubRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<FinnhubStockPriceQuoteService> logger)
        {
            _finnhubRepository = finnhubRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            _logger.LogInformation($"{nameof(GetStockPriceQuote)} method of {nameof(FinnhubStockPriceQuoteService)}");

            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);

            return responseDictionary;
        } 
    }
}