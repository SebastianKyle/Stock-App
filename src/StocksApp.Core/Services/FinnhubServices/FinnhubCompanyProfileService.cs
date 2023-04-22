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
    public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
    {
        private readonly IFinnhubRepository _finnhubRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FinnhubCompanyProfileService> _logger;

        public FinnhubCompanyProfileService(IFinnhubRepository finnhubRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<FinnhubCompanyProfileService> logger)
        {
            _finnhubRepository = finnhubRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            _logger.LogInformation($"{nameof(GetCompanyProfile)} method of {nameof(FinnhubCompanyProfileService)}");

            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);

            return responseDictionary;
        } 
    }
}