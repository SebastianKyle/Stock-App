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
  public class AccountBalanceRepository : IAccountBalanceRepository
  {
    private readonly ApplicationDbContext _db;

    public AccountBalanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UserAccountBalance> CreateAccountBalance(UserAccountBalance userAccountBalance)
    {
        _db.UserAccountBalances.Add(userAccountBalance);
        await _db.SaveChangesAsync();

        return userAccountBalance;
    }

    public async Task<UserAccountBalance?> GetAccountBalance(Guid? userID)
    {
      UserAccountBalance? accountBalance = await _db.UserAccountBalances.FirstOrDefaultAsync(temp => temp.UserID == userID);
      await _db.SaveChangesAsync();

      return accountBalance;
    }
  }
}