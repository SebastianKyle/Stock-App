using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.DTO
{
    public interface IOrderResponse
    {
        string StockSymbol { get; set; }
        string StockName { get; set; }

        DateTime DateAndTimeOfOrder { get; set; }

        uint Quantity { get; set; }

        double Price { get; set; }

        OrderType TypeOfOrder { get; }

        double TradeAmount { get; set; }     
    }

    public enum OrderType
    {
        BuyOrder, SellOrder
    }
}