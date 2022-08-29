using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Models;
using GameRay.Helpers;

namespace GameRay.Repositories.AdminRepository
{
    public interface IAdminRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        List<Admin> GetAll();
        Admin Get(int Id);
        Admin Create(Admin admin);
        Admin Update(Admin admin);
        Admin Delete(Admin admin);
    }
}
