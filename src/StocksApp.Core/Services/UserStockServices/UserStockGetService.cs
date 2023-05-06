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
  public class UserStockGetService : IUserStockGetService
  {
    private readonly IUserStockRepository _userStockRepository;
    private readonly ILogger<UserStockGetService> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public UserStockGetService(IUserStockRepository userStockRepository, ILogger<UserStockGetService> logger, IDiagnosticContext diagnosticContext)
    {
      _userStockRepository = userStockRepository;
      _logger = logger;
      _diagnosticContext = diagnosticContext;
    }

    public async Task<List<UserStockResponse>> GetUserStocks(Guid? userID)
    {
      _logger.LogInformation($"{nameof(GetUserStocks)} method of {nameof(UserStockGetService)}");

      if (userID == null)
      {
        throw new ArgumentNullException(nameof(userID));
      }

      List<UserStock> userStocks = await _userStockRepository.GetUserStocks(userID);

      return userStocks.Select(temp => temp.ToUserStockResponse()).ToList();
    }
  }
}