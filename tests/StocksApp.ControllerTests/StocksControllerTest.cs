using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StocksApp.Core.ServiceContracts.FinnhubServices;
using StocksApp.Core.ServiceContracts.UserStockServices;
using StocksApp.UI;
using StocksApp.UI.Controllers;
using StocksApp.UI.Models;
using Xunit;

namespace StocksApp.ControllerTests
{
    public class StocksControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubStocksService _finnhubStocksService;
        private readonly Mock<IFinnhubStocksService> _finnhubStocksServiceMock;
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly Mock<IFinnhubCompanyProfileService> _finnhubCompanyProfileServiceMock;
        private readonly IUserStockGetService _userStockGetService;
        private readonly Mock<IUserStockGetService> _userStockGetServiceMock;
        private readonly ILogger<StocksController> _logger;
        private readonly Mock<ILogger<StocksController>> _mockLogger;
        private readonly Fixture _fixture;

        public StocksControllerTest()
        {
            _finnhubStocksServiceMock = new Mock<IFinnhubStocksService>();
            _finnhubStocksService = _finnhubStocksServiceMock.Object;

            _finnhubCompanyProfileServiceMock = new Mock<IFinnhubCompanyProfileService>();
            _finnhubCompanyProfileService = _finnhubCompanyProfileServiceMock.Object;

            _userStockGetServiceMock = new Mock<IUserStockGetService>();
            _userStockGetService = _userStockGetServiceMock.Object;

            _mockLogger = new Mock<ILogger<StocksController>>(); 
            _logger = _mockLogger.Object;

            _fixture = new Fixture();
        }

        #region Explore

        [Fact]
        public async Task Explore_StockIsNull_ShouldReturnExploreViewWithStockList()
        {
            // Arrange
            IOptions<TradingOptions> tradingOptions = Options.Create(new TradingOptions() {
                DefaultOrderQuantity = 100,
                Top25PopularStocks = "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO"
            });

            StocksController stocksController = new StocksController(tradingOptions, _configuration, _finnhubStocksService, _finnhubCompanyProfileService, _userStockGetService, _logger);

            List<Dictionary<string, string>>? stocksDict = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(@"[{'currency':'USD','description':'APPLE INC','displaySymbol':'AAPL','figi':'BBG000B9XRY4','isin':null,'mic':'XNAS','shareClassFIGI':'BBG001S5N8V8','symbol':'AAPL','symbol2':'','type':'Common Stock'}, {'currency':'USD','description':'MICROSOFT CORP','displaySymbol':'MSFT','figi':'BBG000BPH459','isin':null,'mic':'XNAS','shareClassFIGI':'BBG001S5TD05','symbol':'MSFT','symbol2':'','type':'Common Stock'}, {'currency':'USD','description':'AMAZON.COM INC','displaySymbol':'AMZN','figi':'BBG000BVPV84','isin':null,'mic':'XNAS','shareClassFIGI':'BBG001S5PQL7','symbol':'AMZN','symbol2':'','type':'Common Stock'}, {'currency':'USD','description':'TESLA INC','displaySymbol':'TSLA','figi':'BBG000N9MNX3','isin':null,'mic':'XNAS','shareClassFIGI':'BBG001SQKGD7','symbol':'TSLA','symbol2':'','type':'Common Stock'}, {'currency':'USD','description':'ALPHABET INC-CL A','displaySymbol':'GOOGL','figi':'BBG009S39JX6','isin':null,'mic':'XNAS','shareClassFIGI':'BBG009S39JY5','symbol':'GOOGL','symbol2':'','type':'Common Stock'}]");

            // Mock repository
            _finnhubStocksServiceMock.Setup(temp => temp.GetStocks()).ReturnsAsync(stocksDict);

            List<Stock> stocksListExpected = stocksDict.Select(temp => new Stock() {
                StockSymbol = Convert.ToString(temp["symbol"]),
                StockName = Convert.ToString(temp["description"])
            }).ToList();
        
            // Acts
            IActionResult result = await stocksController.Explore(null, false);
        
            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(stocksListExpected);
        }

        #endregion
    }
}