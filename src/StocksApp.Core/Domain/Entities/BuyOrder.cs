using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.IdentityEntities;

namespace StocksApp.Core.Domain.Entities
{
    /// <summary>
    /// Represents the buy order to buy the stocks (shares)
    /// </summary>
    public class BuyOrder
    {
        /// <summary>
        /// The unique ID of the user that placed the order
        /// </summary>
        [ForeignKey(nameof(ApplicationUser.Id))]
        public Guid UserID { get; set; }

        /// <summary>
        /// The unique ID of buy order
        /// </summary>
        [Key]
        public Guid BuyOrderID { get; set; } 

        /// <summary>
        /// The unique symbol of stock
        /// </summary>
        [Required(ErrorMessage = "Stock symbol can't be null or empty")]
        public string StockSymbol { get; set; }

        /// <summary>
        /// The company name of stock
        /// </summary>
        [Required(ErrorMessage = "Stock name can't be null or empty")]
        public string StockName { get; set; }

        /// <summary>
        /// Date and time of order
        /// </summary>
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// The number of stocks (shares) to buy
        /// </summary>
        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in a single order. Minumum is 1")]
        public uint Quantity { get; set; }

        /// <summary>
        /// The price of each stock
        /// </summary>
        [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1")]
        public double Price { get; set; }

        public override string ToString()
        {
            return $"Buy Order Id: {BuyOrderID},\n Stock Symbol: {StockSymbol},\n Stock name: {StockName},\n Date and time of buy order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm:ss tt")},\n Quantity: {Quantity},\n Buy Price: {Price}";
        }
    }
}