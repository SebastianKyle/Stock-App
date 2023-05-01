using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents a stocks repository that perform operations like create orders, retrieve orders details
    /// </summary>
    public interface IStocksRepository
    {
        /// <summary>
        /// Create a buy order
        /// </summary>
        /// <param name="buyOrder">Buy order to create</param>
        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        /// <summary>
        /// Creates a sell order
        /// </summary>
        /// <param name="sellOrder">Sell order to create</param>
        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        /// <summary>
        /// Get all buy orders
        /// </summary>
        /// <returns>List of BuyOrder objects</returns>
        Task<List<BuyOrder>> GetBuyOrders();

        /// <summary>
        /// Get all sell orders
        /// </summary>
        /// <returns>List of SellOrder objects</returns>
        Task<List<SellOrder>> GetSellOrder(); 

        /// <summary>
        /// Get all buy orders of current user
        /// </summary>
        /// <returns>List of BuyOrders of current user</returns>
        Task<List<BuyOrder>> GetUserBuyOrders(Guid userID);

        /// <summary>
        /// Get all sell orders of current user
        /// </summary>
        /// <returns>List of SellOrder od current user</returns>
        Task<List<SellOrder>> GetUserSellOrders(Guid userID);
    }
}