using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StocksApp.Core.Domain.IdentityEntities;
using StocksApp.Core.DTO;

namespace StocksApp.Core.Domain.Entities
{
    /// <summary>
    /// Represents a stock of user
    /// </summary>
    public class UserStock
    {
        /// <summary>
        /// Unique id of user
        /// </summary>
        [ForeignKey(nameof(ApplicationUser.Id))]
        public Guid UserID { get; set; }

        /// <summary>
        /// The unique symbol of stock
        /// </summary>
        public string StockSymbol { get; set; }

        /// <summary>
        /// The company name of stock
        /// </summary>
        /// <value></value>
        public string StockName { get; set; }

        /// <summary>
        /// The amount of shares placed
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        /// Stock price of last placed order
        /// </summary>
        public double LastPrice { get; set; }
    }

    /// <summary>
    /// Extensions to convert from DTO class object (UserStockRequest) to nenw object of UserStock type
    /// </summary>
    public static class UserStockRequestExtensions
    {
        /// <summary>
        /// Create a new UserStock object based on the DTO object (UserStockRequest)
        /// </summary>
        /// <param name="userStockRequest">UserStockRequest to convert</param>
        /// <returns>An object of UserStock type</returns>
        public static UserStock ToUserStock(this UserStockRequest userStockRequest)
        {
            UserStock newUserStock = new UserStock() {
                UserID = userStockRequest.UserID,
                StockSymbol = userStockRequest.StockSymbol,
                StockName = userStockRequest.StockName,
                Quantity = userStockRequest.Quantity,
                LastPrice = userStockRequest.LastPrice
            };

            return newUserStock;
        }
    }
}