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
    /// DTO that represents a buy order to purchase the stocks - can be used while inserting / updating
    /// </summary>
    public class BuyOrderRequest : IOrderRequest
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
        /// Date and time of order, when it's placed by users
        /// </summary>
        [DateRangeValidator]
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// The number of stocks (shares) to buy
        /// </summary>
        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in a single order. Minimum is 1")]
        public uint Quantity { get; set; }

        /// <summary>
        /// The price of each stock (share)
        /// </summary>
        [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minumum is 1")]
        public double Price { get; set; } 
    }

    /// <summary>
    /// Extension to convert from DTO class (BuyOrderRequest) to a new object of BuyOrder type
    /// </summary>
    public static class BuyOrderRequestExtentions
    {
        /// <summary>
        /// Create a new object of BuyOrder type based on the BuyOrderRequest object
        /// </summary>
        /// <param name="buyOrderRequest">BuyOrderRequest object to convert</param>
        /// <returns>An object of BuyOrder type</returns>
        public static BuyOrder ToBuyOrder(this BuyOrderRequest buyOrderRequest)
        {
            BuyOrder buyOrder = new BuyOrder() {
                UserID = buyOrderRequest.UserID,
                StockSymbol = buyOrderRequest.StockSymbol,
                StockName = buyOrderRequest.StockName,
                DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
                Quantity = buyOrderRequest.Quantity,
                Price = buyOrderRequest.Price
            };

            return buyOrder;
        }
    }
}