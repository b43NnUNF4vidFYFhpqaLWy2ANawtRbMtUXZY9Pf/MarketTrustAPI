using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<User> Participants { get; set; } = new List<User>();
    }
}