using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;
using GameRay.Contexts;

namespace GameRay.Repositories.GameCategoryRepository
{
    public class GameCategoryRepository: IGameCategoryRepository
    {
        public Context _context { get; set; }

        public GameCategoryRepository(Context context)
        {
            _context = context;
        }
        public GameCategory Create(GameCategory gameCategory)
        {
            var result = _context.Add<GameCategory>(gameCategory);
            _context.SaveChanges();
            return result.Entity;
        }
        public GameCategory Get(int Id)
        {
            return _context.GameCategories.SingleOrDefault(x => x.Id == Id);
        }
        public List<GameCategory> GetAll()
        {
            return _context.GameCategories.ToList();
        }
        public GameCategory Update(GameCategory gameCategory)
        {
            _context.Entry(gameCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return gameCategory;
        }
        public GameCategory Delete(GameCategory gameCategory)
        {
            var result = _context.Remove(gameCategory);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
