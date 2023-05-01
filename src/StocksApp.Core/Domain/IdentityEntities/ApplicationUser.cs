using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StocksApp.Core.Domain.Entities;

namespace StocksApp.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Name of user
        /// </summary>
        public string PersonName { get; set; } 
    }
}