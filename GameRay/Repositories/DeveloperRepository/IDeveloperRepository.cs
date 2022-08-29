using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;

namespace GameRay.Repositories.DeveloperRepository
{
    public interface IDeveloperRepository
    {
        List<Developer> GetAll();
        Developer Get(int Id);
        Developer Create(Developer developer);
        Developer Update(Developer developer);
        Developer Delete(Developer developer);
    }
}
