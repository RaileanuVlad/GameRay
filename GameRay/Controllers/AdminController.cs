using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using GameRay.DTOs;
using GameRay.Models;
using GameRay.Repositories.AdminRepository;
using GameRay.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAdminRepository IAdminRepository { get; set; }
        private readonly AppSettings _appSettings;

        public AdminController(IAdminRepository adminRepository, IOptions<AppSettings> appSettings)
        {
            IAdminRepository = adminRepository;
            _appSettings = appSettings.Value;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Admin>> Get()
        {
            List<Admin> Admins = IAdminRepository.GetAll();
            foreach (Admin admin in Admins)
            {
                admin.Password = null;
            }
            return Admins;
        }

        [HttpGet("{id}")]
        public AdminDTO Get(int id)
        {
            Admin Admin = IAdminRepository.Get(id);
            AdminDTO MyAdmin = new AdminDTO()
            {
                Email = Admin.Email,
                First = Admin.First,
                Last = Admin.Last, 
                Birth = Admin.Birth,
                Phone = Admin.Phone,
                State = Admin.State
            };
            return MyAdmin;
        }

        [HttpPost]
        public IActionResult Post(AdminDTO value)
        {
            List<Admin> Admins = IAdminRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(value.Email))
            {
                if (!IsValid(value.Email))
                {
                    return BadRequest(new { message = "Email is not valid" });
                }
            }
            if (Admins.SingleOrDefault(x => x.Email == value.Email) != null)
            {
                return BadRequest(new { message = "There is another admin with this email already" });
            }
            
            Admin model = new Admin
            {
                Email = value.Email,
                Password = Encrypt(value.Password),
                First = value.First,
                Last = value.Last,
                Birth = value.Birth,
                Phone = value.Phone,
                State = value.State
            };
            IAdminRepository.Create(model);
            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = IAdminRepository.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Wrong email or password" });

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AdminDTO value)
        {
            List<Admin> Admins = IAdminRepository.GetAll();
            Admin model = IAdminRepository.Get(id);
            if (!string.IsNullOrWhiteSpace(value.Email))
            {
                if (!IsValid(value.Email))
                {
                    return BadRequest(new { message = "Email is not valid" });
                }
            }
            if (model.Email!=value.Email) if (Admins.SingleOrDefault(x => x.Email == value.Email) != null)
            {
                return BadRequest(new { message = "There is another admin with this email already" });
            }
            
            if (!string.IsNullOrWhiteSpace(value.Email))
            {
                model.Email = value.Email;
            }
            if (!string.IsNullOrWhiteSpace(value.Password))
            {
                model.Password = Encrypt(value.Password);
            }
            if (!string.IsNullOrWhiteSpace(value.First))
            {
                model.First = value.First;
            }
            if (!string.IsNullOrWhiteSpace(value.Last))
            {
                model.Last = value.Last;
            }
            if (!string.IsNullOrWhiteSpace(value.Birth))
            {
                model.Birth = value.Birth;
            }
            if (!string.IsNullOrWhiteSpace(value.Phone))
            {
                model.Phone = value.Phone;
            }
            if (value.State >= 0)
            {
                model.State = value.State;
            }
            IAdminRepository.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public Admin Delete(int id)
        {
            Admin model = IAdminRepository.Get(id);
            return IAdminRepository.Delete(model);
        }
    }
}
