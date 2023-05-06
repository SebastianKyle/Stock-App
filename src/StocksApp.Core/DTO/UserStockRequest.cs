using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.DTO
{
    /// <summary>
    /// DTO class represents a UserStock after placing buy order - can be used for inserting / updating
    /// </summary>
    public class UserStockRequest
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
        /// The amount of shares placed
        /// </summary>
        public uint Quantity { get; set; }
    }
}