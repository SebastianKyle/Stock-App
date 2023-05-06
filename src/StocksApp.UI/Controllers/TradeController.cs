using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StocksApp.Core.DTO;
using StocksApp.Core.ServiceContracts.FinnhubServices;
using StocksApp.Core.ServiceContracts.StocksServices;
using StocksApp.Core.ServiceContracts.UserStockServices;
using StocksApp.UI.Filters.ActionFilters;
using StocksApp.UI.Models;

namespace StocksApp.UI.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStocksPriceQuoteService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        private readonly IBuyOrdersService _buyOrdersService;
        private readonly ISellOrdersService _sellOrdersService;
        private readonly IUserStockAddService _userStockAddService;
        private readonly ILogger<TradeController> _logger;

        /// <summary>
        /// Constructor for TradeController
        /// </summary>
        /// <param name="finnhubService">Inject FinnhubService</param>
        /// <param name="tradingOptions">Inject TradingOptions config through Options pattern</param>
        /// <param name="configuration">Inject IConfiguration</param>
        /// <param name="stocksService">Inject StocksService</param>
        /// <param name="logger">Inject ILogger object for logging</param>
        public TradeController(IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration, IBuyOrdersService buyOrdersService, ISellOrdersService sellOrdersService, IUserStockAddService userStockAddService, ILogger<TradeController> logger) 
        {
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStocksPriceQuoteService = finnhubStockPriceQuoteService;
            _tradingOptions = tradingOptions.Value;
            _configuration = configuration;
            _buyOrdersService = buyOrdersService;
            _sellOrdersService = sellOrdersService;
            _userStockAddService = userStockAddService;
            _logger = logger;
        }

        [Route("[action]/{stockSymbol}")]
        [Route("~/[controller]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol, bool succeeded = true)
        {
            _logger.LogInformation($"{nameof(Index)} IAction method of {nameof(TradeController)} controller");

            if (string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }

            Dictionary<string, object>? companyProf = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);    
            Dictionary<string, object>? stockPrice = await _finnhubStocksPriceQuoteService.GetStockPriceQuote(stockSymbol);

            StockTrade stock = new StockTrade() {
                StockSymbol = stockSymbol,
                StockName = Convert.ToString(companyProf["name"]),
                Price = Convert.ToDouble(stockPrice["c"].ToString())
            };
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            if (succeeded == false)
            {
                TempData["Succeeded"] = 0;
                ViewBag.ErrorMessage = TempData["Errors"];
            }
            else
            {
                TempData["Succeeded"] = 1;
            }

            return View(stock);
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        // [TypeFilter(typeof(HandleExceptionFilter))] // with TypeFilter, we can inject ILogger
        public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
        {
            _logger.LogInformation($"{nameof(BuyOrder)} IAction method of {nameof(TradeController)} controller");

            Guid userID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            orderRequest.UserID = userID;

            // invoke service method
            BuyOrderResponse buyOrderResponse = new BuyOrderResponse();
            try
            {
                buyOrderResponse = await _buyOrdersService.CreateBuyOrder(orderRequest);
            }
            catch (ArgumentException ex)
            {
                TempData["Errors"] = ex.Message;
                return RedirectToAction(nameof(Index), new { stockSymbol = orderRequest.StockSymbol, succeeded = false });
            }

            UserStockRequest userStockRequest = new UserStockRequest() {
                UserID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                StockSymbol = orderRequest.StockSymbol,
                StockName = orderRequest.StockName,
                Quantity = orderRequest.Quantity
            };
            await _userStockAddService.AddUserStock(userStockRequest);

            TempData["Succeeded"] = 1;

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        // [TypeFilter(typeof(HandleExceptionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
            _logger.LogInformation($"{nameof(SellOrder)} IAction method of {nameof(TradeController)} controller");

            Guid userID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            orderRequest.UserID = userID;

            // Invoke service method
            SellOrderResponse sellOrderResponse = new SellOrderResponse();
            try
            {
                sellOrderResponse = await _sellOrdersService.CreateSellOrder(orderRequest);
            }
            catch (ArgumentException ex)
            {
                TempData["Errors"] = ex.Message;
                return RedirectToAction(nameof(Index), new { stockSymbol = orderRequest.StockSymbol, succeeded = false });
            }
            TempData["Succeeded"] = 1;

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        public async Task<IActionResult> Orders() 
        {
            _logger.LogInformation($"{nameof(Orders)} IAction method of {nameof(TradeController)} controller");

            // List<BuyOrderResponse> buyOrders = await _buyOrdersService.GetBuyOrders();
            // List<SellOrderResponse> sellOrders = await _sellOrdersService.GetSellOrders();

            Guid userID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<BuyOrderResponse> userBuyOrders = await _buyOrdersService.GetUserBuyOrders(userID);
            List<SellOrderResponse> userSellOrders = await _sellOrdersService.GetUserSellOrders(userID);

            Orders orders = new Orders() {
                BuyOrders = userBuyOrders,
                SellOrders = userSellOrders
            };

            ViewBag.TradingOptions = _tradingOptions;
            ViewBag.Succeeded = TempData["Succeeded"];

            return View(orders);
        }

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            // List<IOrderResponse> orders = new List<IOrderResponse>();
            // orders.AddRange(await _buyOrdersService.GetBuyOrders());
            // orders.AddRange(await _sellOrdersService.GetSellOrders());
            // orders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            Guid userID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<IOrderResponse> orders = new List<IOrderResponse>();
            orders.AddRange(await _buyOrdersService.GetUserBuyOrders(userID));
            orders.AddRange(await _sellOrdersService.GetUserSellOrders(userID));
            orders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            ViewBag.TradingOptions = _tradingOptions;

            // return view as PDF
            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}