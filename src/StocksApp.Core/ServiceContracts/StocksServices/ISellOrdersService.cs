using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.StocksServices
{
    /// <summary>
    /// Represents a service that perform operations like creating sell orders, get sell orders
    /// </summary>
    public interface ISellOrdersService
    {
        /// <summary>
        /// Create a sell order
        /// </summary>
        /// <param name="sellOrderRequest">Sell order object</param>
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        /// <summary>
        /// Returns all existing sell orders
        /// </summary>
        /// <returns>List of SellOrderResponse object</returns>
        Task<List<SellOrderResponse>> GetSellOrders(); 

        /// <summary>
        /// Returns all sell orders of current user
        /// </summary>
        /// <returns>List of SellOrderResponse of current user</returns>
        Task<List<SellOrderResponse>> GetUserSellOrders(Guid userID);
    }
}