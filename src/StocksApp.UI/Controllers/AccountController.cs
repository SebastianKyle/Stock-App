using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StocksApp.Core.Domain.IdentityEntities;
using StocksApp.Core.DTO;
using StocksApp.Core.ServiceContracts.AccountBalanceServices;

namespace StocksApp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountBalanceCreateService _accountBalanceCreateService;

        public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAccountBalanceCreateService accountBalanceCreateService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountBalanceCreateService = accountBalanceCreateService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser() {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
                UserName = registerDTO.Email,
                PersonName = registerDTO.UserName
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                // Sign in
                await _signInManager.SignInAsync(user, isPersistent: false);

                UserAccountBalanceRequest userAccountBalanceRequest = new UserAccountBalanceRequest() {
                    UserID = user.Id,
                    AccountBalance = 0
                };

                await _accountBalanceCreateService.CreateAccountBalance(userAccountBalanceRequest);

                return RedirectToAction(nameof(StocksController.Explore), "Explore");
            }
            else
            {
                // Read error details and add error model into ModelState
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(registerDTO);
            }

        }

        public IActionResult Login()
        {
            return View();
        }
    }
}