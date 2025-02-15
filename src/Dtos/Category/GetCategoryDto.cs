using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Dtos.Category
{
    [BindProperties]
    public class GetCategoryDto
    {
        public string? Name { get; set; }
    }
}