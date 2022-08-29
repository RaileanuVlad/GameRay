using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;
using GameRay.Contexts;

namespace GameRay.Repositories.UserGameRepository
{
    public class UserGameRepository : IUserGameRepository
    {
        public Context _context { get; set; }

        public UserGameRepository(Context context)
        {
            _context = context;
        }
        public UserGame Create(UserGame userGame)
        {
            var result = _context.Add<UserGame>(userGame);
            _context.SaveChanges();
            return result.Entity;
        }
        public UserGame Get(int Id)
        {
            return _context.UserGames.SingleOrDefault(x => x.Id == Id);
        }
        public List<UserGame> GetAll()
        {
            return _context.UserGames.ToList();
        }
        public UserGame Update(UserGame userGame)
        {
            _context.Entry(userGame).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return userGame;
        }
        public UserGame Delete(UserGame userGame)
        {
            var result = _context.Remove(userGame);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}

