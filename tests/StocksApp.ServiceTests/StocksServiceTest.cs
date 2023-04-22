using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.DTO;
using StocksApp.Core.ServiceContracts.StocksServices;
using StocksApp.Core.Services.StocksServices;
using Xunit;
using Xunit.Abstractions;

namespace StocksApp.ServiceTests
{
    public class StocksServiceTest
    {
        private readonly IBuyOrdersService _buyOrdersService;
        private readonly ISellOrdersService _sellOrdersService;
        private readonly IStocksRepository _stocksRepository;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly ILogger<BuyOrdersService> _buyOrdersServiceLogger;
        private readonly Mock<ILogger<BuyOrdersService>> _buyOrdersServiceLoggerMock;
        private readonly ILogger<SellOrdersService> _sellOrdersServiceLogger;
        private readonly Mock<ILogger<SellOrdersService>> _sellOrdersServiceLoggerMock;
        private readonly IFixture _fixture;

        public StocksServiceTest(ITestOutputHelper testOutputHelper) 
        {
            _fixture = new Fixture();
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object; // create mock repository object

            // Mock logger
            _buyOrdersServiceLoggerMock = new Mock<ILogger<BuyOrdersService>>();
            _buyOrdersServiceLogger = _buyOrdersServiceLoggerMock.Object;

            _sellOrdersServiceLoggerMock = new Mock<ILogger<SellOrdersService>>();
            _sellOrdersServiceLogger = _sellOrdersServiceLoggerMock.Object;

            // Mock diagnosticContext
            var diagnosticContextMock = new Mock<IDiagnosticContext>();
            IDiagnosticContext diagnosticContext = diagnosticContextMock.Object;

            _testOutputHelper = testOutputHelper;

            _buyOrdersService = new BuyOrdersService(_stocksRepository, _buyOrdersServiceLogger, diagnosticContext);
            _sellOrdersService = new SellOrdersService(_stocksRepository, _sellOrdersServiceLogger, diagnosticContext);
        }

        #region CreateBuyOrder

        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null; 

            // Mock 
            BuyOrder buyOrder = _fixture.Build<BuyOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Assert
            // await Assert.ThrowsAsync<ArgumentNullException>(async () => {
            //     // Acts
            //     await _stocksService.CreateBuyOrder(buyOrderRequest); 
            // });

            // Fluent Assertion
            Func<Task> action = async () => {
                await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory] // With Theory attribute, we can pass params into our test method
        [InlineData(0)]
        public async Task CreateBuyOrder_ZeroQuantity_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                       .With(temp => temp.Quantity, buyOrderQuantity)
                                                       .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyorder_OverflowQuantity_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                       .With(temp => temp.Quantity, buyOrderQuantity)
                                                       .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Assert
            // await Assert.ThrowsAsync<ArgumentException>(async () => {
            //     // Acts
            //     await _stocksService.CreateBuyOrder(buyOrderRequest);
            // }); 

            // Fluent Assertion
            Func<Task> action = async () => {
                await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>(); 
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateBuyorder_ZeroPrice_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                       .With(temp => temp.Price, buyOrderPrice)
                                                       .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // FLuent Assertion
            Func<Task> action = async () => {
                await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(10001)]
        public async Task CreateBuyorder_OverflowPrice_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                       .With(temp => temp.Price, buyOrderPrice)
                                                       .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyorder_NullStockSymbol_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                       .With(temp => temp.StockSymbol, null as string)
                                                       .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyorder_InvalidDateAndTimeOfOrder_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                       .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
                                                       .Create();

            // Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyorder_ProperOrder_ToBeSuccessful()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() {
                StockSymbol = "MSFT",
                StockName = "Microsoft Corp",
                DateAndTimeOfOrder = Convert.ToDateTime("2020-12-01"),
                Quantity = 18792,
                Price = 287.7
            };
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            // Acts
            BuyOrderResponse buyOrderResponseFromCreate = await _buyOrdersService.CreateBuyOrder(buyOrderRequest); 
            BuyOrderResponse buyOrderResponseExpected = buyOrder.ToBuyOrderResponse();
            buyOrderResponseExpected.BuyOrderID = buyOrderResponseFromCreate.BuyOrderID;

            _testOutputHelper.WriteLine("\n Expected output: ");
            _testOutputHelper.WriteLine(buyOrderResponseExpected.ToString());

            _testOutputHelper.WriteLine("\n Actual output: ");
            _testOutputHelper.WriteLine(buyOrderResponseFromCreate.ToString());

            // Fluent assertion
            buyOrderResponseFromCreate.BuyOrderID.Should().NotBe(Guid.Empty);
            buyOrderResponseFromCreate.Should().BeEquivalentTo(buyOrderResponseExpected);
        }

        #endregion

        #region CreateSellOrder

