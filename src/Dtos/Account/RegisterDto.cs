using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool IsPublicEmail { get; set; } = false;
        [Phone]
        public string? Phone { get; set; }
        public bool IsPublicPhone { get; set; } = false;
        public Point? Location { get; set; }
        public bool IsPublicLocation { get; set; } = false;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}