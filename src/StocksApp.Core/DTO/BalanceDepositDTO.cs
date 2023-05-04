using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.DTO
{
    public class BalanceDepositDTO
    {
        public Guid UserID { get; set; }

        [Range(1, 10000, ErrorMessage = "You can deposit maximum 10000$, minimum 1$")]
        public double DepositAmount { get; set; }
    }
}