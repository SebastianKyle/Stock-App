using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.DTO;
using StocksApp.Core.ServiceContracts.AccountBalanceServices;

namespace StocksApp.UI.ViewComponents
{
    public class AccountBalanceViewComponent : ViewComponent
    {
        private readonly IAccountBalanceGetService _accountBalanceGetService;

        public AccountBalanceViewComponent(IAccountBalanceGetService accountBalanceGetService)
        {
            _accountBalanceGetService = accountBalanceGetService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid userID)
        {
            UserAccountBalanceResponse? accountBalance = await _accountBalanceGetService.GetAccountBalance(userID);

            return View(accountBalance);
        } 
    }
}