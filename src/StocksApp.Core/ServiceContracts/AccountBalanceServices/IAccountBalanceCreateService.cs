using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.AccountBalanceServices
{
    /// <summary>
    /// Represents a service that perform operation of creating account balance for the current user
    /// </summary>
    public interface IAccountBalanceCreateService
    {
        /// <summary>
        /// Create account balance for current user
        /// </summary>
        /// <param name="userAccountBalanceRequest">User account balance to create</param>
        /// <returns>UserAccountBalanceResponse object</returns>
        Task<UserAccountBalanceResponse> CreateAccountBalance(UserAccountBalanceRequest? userAccountBalanceRequest);
    }
}