using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.ServiceContracts.FinnhubServices
{
    /// <summary>
    /// Represents a service that makes HTTP request to finnhub.io to retrieve matching stock with given stock symbol
    /// </summary>
    public interface IFinnhubSearchStocksService
    {
        /// <summary>
        /// Returns list of matching stocks based on the given stock symbol
        /// </summary>
        /// <param name="stockSymbolToSearch">Stock symbol to search</param>
        /// <returns>List of matching stocks</returns>
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch); 
    }
}