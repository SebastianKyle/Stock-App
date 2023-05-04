using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.AccountBalanceServices
{
    public interface IAccountBalanceWithdrawService
    {
        /// <summary>
        /// Witdraw money from user account balance
        /// </summary>
        /// <param name="userID">User ID to search</param>
        /// <returns>UserAccountBalanceResponse object after withdrawing</returns>
        Task<UserAccountBalanceResponse?> Withdraw(BalanceWithdrawDTO? balanceWithdrawDTO);
    }
}