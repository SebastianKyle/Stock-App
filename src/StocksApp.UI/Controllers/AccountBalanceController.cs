using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StocksApp.Core.DTO;
using StocksApp.Core.ServiceContracts.AccountBalanceServices;

namespace StocksApp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountBalanceController : Controller
    {
        private readonly ILogger<AccountBalanceController> _logger;
        private readonly IAccountBalanceGetService _getService;
        private readonly IAccountBalanceDepositService _depositService;
        private readonly IAccountBalanceWithdrawService _withdrawService;

        public AccountBalanceController(ILogger<AccountBalanceController> logger, IAccountBalanceGetService getService, IAccountBalanceDepositService depositService, IAccountBalanceWithdrawService withdrawService)
        {
            _logger = logger;
            _getService = getService;
            _depositService = depositService;
            _withdrawService = withdrawService;
        }

        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            // Get user account balance
            Guid userID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UserAccountBalanceResponse? userAccountBalanceResponse = await _getService.GetAccountBalance(userID);

            ViewBag.AccountBalance = userAccountBalanceResponse.AccountBalance;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(BalanceDepositDTO balanceDepositDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage).ToList();

                return View(balanceDepositDTO);
            }   

            balanceDepositDTO.UserID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UserAccountBalanceResponse userAccountBalanceResponse = await _depositService.Deposit(balanceDepositDTO);

            ViewBag.AccountBalance = userAccountBalanceResponse.AccountBalance;
            TempData["Succeeded"] = 1;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Withdraw()
        {
            // Get user account balance
            Guid userID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UserAccountBalanceResponse? userAccountBalanceResponse = await _getService.GetAccountBalance(userID);

            ViewBag.AccountBalance = userAccountBalanceResponse.AccountBalance;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(BalanceWithdrawDTO balanceWithdrawDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage).ToList();

                return View(balanceWithdrawDTO);
            }   

            balanceWithdrawDTO.UserID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UserAccountBalanceResponse? userAccountBalanceResponse = null;
            try 
            {
                userAccountBalanceResponse = await _withdrawService.Withdraw(balanceWithdrawDTO);
            }
            catch (ArgumentException ex)
            {
                TempData["Succeeded"] = 0;
                TempData["Errors"] = ex.Message;
                ViewBag.AccountBalance = userAccountBalanceResponse.AccountBalance;
                return View(balanceWithdrawDTO);
            }

            ViewBag.AccountBalance = userAccountBalanceResponse.AccountBalance;
            TempData["Succeeded"] = 1;

            return View(balanceWithdrawDTO);
        }
    }
}