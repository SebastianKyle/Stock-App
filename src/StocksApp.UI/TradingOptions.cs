using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.UI
{
    /// <summary>
    /// Represents options pattern for "StockPrice" configuration
    /// </summary>
    public class TradingOptions
    {
        public uint? DefaultOrderQuantity { get; set; }
        public string? Top25PopularStocks { get; set; }
    }
}