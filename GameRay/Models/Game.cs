using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRay.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string MinReq { get; set; }
        public string RecReq { get; set; }
        public string Platform { get; set; }
        public string ImgLink { get; set; }
        public string BanLink { get; set; }
        public int Views { get; set; }
        public int Sale { get; set; }
        public int DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }
        public List<GameCategory> GameCategory { get; set; }
        public List<UserGame> UserGame { get; set; }

    }
}
