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
  public class AccountBalanceDepositService : IAccountBalanceDepositService
  {
		private readonly IAccountBalanceRepository _accountBalanceRepository;
		private readonly ILogger<AccountBalanceDepositService> _logger;
		private readonly IDiagnosticContext _diagnosticContext;

		public AccountBalanceDepositService(IAccountBalanceRepository accountBalanceRepository, ILogger<AccountBalanceDepositService> logger, IDiagnosticContext diagnosticContext)
		{
			_accountBalanceRepository = accountBalanceRepository;
			_logger = logger;
			_diagnosticContext = diagnosticContext;
		}

    public async Task<UserAccountBalanceResponse> Deposit(BalanceDepositDTO? balanceDepositDTO)
    {
      if (balanceDepositDTO == null)
      {
        throw new ArgumentNullException(nameof(balanceDepositDTO));
      }

			ValidationHelper.ModelValidation(balanceDepositDTO);

			UserAccountBalance matchingUser = await _accountBalanceRepository.GetAccountBalance(balanceDepositDTO.UserID);

			matchingUser.AccountBalance += balanceDepositDTO.DepositAmount;
			await _accountBalanceRepository.UpdateAccountBalance(matchingUser);

			return matchingUser.ToUserAccountBalanceResponse();
    }
  }
}