        [Fact]
        public async Task CreateSellOrder_NullRequest_ToBeArgumentNullException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Mock
            SellOrder sellOrder = _fixture.Build<SellOrder>().Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                // Acts
                await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_ZeroQuantity_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                         .With(temp => temp.Quantity, sellOrderQuantity)
                                                         .Create();

            // Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                // Acts
                await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_OverflowQuantity_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                         .With(temp => temp.Quantity, sellOrderQuantity)
                                                         .Create();

            // Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                // Acts
                await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_ZeroPrice_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                         .With(temp => temp.Price, sellOrderPrice)
                                                         .Create();

            // Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                // Acts
                await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_OverflowPrice_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                         .With(temp => temp.Price, sellOrderPrice)
                                                         .Create();

            // Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                // Acts
                await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_NullStockSymbol_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                         .With(temp => temp.StockSymbol, null as string)
                                                         .Create();

            // Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                // Acts
                await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_InvalidDateAndTimeOfOrder_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                         .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
                                                         .Create();

            // Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Fluent Assertion
            Func<Task> action = async () => {
                // Acts
                await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_ProperRequest_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() {
                StockSymbol = "MSFT",
                StockName = "Microsoft Corp",
                DateAndTimeOfOrder = Convert.ToDateTime("2020-12-01"),
                Quantity = 18792,
                Price = 216.93
            };
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            // Acts
            SellOrderResponse sellOrderResponseFromCreate = await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            SellOrderResponse sellOrderResponseExpected = sellOrder.ToSellOrderResponse();
            sellOrderResponseExpected.SellOrderID = sellOrderResponseFromCreate.SellOrderID;

            // Fluent Assertion
            sellOrderResponseFromCreate.SellOrderID.Should().NotBe(Guid.Empty);
            sellOrderResponseFromCreate.Should().BeEquivalentTo(sellOrderResponseExpected);
        }

        #endregion

        #region GetAllBuyOrders

        [Fact]
        public async Task GetAllBuyOrders_DefaultList_ToBeEmpty()
        {
            // Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders);

            // Acts
            List<BuyOrderResponse> buyOrdersFromGet = await _buyOrdersService.GetBuyOrders();

            // Assert
            Assert.Empty(buyOrdersFromGet);
        }

        [Fact]
        public async Task GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            // Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>() {
                new BuyOrder() {
                    BuyOrderID = Guid.NewGuid(),
                    StockSymbol = "MSFT", 
                    StockName = "Microsoft", 
                    Price = 1, 
                    Quantity = 1, 
                    DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
                },
                new BuyOrder() {
                    BuyOrderID = Guid.NewGuid(),
                    StockSymbol = "MSFT", 
                    StockName = "Microsoft", 
                    Price = 1, 
                    Quantity = 1, 
                    DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
                }
            };

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders);

            List<BuyOrderResponse> buyOrderResponsesListExpected = buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();

            // Acts
            List<BuyOrderResponse> buyOrderResponseListFromGet = await _buyOrdersService.GetBuyOrders();

            // Assert
            // foreach (BuyOrderResponse order in buyOrderResponsesListExpected)
            // {
            //     // Fluent Assertion
            //     buyOrderResponseListFromGet.Should().Contain(order);
            // }

            // Fluent Assertion
            buyOrderResponseListFromGet.Should().BeEquivalentTo(buyOrderResponsesListExpected);
        }

        #endregion

        #region GetAllSellOrders

        [Fact]
        public async Task GetSellOrders_DefaultList_ToBeEmpty()
        {
            // Arrange
            List<SellOrder> sellOrders = new List<SellOrder>();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrder()).ReturnsAsync(sellOrders);

            // Acts
            List<SellOrderResponse> sellOrderResponses = await _sellOrdersService.GetSellOrders();

            // Fluent Assertion
            sellOrderResponses.Should().BeEmpty();
        }

        [Fact]
        public async Task GetSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            // Arrange
            List<SellOrder> sellOrders = new List<SellOrder>() {
                new SellOrder() {
                    SellOrderID = Guid.NewGuid(),
                    StockSymbol = "MSFT", 
                    StockName = "Microsoft", 
                    Price = 1, 
                    Quantity = 1, 
                    DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
                },
                new SellOrder() {
                    SellOrderID = Guid.NewGuid(),
                    StockSymbol = "MSFT", 
                    StockName = "Microsoft", 
                    Price = 1, 
                    Quantity = 1, 
                    DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
                }
            };
            List<SellOrderResponse> sellOrderResponsesListExpected = sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrder()).ReturnsAsync(sellOrders);

            // Acts
            List<SellOrderResponse> sellOrderResponsesListFromGet = await _sellOrdersService.GetSellOrders();

            // Fluent Assertion
            sellOrderResponsesListFromGet.Should().BeEquivalentTo(sellOrderResponsesListExpected);
        }

        #endregion

    }
}