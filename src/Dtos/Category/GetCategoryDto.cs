using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Dtos.Category
{
    /// <summary>
    /// Represents the data required to search for categories.
    /// </summary>
    [BindProperties]
    public class GetCategoryDto
    {
        /// <summary>
        /// The name of the category to search for.
        /// </summary>
        public string? Name { get; set; }
    }
}