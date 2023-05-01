using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.Entities.CustomValidators;

namespace StocksApp.Core.DTO
{
    /// <summary>
    /// DTO class represents a sell order - can be used while inserting/updating
    /// </summary>
    public class SellOrderRequest : IOrderRequest
    {
		/// <summary>
		/// The unique ID of the user that placed the order
		/// </summary>
		public Guid UserID { get; set; }

        /// <summary>
        /// The unique symbol of stock
        /// </summary>
        [Required(ErrorMessage = "Stock symbol can not be null or empty")]
        public string StockSymbol { get; set; }

        /// <summary>
        /// The company name of stock
        /// </summary>
        [Required(ErrorMessage = "Stock name can not be null or empty")]
        public string StockName { get; set; }

        /// <summary>
        /// Date and time of sell order when it's placed by user
        /// </summary>
        [DateRangeValidator]
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// The amount of stocks (shares) to sell 
        /// </summary>
        [Range(1, 100000, ErrorMessage = "You can sell maximum of 100000 shares in a single order. Minumum is 1")]
        public uint Quantity { get; set; }

        /// <summary>
        /// The price of each stock (share)
        /// </summary>
        [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1")]
        public double Price { get; set; } 
    }

    /// <summary>
    /// SellOrderRequest extension
    /// </summary>
    public static class SellOrderRequestExtension
    {
        /// <summary>
        /// Convert a DTO class (SellOrderRequest) object to a SellOrder object
        /// </summary>
        /// <param name="sellOrderRequest">SellOrderRequest object to convert</param>
        /// <returns>The converted SellOrder object</returns>
        public static SellOrder ToSellOrder(this SellOrderRequest sellOrderRequest)
        {
            SellOrder sellOrder = new SellOrder() {
                UserID = sellOrderRequest.UserID,
                StockSymbol = sellOrderRequest.StockSymbol,
                StockName = sellOrderRequest.StockName,
                DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
                Quantity = sellOrderRequest.Quantity,
                Price = sellOrderRequest.Price 
            };

            return sellOrder;
        }
    }
}