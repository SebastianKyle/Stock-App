using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.DTO;
using StocksApp.Core.ServiceContracts.AccountBalanceServices;

namespace StocksApp.Core.Services.AccountBalanceServices
{
  public class AccountBalanceGetService : IAccountBalanceGetService
  {
    private readonly IAccountBalanceRepository _accountBalanceRepository;
    private readonly ILogger<AccountBalanceGetService> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public AccountBalanceGetService(IAccountBalanceRepository accountBalanceRepository, ILogger<AccountBalanceGetService> logger, IDiagnosticContext diagnosticContext)
    {
        _accountBalanceRepository = accountBalanceRepository;
        _logger = logger;
        _diagnosticContext = diagnosticContext;
    }

    public async Task<UserAccountBalanceResponse?> GetAccountBalance(Guid? userID)
    {
        if (userID == Guid.Empty)
        {
            return null;
        }

        UserAccountBalance? userAccountBalance = await _accountBalanceRepository.GetAccountBalance(userID);

        if (userAccountBalance == null)
        {
            return null;
        }

        UserAccountBalanceResponse userAccountBalanceResponse = userAccountBalance.ToUserAccountBalanceResponse();    

        return userAccountBalanceResponse;
    }
  }
}