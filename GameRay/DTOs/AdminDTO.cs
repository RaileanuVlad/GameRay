using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRay.DTOs
{
    public class AdminDTO
    {
        public string Password { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Birth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int State { get; set; }
    }
}
