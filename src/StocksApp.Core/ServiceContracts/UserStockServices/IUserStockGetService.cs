using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.UserStockServices
{
    /// <summary>
    /// Represents a service that perform operation of getting stocks of current user
    /// </summary>
    public interface IUserStockGetService
    {
        /// <summary>
        /// Get list of stocks of current user
        /// </summary>
        /// <param name="userID">User id to search</param>
        /// <returns>List of matching stocks with given user id</returns>
        Task<List<UserStockResponse>> GetUserStocks(Guid? userID);
    }
}