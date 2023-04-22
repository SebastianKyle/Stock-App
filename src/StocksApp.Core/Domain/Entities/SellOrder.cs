using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities.CustomValidators;

namespace StocksApp.Core.Domain.Entities
{
	/// <summary>
	/// Represents the sell order to sell the stocks
	/// </summary>
	public class SellOrder
	{
		/// <summary>
		/// The unique ID of the sell order
		/// </summary>
		[Key]
		public Guid SellOrderID { get; set; }

		/// <summary>
		/// The unique symbol of the stock
		/// </summary>
		public string StockSymbol { get; set; }

		/// <summary>
		/// The company name of stock
		/// </summary>
		[Required(ErrorMessage = "Stock name can't be null or empty")]
		public string StockName { get; set; }

		/// <summary>
		/// Date and time of order
		/// </summary>
		[DateRangeValidator]
		public DateTime DateAndTimeOfOrder { get; set; }

		/// <summary>
		/// The number of stocks to sell
		/// </summary>
		[Range(1, 100000, ErrorMessage = "You can sell maximum of 100000 shares in single order. Minimum is 1")]
		public uint Quantity { get; set; }

		/// <summary>
		/// The price of each stock
		/// </summary>
		[Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1")]
		public double Price { get; set; }

		public override string ToString()
		{
			return $"Sell Order Id: {SellOrderID},\n Stock Symbol: {StockSymbol},\n Stock name: {StockName},\n Date and time of sell order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm:ss tt")},\n Quantity: {Quantity},\n Buy Price: {Price}";
		}
	}
}