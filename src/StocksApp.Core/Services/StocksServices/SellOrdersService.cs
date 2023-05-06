using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.DTO;
using StocksApp.Core.Helpers;
using StocksApp.Core.ServiceContracts.StocksServices;
using StocksApp.Core.ServiceContracts.UserStockServices;

namespace StocksApp.Core.Services.StocksServices
{
    public class SellOrdersService : ISellOrdersService
    {
        private readonly IStocksRepository _stocksRepository;
        private readonly IUserStockGetService _userStockGetService;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        private readonly ILogger<SellOrdersService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public SellOrdersService(IStocksRepository stocksRepository, IUserStockGetService userStockGetService, IAccountBalanceRepository accountBalanceRepository, ILogger<SellOrdersService> logger, IDiagnosticContext diagnosticContext) 
        {
            _stocksRepository = stocksRepository;
            _userStockGetService = userStockGetService;
            _accountBalanceRepository = accountBalanceRepository;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            _logger.LogInformation($"{nameof(CreateSellOrder)} method of {nameof(SellOrdersService)}");

            if (sellOrderRequest == null) 
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            // Model validation
            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();

            // Validate if user has enough shares to sell
            // List<BuyOrder> buyOrders = await _stocksRepository.GetUserBuyOrders(sellOrderRequest.UserID);
            // List<BuyOrder> matchingBuyOrders = buyOrders.Where(temp => temp.StockSymbol == sellOrderRequest.StockSymbol).ToList();
            List<UserStockResponse> userStocks = await _userStockGetService.GetUserStocks(sellOrderRequest.UserID);
            UserStockResponse? matchingStock = userStocks.FirstOrDefault(temp => temp.StockSymbol == sellOrderRequest.StockSymbol);
            // long sharesAmount = matchingBuyOrders.Sum(temp => temp.Quantity);
            long sharesAmount = matchingStock == null ? 0 : matchingStock.Quantity;

            if (sellOrderRequest.Quantity > sharesAmount)
            {
                throw new ArgumentException($"You don't have enough {sellOrderRequest.StockName} shares to sell.");
            }

            SellOrder sellOrderFromRepo = await _stocksRepository.CreateSellOrder(sellOrder);

            // Increase account balance
            double tradeAmount = sellOrderRequest.Quantity * sellOrderRequest.Price;
            UserAccountBalance matchingUser = await _accountBalanceRepository.GetAccountBalance(sellOrderRequest.UserID); 
            matchingUser.AccountBalance += tradeAmount;
            await _accountBalanceRepository.UpdateAccountBalance(matchingUser);

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            _logger.LogInformation($"{nameof(GetSellOrders)} method of {nameof(SellOrdersService)}");

            List<SellOrder> sellOrders = await _stocksRepository.GetSellOrder();

            _diagnosticContext.Set("Sell orders", sellOrders);

            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetUserSellOrders(Guid userID)
        {
            _logger.LogInformation($"{nameof(GetUserSellOrders)} method of {nameof(SellOrdersService)}");

            List<SellOrder> userSellOrders = await _stocksRepository.GetUserSellOrders(userID);

            _diagnosticContext.Set("User sell orders", userSellOrders);

            return userSellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }
  }
}