using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRay.DTOs
{
    public class UserGameDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string GameKey { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
