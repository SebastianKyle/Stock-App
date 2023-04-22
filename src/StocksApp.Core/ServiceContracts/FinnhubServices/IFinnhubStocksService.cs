using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.ServiceContracts.FinnhubServices
{
    /// <summary>
    /// Represents a service that makes HTTP request to finnhub.io to retrieve list of stocks
    /// </summary>
    public interface IFinnhubStocksService
    {
        /// <summary>
        /// Returns list of all stocks supported by an exchange (default: US)
        /// </summary>
        /// <returns>List of stocks</returns>
        Task<List<Dictionary<string, string>>?> GetStocks(); 
    }
}