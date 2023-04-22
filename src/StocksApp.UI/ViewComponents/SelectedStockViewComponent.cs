using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Core.ServiceContracts.FinnhubServices;

namespace StocksApp.UI.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStocksPriceQuoteService;
        private readonly IConfiguration _configuration;

        public SelectedStockViewComponent(IOptions<TradingOptions> tradingOptions, IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteService finnhubStocksPriceQuoteService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStocksPriceQuoteService = finnhubStocksPriceQuoteService;
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? companyProfileDict = null;

            if (stockSymbol != null)
            {
                companyProfileDict = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
                var stockProfileDict = await _finnhubStocksPriceQuoteService.GetStockPriceQuote(stockSymbol);
                if (stockProfileDict != null && companyProfileDict != null) 
                {
                    companyProfileDict.Add("price", stockProfileDict["c"]);
                }
            }

            if (companyProfileDict != null && companyProfileDict.ContainsKey("logo"))
            {
                return View(companyProfileDict);
            }
            else
            {
                return Content("");
            }
        } 
    }
}