using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.DTO
{
    /// <summary>
    /// A DTO class that can be used while inserting / updating
    /// </summary>
    public class UserAccountBalanceRequest
    { 
        /// <summary>
        /// Unique ID of each user
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Balance of user account
        /// </summary>
        public double AccountBalance { get; set; }
    }
}