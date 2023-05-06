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
using StocksApp.Core.ServiceContracts.UserStockServices;
using StocksApp.Core.Services.UserStockServices;
using Xunit;
using Xunit.Abstractions;

namespace StocksApp.ServiceTests
{
    public class UserStockServiceTests
    {
        private readonly IUserStockRepository _userStockRepository;
        private readonly Mock<IUserStockRepository> _userStockRepositoryMock;
        private readonly IUserStockAddService _userStockAddService;
        private readonly ILogger<UserStockAddService> _userStockAddServiceLogger;
        private readonly Mock<ILogger<UserStockAddService>> _userStockAddServiceLoggerMock;
        private readonly IUserStockGetService _userStockGetService;
        private readonly ILogger<UserStockGetService> _userStockGetServiceLogger;
        private readonly Mock<ILogger<UserStockGetService>> _userStockGetServiceLoggerMock;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public UserStockServiceTests(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();

            // Repository mock
            _userStockRepositoryMock = new Mock<IUserStockRepository>();
            _userStockRepository = _userStockRepositoryMock.Object;

            // Logger mock
            _userStockAddServiceLoggerMock = new Mock<ILogger<UserStockAddService>>();
            _userStockAddServiceLogger = _userStockAddServiceLoggerMock.Object;
            _userStockGetServiceLoggerMock = new Mock<ILogger<UserStockGetService>>();
            _userStockGetServiceLogger = _userStockGetServiceLoggerMock.Object;

            // DiagnosticContext mock
            var diagnosticContextMock = new Mock<IDiagnosticContext>();
            IDiagnosticContext diagnosticContext = diagnosticContextMock.Object;

            // Service mock
            _userStockAddService = new UserStockAddService(_userStockRepository, _userStockAddServiceLogger, diagnosticContext);
            _userStockGetService = new UserStockGetService(_userStockRepository, _userStockGetServiceLogger, diagnosticContext);

            _testOutputHelper = testOutputHelper;
        }

        #region UserStockAddService

        [Fact]
        public async Task UserStockAddService_NullUserStockRequest_ToBeArgumentNullException()
        {
            // Arrange
            UserStockRequest? userStockRequest = null;
        
            // Acts
            Func<Task> action = async () => {
                await _userStockAddService.AddUserStock(userStockRequest);
            };
        
            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UserStockAddService_ProperUserStock_ToBeSuccessful()
        {
            // Arrange
            UserStockRequest userStockRequest = new UserStockRequest() {
                UserID = Guid.NewGuid(),
                StockSymbol = "APPL",
                StockName = "Apple Inc",
                Quantity = 15
            };
            UserStock userStock = userStockRequest.ToUserStock();
            UserStockResponse userStockResponse = userStock.ToUserStockResponse();

            // Mock repository
            _userStockRepositoryMock.Setup(temp => temp.AddUserStock(It.IsAny<UserStock>())).ReturnsAsync(userStock);
        
            // Acts
            UserStockResponse? userStockResponseFromAdd = await _userStockAddService.AddUserStock(userStockRequest);
        
            // Assert
            userStockResponseFromAdd.Should().NotBeNull(); 
            userStockResponse.Should().BeEquivalentTo(userStockResponseFromAdd);
        }

        #endregion

    }
}