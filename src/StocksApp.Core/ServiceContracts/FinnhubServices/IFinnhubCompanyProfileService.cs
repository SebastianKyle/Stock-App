using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.ServiceContracts.FinnhubServices
{
    /// <summary>
    /// Represents a service that make HTTP request to finnhub.io to retrieve company profile details
    /// </summary>
    public interface IFinnhubCompanyProfileService
    {
        /// <summary>
        /// Returns company profile details such as company country, currency, exchange, IPO date, logo image, ...
        /// </summary>
        /// <param name="stockSymbol">Stock symbol to search</param>
        /// <returns>Dictionary contains details such as company country, currency, exchange, IPO date, logo image, ...</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol); 
    }
}