using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.UserStockServices
{
    /// <summary>
    /// Represents a service that perform operation of decreasing user stock after placing sell order
    /// </summary>
    public interface IUserStockDecreaseService
    {
        /// <summary>
        /// Remove or decrease user stock after placing sell order
        /// </summary>
        /// <param name="userStock">UserStock to decrease</param>
        Task<UserStockResponse> DecreaseUserStock(UserStockRequest? userStock);
    }
}