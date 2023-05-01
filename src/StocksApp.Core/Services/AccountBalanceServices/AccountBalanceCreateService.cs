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
    public class AccountBalanceCreateService : IAccountBalanceCreateService
    {
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        private readonly ILogger<AccountBalanceCreateService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public AccountBalanceCreateService(IAccountBalanceRepository accountBalanceRepository, ILogger<AccountBalanceCreateService> logger, IDiagnosticContext diagnosticContext)
        {
            _accountBalanceRepository = accountBalanceRepository;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        public async Task<UserAccountBalanceResponse> CreateAccountBalance(UserAccountBalanceRequest? userAccountBalanceRequest)
        {
            _logger.LogInformation($"{nameof(CreateAccountBalance)} method of {nameof(AccountBalanceCreateService)}");

            if (userAccountBalanceRequest == null)
            {
                throw new ArgumentNullException(nameof(userAccountBalanceRequest));
            }

            UserAccountBalance userAccountBalance = userAccountBalanceRequest.ToUserAccountBalance();

            UserAccountBalance userAccountBalanceFromRepo = await _accountBalanceRepository.CreateAccountBalance(userAccountBalance);
            
            UserAccountBalanceResponse userAccountBalanceResponse = userAccountBalanceFromRepo.ToUserAccountBalanceResponse();

            return userAccountBalanceResponse;
        }
  }
}