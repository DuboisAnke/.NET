using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGamingAPI.Models
{
    public class RegistrationModel : LoginModel
    {
        [Required, MinLength(6), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
