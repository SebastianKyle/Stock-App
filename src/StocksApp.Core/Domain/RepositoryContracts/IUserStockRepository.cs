using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents a user stock repository that perform operations like add and get user stocks
    /// </summary>
    public interface IUserStockRepository
    {
        /// <summary>
        /// Add user stock after place buy order
        /// </summary>
        /// <param name="userStock">UserStock to add</param>
        Task<UserStock> AddUserStock(UserStock userStock);

        /// <summary>
        /// Get the stocks of the current user
        /// </summary>
        /// <param name="userID">userID to search</param>
        /// <returns>List of stocks that belongs to the current user</returns>
        Task<List<UserStock>> GetUserStocks(Guid? userID);
    }
}