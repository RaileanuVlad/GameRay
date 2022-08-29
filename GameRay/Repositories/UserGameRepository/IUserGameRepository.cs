using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;

namespace GameRay.Repositories.UserGameRepository
{
    public interface IUserGameRepository
    {
        List<UserGame> GetAll();
        UserGame Get(int Id);
        UserGame Create(UserGame userGame);
        UserGame Update(UserGame userGame);
        UserGame Delete(UserGame userGame);
    }
}
