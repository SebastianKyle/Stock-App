using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.UI.Models
{
    /// <summary>
    /// Represents a model class that provice trade details (stockID, stock name, price, ...) to the Trade/Index view
    /// </summary>
    public class StockTrade
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; } = 0;
        public uint Quantity { get; set; } = 0;
    }
}