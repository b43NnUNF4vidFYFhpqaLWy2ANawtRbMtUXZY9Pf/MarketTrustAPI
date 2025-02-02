using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.User
{
    public class LoginDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}