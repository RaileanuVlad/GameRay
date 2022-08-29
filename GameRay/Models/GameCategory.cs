using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRay.Models
{
    public class GameCategory
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CategoryId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Category Category { get; set; }
    }
}
