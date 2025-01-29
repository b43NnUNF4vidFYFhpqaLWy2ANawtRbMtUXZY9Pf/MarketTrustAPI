using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.Post
{
    public class UpdatePostDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public int? CategoryId { get; set; }
    }
}