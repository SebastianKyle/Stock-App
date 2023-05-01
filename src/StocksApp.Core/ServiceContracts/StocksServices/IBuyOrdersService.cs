using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.StocksServices
{
    /// <summary>
    /// Represents a service that perform operations like creating buy orders, get buy orders
    /// </summary>
    public interface IBuyOrdersService
    {
        /// <summary>
        /// Create a buy order
        /// </summary>
        /// <param name="buyOrderRequest">Buy order object</param>
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
        
        /// <summary>
        /// Returns all existing buy orders
        /// </summary>
        /// <returns>List of BuyOrderResponse objects</returns>
        Task<List<BuyOrderResponse>> GetBuyOrders(); 

        /// <summary>
        /// Returns all buy orders of current user
        /// </summary>
        /// <returns>List of BuyOrderResponse of current user</returns>
        Task<List<BuyOrderResponse>> GetUserBuyOrders(Guid userID);
    }
}