using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRay.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public List <Game> Game { get; set; }

    }
}
