using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.IdentityEntities;
using StocksApp.Core.DTO;

namespace StocksApp.Core.Domain.Entities
{
    /// <summary>
    /// Represents a model containing details for account balance of each user
    /// </summary>
    public class UserAccountBalance
    {
        /// <summary>
        /// Unique ID of each user
        /// </summary>
        [Key]
        [ForeignKey(nameof(ApplicationUser.Id))]
        public Guid UserID { get; set; } 

        /// <summary>
        /// The current balance of user account
        /// </summary>
        public double AccountBalance { get; set; }
    }

    /// <summary>
    /// UserAccountBalanceRequest extensions
    /// </summary>
    public static class UserAccountBalanceRequestExtensions
    {
        /// <summary>
        /// Convert current UserAccountBalanceRequest object to UserAccountBalance object
        /// </summary>
        /// <param name="userAccountBalance">UserAccountBalanceRequest object to convert</param>
        /// <returns>The converted UserAccountBalance object</returns>
        public static UserAccountBalance ToUserAccountBalance(this UserAccountBalanceRequest userAccountBalanceRequest)
        {
            UserAccountBalance userAccountBalance = new UserAccountBalance() {
                UserID = userAccountBalanceRequest.UserID,
                AccountBalance = userAccountBalanceRequest.AccountBalance
            };

            return userAccountBalance;
        }
    }
}