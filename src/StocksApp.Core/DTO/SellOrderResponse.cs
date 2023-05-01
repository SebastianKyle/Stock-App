using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.DTO
{
    public class SellOrderResponse : IOrderResponse
    {
        /// <summary>
        /// The unique ID of the user that placed the order
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// The unique ID of sell order
        /// </summary>
        public Guid SellOrderID { get; set; } 

        /// <summary>
        /// The unique symbol of stock
        /// </summary>
        public string StockSymbol { get; set; }

        /// <summary>
        /// The Company name of stock
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// Date and time of order when it's placed by user
        /// </summary>
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// The amount of stocks (shares) to sell
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        /// The price of each stock
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The total price of sell order
        /// </summary>
        public double TradeAmount { get; set; }

        /// <summary>
        /// The order type (buy order / sell order)
        /// </summary>
        public OrderType TypeOfOrder => OrderType.SellOrder;

        /// <summary>
        /// Check whether the current object and other SellOrderResponse object match
        /// </summary>
        /// <param name="obj">Other SellOrderResponse object to compare</param>
        /// <returns>True or false determines whether current object and other one match</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            SellOrderResponse sellOrderResponse = (SellOrderResponse)obj;

            return (this.SellOrderID == sellOrderResponse.SellOrderID) && (this.StockSymbol == sellOrderResponse.StockSymbol) && (this.StockName == sellOrderResponse.StockName) && (this.DateAndTimeOfOrder == sellOrderResponse.DateAndTimeOfOrder) && (this.Quantity == sellOrderResponse.Quantity) && (this.Price == sellOrderResponse.Price) && (this.TradeAmount == sellOrderResponse.TradeAmount);
        }

        /// <summary>
        /// Converts the current SellOrderResponse object into string which specify all the values and properties
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $" Sell Order Id: {SellOrderID},\n Stock Symbol: {StockSymbol},\n Stock name: {StockName},\n Date and time of sell order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm:ss tt")},\n Quantity: {Quantity},\n Buy Price: {Price},\n Trade amount: {TradeAmount}";
        } 
    }

    /// <summary>
    /// SellOrder extension
    /// </summary>
    public static class SellOrderExtension
    {
        /// <summary>
        /// Convert the current SellOrder object to SellOrderResponse object
        /// </summary>
        /// <param name="sellOrder">SellOrder object to convert</param>
        /// <returns>The converted SellOrderResponse object</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            SellOrderResponse sellOrderResponse = new SellOrderResponse() {
                UserID = sellOrder.UserID,
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price 
            };

            return sellOrderResponse;
        }
    }
}