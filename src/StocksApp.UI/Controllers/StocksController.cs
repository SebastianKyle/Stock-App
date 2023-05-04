using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StocksApp.Core.ServiceContracts.FinnhubServices;
using StocksApp.UI.Models;

namespace StocksApp.UI.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class StocksController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubStocksService _finnhubStocksService;
        private readonly ILogger<StocksController> _logger;

        /// <summary>
        /// Constructor for StocksController that executes when a new object is created for the class
        /// </summary>
        /// <param name="tradingOptions">Inject TradingOptions config through Options pattern</param>
        /// <param name="finnhubService">Inject FinnhubService</param>
        /// <param name="logger">Inject ILogger object for logging</param>
        public StocksController(IOptions<TradingOptions> tradingOptions, IFinnhubStocksService finnhubStocksService, ILogger<StocksController> logger)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubStocksService = finnhubStocksService;
            _logger = logger;
        }

        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            _logger.LogInformation($"{nameof(Explore)} IAction method of {nameof(StocksController)}");

            List<Dictionary<string, string>>? stocksDictionary = await _finnhubStocksService.GetStocks(); 
            List<Stock> stocks = new List<Stock>();

            if (stocksDictionary is not null)
            {
                // filter stocks
                if (!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(",");
                    if (Top25PopularStocksList is not null)
                    {
                        stocksDictionary = stocksDictionary.Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"]))).ToList();
                    }
                }

                // Convert dictionary objects to stock objects
                stocks = stocksDictionary.Select(temp => new Stock() {
                    StockSymbol = Convert.ToString(temp["symbol"]),
                    StockName = Convert.ToString(temp["description"])
                }).ToList();
            }

            // Dictionary<string, object>? selectedStock = await _finnhubService.SearchStocks(stock);
            // ViewBag.Stock = selectedStock;
            ViewBag.Stock = stock;

            return View(stocks);
        }
    }
}