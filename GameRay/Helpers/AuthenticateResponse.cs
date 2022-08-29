using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;
using GameRay;

namespace GameRay.Helpers
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int State { get; set; }



        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            State = user.State;
            Token = token;
        }

        public AuthenticateResponse(Admin admin, string token)
        {
            Id = admin.Id;
            Email = admin.Email;
            State = admin.State;
            Token = token;
        }
    }
}
