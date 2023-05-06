using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.UserStockServices
{
    /// <summary>
    /// Represents a service that perform operation of adding user stock after placing buy order
    /// </summary>
    public interface IUserStockAddService
    {
        /// <summary>
        /// Add user stock after placing buy order
        /// </summary>
        /// <param name="userStock">UserStock to add</param>
        Task<UserStockResponse> AddUserStock(UserStockRequest? userStock);
    }
}