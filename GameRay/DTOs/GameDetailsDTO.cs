using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRay.DTOs
{
    public class GameDetailsDTO
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
        public int CopiesSold { get; set; }
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public List<int> CategoryId { get; set; }
        public List<string> CategoryName { get; set; } 
    }
}
