using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Microsoft.Extensions.Options;
using GameRay.DTOs;
using GameRay.Models;
using GameRay.Repositories.UserRepository;
using GameRay.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        public IUserRepository IUserRepository { get; set; }
        private readonly AppSettings _appSettings;

        public UserController(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            IUserRepository = userRepository;
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
        public ActionResult<IEnumerable<User>> Get()
        {
            List<User> Users = IUserRepository.GetAll();
            foreach (User user in Users)
            {
                user.Password = null;
            }
            return Users;
        }

        [HttpGet("{id}")]
        public UserDTO Get(int id)
        {
            User User = IUserRepository.Get(id);
            UserDTO MyUser = new UserDTO()
            {
                Email = User.Email,
                State = User.State
            };
            return MyUser;
        }

        [HttpPost]
        public IActionResult Post(UserDTO value)
        {
            List<User> Users = IUserRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(value.Email))
            {
                if (!IsValid(value.Email))
                {
                    return BadRequest(new { message = "Email is not valid" });
                }
            }
            if (Users.SingleOrDefault(x => x.Email == value.Email) != null)
            {
                return BadRequest(new { message = "This email is already registered" });
            }
            
            User model = new User
            {
                Email = value.Email,
                Password = Encrypt(value.Password),
                State = value.State
            };

            IUserRepository.Create(model);
            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = IUserRepository.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Wrong email or password" });

            //if (response.Token == "")
                //return BadRequest(new { message = "Your email is not confirmed" });

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserDTO value)
        {
            List<User> Users = IUserRepository.GetAll();
            User model = IUserRepository.Get(id);
            if (!string.IsNullOrWhiteSpace(value.Email))
            {
                if(!IsValid(value.Email))
                {
                    return BadRequest(new { message = "Email is not valid" });
                }
            }
            if (model.Email != value.Email) if (Users.SingleOrDefault(x => x.Email == value.Email) != null)
            {
                return BadRequest(new { message = "There is another user with this email already" });
            }
            if (!string.IsNullOrWhiteSpace(value.Email))
            {
                model.Email = value.Email;
            }
            if (!string.IsNullOrWhiteSpace(value.Password))
            {
                model.Password = Encrypt(value.Password);
            }
            if (value.State >= 0)
            {
                model.State = value.State;
            }
            IUserRepository.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public User Delete(int id)
        {
            User model = IUserRepository.Get(id);
            return IUserRepository.Delete(model);
        }
    }
}
