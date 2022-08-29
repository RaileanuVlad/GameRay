using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRay.Models
{
    public class UserGame
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string GameKey { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
    }
}
