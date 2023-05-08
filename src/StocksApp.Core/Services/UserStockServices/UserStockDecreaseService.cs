using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.RepositoryContracts;
using StocksApp.Core.DTO;
using StocksApp.Core.ServiceContracts.UserStockServices;

namespace StocksApp.Core.Services.UserStockServices
{
  public class UserStockDecreaseService : IUserStockDecreaseService
  {
    private readonly IUserStockRepository _userStockRepository;
    private readonly ILogger<UserStockAddService> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public UserStockDecreaseService(IUserStockRepository userStockRepository, ILogger<UserStockAddService> logger, IDiagnosticContext diagnosticContext)
    {
      _userStockRepository = userStockRepository;
      _logger = logger;
      _diagnosticContext = diagnosticContext;
    }

    public async Task<UserStockResponse> DecreaseUserStock(UserStockRequest? userStockRequest)
    {
      _logger.LogInformation($"{nameof(DecreaseUserStock)} method of {nameof(UserStockAddService)}");

      if (userStockRequest == null)
      {
        throw new ArgumentNullException(nameof(userStockRequest));
      }

      UserStock userStock = userStockRequest.ToUserStock();

      UserStock userStockFromDecrease = await _userStockRepository.DecreaseUserStock(userStock);

      return userStockFromDecrease.ToUserStockResponse();
    }
  }
}