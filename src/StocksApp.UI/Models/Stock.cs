using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.UI.Models
{
    public class Stock
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            Stock other = (Stock)obj;

            return StockSymbol == other.StockSymbol && StockName == other.StockName;
        }
    }
}