using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.DTO;
using StocksApp.Core.Helpers;
using StocksApp.Core.ServiceContracts.AccountBalanceServices;

namespace StocksApp.Core.Services.AccountBalanceServices
{
  public class AccountBalanceWithdrawService : IAccountBalanceWithdrawService
  {
		private readonly IAccountBalanceRepository _accountBalanceRepository;
		private readonly ILogger<AccountBalanceDepositService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;

		public AccountBalanceWithdrawService(IAccountBalanceRepository accountBalanceRepository, ILogger<AccountBalanceDepositService> logger, IDiagnosticContext diagnosticContext)
		{
			_accountBalanceRepository = accountBalanceRepository;
			_logger = logger;
			_diagnosticContext = diagnosticContext;
		}

    public async Task<UserAccountBalanceResponse?> Withdraw(BalanceWithdrawDTO? balanceWithdrawDTO)
    {
      if (balanceWithdrawDTO == null)
      {
        throw new ArgumentNullException(nameof(balanceWithdrawDTO));
      }

			ValidationHelper.ModelValidation(balanceWithdrawDTO);

			UserAccountBalance? matchingUser = await _accountBalanceRepository.GetAccountBalance(balanceWithdrawDTO.UserID);

			if (matchingUser == null)
			{
				return null;
			}

			if (matchingUser.AccountBalance < balanceWithdrawDTO.WithdrawAmount)
			{
				throw new ArgumentException("Withdraw amount is greater than acccount balance");
			}

			matchingUser.AccountBalance -= balanceWithdrawDTO.WithdrawAmount;
			await _accountBalanceRepository.UpdateAccountBalance(matchingUser);

			return matchingUser.ToUserAccountBalanceResponse();
    }
  }
}