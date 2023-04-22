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
    public class SellOrdersService : ISellOrdersService
    {
        private readonly IStocksRepository _stocksRepository;
        private readonly ILogger<SellOrdersService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public SellOrdersService(IStocksRepository stocksRepository, ILogger<SellOrdersService> logger, IDiagnosticContext diagnosticContext) 
        {
            _stocksRepository = stocksRepository;
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

        SellOrder sellOrderFromRepo = await _stocksRepository.CreateSellOrder(sellOrder);

        return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            _logger.LogInformation($"{nameof(GetSellOrders)} method of {nameof(SellOrdersService)}");

            List<SellOrder> sellOrders = await _stocksRepository.GetSellOrder();

            _diagnosticContext.Set("Sell orders", sellOrders);

            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        } 
    }
}