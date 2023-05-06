using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.DTO
{
    /// <summary>
    /// DTO class represents a UserStock after placing buy order - can be used as return type for services
    /// </summary>
    public class UserStockResponse
    { 
        /// <summary>
        /// The unique id of user
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// The unique symbol of stock
        /// </summary>
        public string StockSymbol { get; set; }

        /// <summary>
        /// The company name of stock
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// The amount of stocks placed
        /// </summary>
        public uint Quantity { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            UserStockResponse userStockResponse = (UserStockResponse)obj;

            return (this.UserID == userStockResponse.UserID && this.StockSymbol == userStockResponse.StockSymbol && this.StockName == userStockResponse.StockName && this.Quantity == userStockResponse.Quantity);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Extensions to convert from UserStock object to DTO class object (UserStockResponse)
    /// </summary>
    public static class UserStockExtensions
    {
        /// <summary>
        /// Create a new UserStockResponse object based on UserStock object
        /// </summary>
        /// <param name="userStock">UserStock object to convert</param>
        /// <returns>A new UserStockResponse object</returns>
        public static UserStockResponse ToUserStockResponse(this UserStock userStock)
        {
            UserStockResponse newUserStockResponse = new UserStockResponse() {
                UserID = userStock.UserID,
                StockSymbol = userStock.StockSymbol,
                StockName = userStock.StockName,
                Quantity = userStock.Quantity
            };

            return newUserStockResponse;
        }
    }
}