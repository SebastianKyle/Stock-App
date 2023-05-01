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

namespace StocksApp.Core.Services.StocksServices
{
  public class BuyOrdersService : IBuyOrdersService
  {
    private readonly IStocksRepository _stocksRepository;
    private readonly ILogger<BuyOrdersService> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public BuyOrdersService(IStocksRepository stocksRepository, ILogger<BuyOrdersService> logger, IDiagnosticContext diagnosticContext)
    {
      _stocksRepository = stocksRepository;
      _logger = logger;
      _diagnosticContext = diagnosticContext;
    }

    public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
      _logger.LogInformation($"{nameof(CreateBuyOrder)} method of {nameof(BuyOrdersService)}");

      // Buy Order request can not be null
      if (buyOrderRequest == null)
      {
        throw new ArgumentNullException(nameof(buyOrderRequest));
      }

      // Model validation
      ValidationHelper.ModelValidation(buyOrderRequest);

      BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
      buyOrder.BuyOrderID = Guid.NewGuid();

      BuyOrder buyOrderFromRepo = await _stocksRepository.CreateBuyOrder(buyOrder);

      BuyOrderResponse buyOrderResponse = buyOrder.ToBuyOrderResponse();

      return buyOrderResponse;
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {
      _logger.LogInformation($"{nameof(GetBuyOrders)} method of {nameof(BuyOrdersService)}");

      List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();

      _diagnosticContext.Set("Buy orders", buyOrders);

      return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
    }

    public async Task<List<BuyOrderResponse>> GetUserBuyOrders(Guid userID)
    {
      _logger.LogInformation($"{nameof(GetUserBuyOrders)} method of {nameof(BuyOrdersService)}");

      List<BuyOrder> userBuyOrders = await _stocksRepository.GetUserBuyOrders(userID);

      _diagnosticContext.Set("User buy orders", userBuyOrders);

      return userBuyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
    }
  }
}