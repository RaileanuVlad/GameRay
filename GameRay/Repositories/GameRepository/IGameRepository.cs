using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;

namespace GameRay.Repositories.GameRepository
{
    public interface IGameRepository
    {
        List<Game> GetAll();
        Game Get(int Id);
        Game Create(Game game);
        Game Update(Game game);
        Game Delete(Game game);
    }
}
