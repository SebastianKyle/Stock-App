using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents a account balance repository that perform operation of creating and retrieving account balance by accessing database
    /// </summary>
    public interface IAccountBalanceRepository
    {
        /// <summary>
        /// Create account balance of the current user
        /// </summary>
        /// <param name="userAccountBalance">User account balance object to create</param>
        Task<UserAccountBalance> CreateAccountBalance(UserAccountBalance userAccountBalance);

        /// <summary>
        /// Get the account balance of the matching user with the given user ID
        /// </summary>
        /// <param name="userID">ID of the user to search</param>
        Task<UserAccountBalance?> GetAccountBalance(Guid? userID);
    }
}