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
  public class UserStockRepository : IUserStockRepository
  {
    private readonly ApplicationDbContext _db;

    public UserStockRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UserStock> AddUserStock(UserStock userStock)
    {
        UserStock? matchingStock = _db.UserStocks.FirstOrDefault(temp => temp.UserID == userStock.UserID && temp.StockSymbol == userStock.StockSymbol);

        if (matchingStock == null)
        {
            _db.UserStocks.Add(userStock);
            await _db.SaveChangesAsync();
        }
        else
        {
            matchingStock.Quantity += userStock.Quantity; 
            await _db.SaveChangesAsync();
        }

        return userStock;
    }

    public async Task<List<UserStock>> GetUserStocks(Guid? userID)
    {
        List<UserStock> userStocks = await _db.UserStocks.Where(temp => temp.UserID == userID).ToListAsync();

        return userStocks;
    }
  }
}