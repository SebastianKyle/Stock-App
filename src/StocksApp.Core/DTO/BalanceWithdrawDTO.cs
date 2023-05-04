using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.DTO
{
    public class BalanceWithdrawDTO
    {
        public Guid UserID { get; set; }        

        [Range(1, 10000, ErrorMessage = "You can withdraw maximum 10000$, minimum 1$")]
        public double WithdrawAmount { get; set; }
    }
}