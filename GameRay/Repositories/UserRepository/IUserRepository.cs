using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;
using GameRay.Helpers;

namespace GameRay.Repositories.UserRepository
{
    public interface IUserRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        List<User> GetAll();
        User Get(int Id);
        User Create(User user);
        User Update(User user);
        User Delete(User user);
    }
}
