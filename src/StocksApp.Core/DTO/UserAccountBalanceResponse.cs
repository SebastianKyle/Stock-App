using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.DTO
{
    /// <summary>
    /// A DTO class that can be used as a return type for user account balance retrieving
    /// </summary>
    public class UserAccountBalanceResponse
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

    /// <summary>
    /// UserAccountBalance extensions
    /// </summary>
    public static class UserAccountBalanceExtensions
    {
        /// <summary>
        /// Convert current UserAccountBalance object to UserAccountBalanceResponse object
        /// </summary>
        /// <param name="userAccountBalance">UserAccountBalance object to convert</param>
        /// <returns>The converted UserAccountBalanceResponse object</returns>
        public static UserAccountBalanceResponse ToUserAccountBalanceResponse(this UserAccountBalance userAccountBalance)
        {
            UserAccountBalanceResponse userAccountBalanceResponse = new UserAccountBalanceResponse() {
                UserID = userAccountBalance.UserID,
                AccountBalance = userAccountBalance.AccountBalance
            };

            return userAccountBalanceResponse;
        }
    }
}