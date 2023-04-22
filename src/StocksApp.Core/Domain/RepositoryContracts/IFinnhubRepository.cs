using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents a repository that make HTTP request to finnhub.io
    /// </summary>
    public interface IFinnhubRepository
    {
        /// <summary>
        /// Returns company details such as company country, currency, exchange, IPO Date, logo image, ...
        /// </summary>
        /// <param name="stockSymbol">Stock symbol to search</param>
        /// <returns>Returns a dictionary that contains details such as company country, currency, exchange, IPO date, logo image, ...</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        /// <summary>
        /// Returns stock price details such as current price, change in price, percentage change, high price, low price, ...
        /// </summary>
        /// <param name="stockSymbol">Stock symbol to search</param>
        /// <returns>Returns a dictionary contains details of the stock such as current price, change in price, percentage in change, high price, low price, ... </returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);

        /// <summary>
        /// Returns list of stocks supported by an exchange (default: US)
        /// </summary>
        /// <returns>List of stocks</returns>
        Task<List<Dictionary<string, string>>?> GetStocks();

        /// <summary>
        /// Returns list of stocks matched with the given stock symbol
        /// </summary>
        /// <param name="stockSymbolToSearch">Stock symbol to search</param>
        /// <returns>List of matching stocks</returns>
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}