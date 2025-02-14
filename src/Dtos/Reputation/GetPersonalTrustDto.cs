using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Dtos.Reputation
{
    [BindProperties]
    public class GetPersonalTrustDto
    {
        [Required]
        public string TrusteeId { get; set; } = string.Empty;
        [Required]
        [Range(0, 1, ErrorMessage = "d must be between 0 and 1")]
        public double D { get; set; }
    }
}