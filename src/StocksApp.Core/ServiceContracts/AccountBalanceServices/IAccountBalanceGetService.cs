using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.AccountBalanceServices
{
    /// <summary>
    /// Represents a service that perform operation of retrieving account balance of current user
    /// </summary>
    public interface IAccountBalanceGetService
    {
        /// <summary>
        /// Return account balance of matching user with the given user ID
        /// </summary>
        /// <param name="userID">ID of user to search</param>
        /// <returns>Account balance of matching user</returns>
        Task<UserAccountBalanceResponse?> GetAccountBalance(Guid? userID);
    }
}