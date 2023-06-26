using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using StocksApp.Core.DTO;
using StocksApp.UI.Controllers;
using StocksApp.UI.Models;

namespace StocksApp.UI.Filters.ActionFilters
{
  public class CreateOrderActionFilter : IAsyncActionFilter
  {
    private readonly ILogger<CreateOrderActionFilter> _logger;

    public CreateOrderActionFilter(ILogger<CreateOrderActionFilter> logger)
    {
      _logger = logger;
    }

    public async Task OnActionExecutionAsync(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context, ActionExecutionDelegate next)
    {
      _logger.LogInformation("{FilterClass}.{Method} action filter", nameof(CreateOrderActionFilter), nameof(OnActionExecutionAsync));

      // Before logic

      if (context.Controller is TradeController tradeController)
      {
        var orderRequest = context.ActionArguments["orderRequest"] as IOrderRequest;

        if (orderRequest != null)
        {
          // Update date and time of order
          orderRequest.DateAndTimeOfOrder = DateTime.Now;

          // Re-validate
          tradeController.ModelState.Clear();
          tradeController.TryValidateModel(orderRequest);

          if (!tradeController.ModelState.IsValid)
          {
            tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(temp => temp.Errors).Select(v => v.ErrorMessage).ToList();

            StockTrade stockTrade = new StockTrade()
            {
              StockSymbol = orderRequest.StockSymbol,
              StockName = orderRequest.StockName,
              Quantity = orderRequest.Quantity,
              Price = orderRequest.Price
            };

            context.Result = tradeController.View(nameof(TradeController.Index), stockTrade);
          }
          else
          {
            await next(); // invoke subsequent filter or action method
          }
        }
        else
        {
          await next();
        }
      }
      else
      {
        await next();
      }

      // After logic

    }
  }
}