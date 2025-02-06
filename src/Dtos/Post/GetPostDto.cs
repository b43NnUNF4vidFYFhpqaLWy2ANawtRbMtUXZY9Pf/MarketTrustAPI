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
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be atleast 1")]
        public int? Page { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page size must be atleast 1")]
        public int? PageSize { get; set; }
    }
}