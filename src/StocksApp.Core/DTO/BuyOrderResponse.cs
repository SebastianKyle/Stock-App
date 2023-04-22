using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.DTO
{
    public class BuyOrderResponse : IOrderResponse
    {
        /// <summary>
        /// The unique ID of buy order
        /// </summary>
        public Guid BuyOrderID { get; set; }                

        /// <summary>
        /// The unique symbol of stock
        /// </summary>
        public string StockSymbol { get; set; }

        /// <summary>
        /// The company name of stock
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// Date and time of order when it's placed by user
        /// </summary>
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// The number of stocks (shares) to buy
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        /// The price of each stock (share)
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The total price of buy order
        /// </summary>
        public double TradeAmount { get; set; }

        /// <summary>
        /// The order type (buy order / sell order)
        /// </summary>
        public OrderType TypeOfOrder => OrderType.BuyOrder;

        /// <summary>
        /// Check of the current object and other object match
        /// </summary>
        /// <param name="obj">Other BuyOrderResponse object to compare</param>
        /// <returns>True or false determines whether current object and other one match</returns>
        public override bool Equals(object obj)
        { 
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here

            BuyOrderResponse buyOrderResponse = (BuyOrderResponse) obj;

            return (this.BuyOrderID == buyOrderResponse.BuyOrderID) && (this.StockSymbol == buyOrderResponse.StockSymbol) && (this.StockName == buyOrderResponse.StockName) && (this.DateAndTimeOfOrder == buyOrderResponse.DateAndTimeOfOrder) && (this.Quantity == buyOrderResponse.Quantity) && (this.TradeAmount == buyOrderResponse.TradeAmount);
        }

        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

        /// <summary>
        /// Converts the current BuyOrderResponse object into string which specify the values and all properties
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $" Buy Order Id: {BuyOrderID},\n Stock Symbol: {StockSymbol},\n Stock name: {StockName},\n Date and time of buy order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm:ss tt")},\n Quantity: {Quantity},\n Buy Price: {Price},\n Trade amount: {TradeAmount}";
        } 
    }

    /// <summary>
    /// BuyOrder extension
    /// </summary>
    public static class BuyOrderExtension
    {
        /// <summary>
        /// Convert the current BuyOrder object into object of DTO class (BuyOrderResponse)
        /// </summary>
        /// <param name="buyOrder">BuyOrder object to convert</param>
        /// <returns>The converted BuyOrderResponse object</returns>
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            BuyOrderResponse buyOrderResponse = new BuyOrderResponse() {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = buyOrder.Quantity * buyOrder.Price
            };

            return buyOrderResponse;
        }
    }
}