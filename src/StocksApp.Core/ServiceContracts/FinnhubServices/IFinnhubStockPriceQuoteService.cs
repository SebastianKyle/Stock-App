using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.ServiceContracts.FinnhubServices
{
    /// <summary>
    /// Represents a service that makes HTTP request to finnhub.io to retrieve price details of the stock
    /// </summary>
    public interface IFinnhubStockPriceQuoteService
    {
        /// <summary>
        /// Returns stock price details such as current price, change in price, percentage change, high price, low price, ...
        /// </summary>
        /// <param name="stockSymbol">Stock symbol to search</param>
        /// <returns>Dictionary contains details such as current price, change in price, percentage change, high price, low price, ...</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol); 
    }
}