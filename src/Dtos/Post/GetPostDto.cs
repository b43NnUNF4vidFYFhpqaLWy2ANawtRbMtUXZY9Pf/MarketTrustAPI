using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Dtos.Post
{
    [BindProperties]
    public class GetPostDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? CategoryId { get; set; }
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? Longitude { get; set; }
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? Latitude { get; set; }
        public double? SearchRadius { get; set; }
        [Range(0, 1, ErrorMessage = "d must be between 0 and 1")]
        public double? D { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be atleast 1")]
        public int? Page { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page size must be atleast 1")]
        public int? PageSize { get; set; }
    }
}