using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApp.Core.DTO;

namespace StocksApp.Core.ServiceContracts.AccountBalanceServices
{
    public interface IAccountBalanceDepositService
    {
        /// <summary>
        /// Deposit money into user account balance
        /// </summary>
        /// <param name="balanceDepositDTO">DTO object for depositing request</param>
        /// <returns>UserAccountBalanceResponse object after depositing</returns>
        Task<UserAccountBalanceResponse> Deposit(BalanceDepositDTO? balanceDepositDTO);
    }
}