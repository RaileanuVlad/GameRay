using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;
using GameRay.Contexts;

namespace GameRay.Repositories.GameRepository
{
    public class GameRepository:IGameRepository
    {
        public Context _context { get; set; }

        public GameRepository(Context context)
        {
            _context = context;
        }
        public Game Create(Game game)
        {
            var result = _context.Add<Game>(game);
            _context.SaveChanges();
            return result.Entity;
        }
        public Game Get(int Id)
        {
            return _context.Games.SingleOrDefault(x => x.Id == Id) ;
        }
        public List<Game> GetAll()
        {
            return _context.Games.ToList();
        }
        public Game Update(Game game)
        {
            _context.Entry(game).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return game;
        }
        public Game Delete(Game game)
        {
            var result = _context.Remove(game);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
