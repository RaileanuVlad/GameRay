using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.Contexts;
using GameRay.Models;
using GameRay.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GameRay.Repositories.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        public Context _context { get; set; }
        private readonly AppSettings _appSettings;
        public AdminRepository(Context context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public string Encrypt(string text)
        {
            try
            {
                string encryptedText = "";
                string key1 = _appSettings.Key;
                string key2 = string.Join("", key1.ToCharArray().Reverse());
                byte[] key1Byte = { };
                key1Byte = Encoding.UTF8.GetBytes(key1);
                byte[] key2byte = { };
                key2byte = Encoding.UTF8.GetBytes(key2);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = Encoding.UTF8.GetBytes(text);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(key1Byte, key2byte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    encryptedText = Convert.ToBase64String(ms.ToArray());
                }
                return encryptedText;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var admin = _context.Admins.SingleOrDefault(x => x.Email == model.Email && x.Password == Encrypt(model.Password));

            if (admin == null) return null;

            var token = generateJwtToken(admin);

            return new AuthenticateResponse(admin, token);
        }
        public Admin Create(Admin admin)
        {
            var result = _context.Add<Admin>(admin);
            _context.SaveChanges();
            return result.Entity;
        }
        public Admin Get(int Id)
        {
            return _context.Admins.SingleOrDefault(x => x.Id == Id);
        }
        public List<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }
        public Admin Update(Admin admin)
        {
            _context.Entry(admin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return admin;
        }
        public Admin Delete(Admin admin)
        {
            var result = _context.Remove(admin);
            _context.SaveChanges();
            return result.Entity;
        }
        private string generateJwtToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", admin.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

