using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "User name can not be blank!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email can not be blank!")]
        [EmailAddress(ErrorMessage = "Email should be in a proper format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone can not be blank!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain numbers only")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password can not be blank!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password can not be blank!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}