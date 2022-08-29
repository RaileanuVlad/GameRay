using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;
using GameRay.Contexts;

namespace GameRay.Repositories.DeveloperRepository
{
    public class DeveloperRepository: IDeveloperRepository
    {
        public Context _context { get; set; }

        public DeveloperRepository(Context context)
        {
            _context = context;
        }
        public Developer Create(Developer developer)
        {
            var result = _context.Add<Developer>(developer);
            _context.SaveChanges();
            return result.Entity;
        }
        public Developer Get(int Id)
        {
            return _context.Developers.SingleOrDefault(x => x.Id == Id);
        }
        public List<Developer> GetAll()
        {
            return _context.Developers.ToList();
        }
        public Developer Update(Developer developer)
        {
            _context.Entry(developer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return developer;
        }
        public Developer Delete(Developer developer)
        {
            var result = _context.Remove(developer);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
