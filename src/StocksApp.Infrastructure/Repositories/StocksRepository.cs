using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Infrastructure.AppDbContext;

namespace StocksApp.Infrastructure.Repositories
{
    public class StocksRepository : IStocksRepository
    {
       private readonly ApplicationDbContext _dbContext;

        public StocksRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            _dbContext.BuyOrders.Add(buyOrder);
            await _dbContext.SaveChangesAsync();

            return buyOrder;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            _dbContext.SellOrders.Add(sellOrder);
            await  _dbContext.SaveChangesAsync();

            return sellOrder;
        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _dbContext.BuyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();

            return buyOrders;
        }

        public async Task<List<SellOrder>> GetSellOrder()
        {
            List<SellOrder> sellOrders = await _dbContext.SellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();
            
            return sellOrders;
        }

        public async Task<List<BuyOrder>> GetUserBuyOrders(Guid userID)
        {
            List<BuyOrder> userBuyOrders = await _dbContext.BuyOrders.Where(temp => temp.UserID == userID).ToListAsync();

            return userBuyOrders;
        }

        public async Task<List<SellOrder>> GetUserSellOrders(Guid userID)
        {
            List<SellOrder> userSellOrder = await _dbContext.SellOrders.Where(temp => temp.UserID == userID).ToListAsync();
            
            return userSellOrder;
        }
  }
}