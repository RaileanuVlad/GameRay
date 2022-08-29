using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.DTOs;
using GameRay.Models;
using GameRay.Repositories.UserGameRepository;


namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameController : ControllerBase
    {
        public IUserGameRepository IUserGameRepository { get; set; }

        public UserGameController(IUserGameRepository repository)
        {
            IUserGameRepository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserGame>> Get()
        {
            return Ok(IUserGameRepository.GetAll());
        }

        [HttpGet("email/{email}")]
        public ActionResult<IEnumerable<UserGame>> GetByEmail(string email)
        {
            return Ok(IUserGameRepository.GetAll().Where(sale => sale.Email == email));
        }

        [HttpGet("{id}")]
        public ActionResult<UserGame> Get(int id)
        {
            return Ok(IUserGameRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Post(UserGameDTO value)
        {
            UserGame model = new UserGame
            {
                UserId = value.UserId,
                GameId = value.GameId,
                Email = value.Email,
                Name = value.Name,
                Price = value.Price,
                GameKey = value.GameKey,
                Date = value.Date,
            
            };
            IUserGameRepository.Create(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserGameDTO value)
        {
            UserGame model = IUserGameRepository.Get(id);
            if (value.UserId >= 0)
            {
                model.UserId = value.UserId;
            }
            if (value.GameId >= 0)
            {
                model.GameId = value.GameId;
            }
            if (!string.IsNullOrWhiteSpace(value.Email))
            {
                model.Email = value.Email;
            }
            if (!string.IsNullOrWhiteSpace(value.Name))
            {
                model.Name = value.Name;
            }
            if (value.Price >= 0)
            {
                model.Price = value.Price;
            }
            if (!string.IsNullOrWhiteSpace(value.GameKey))
            {
                model.GameKey = value.GameKey;
            }
            if (!string.IsNullOrWhiteSpace(value.Date))
            {
                model.Date = value.Date;
            }

            IUserGameRepository.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public UserGame Delete(int id)
        {
            UserGame model = IUserGameRepository.Get(id);
            return IUserGameRepository.Delete(model);
        }
    }
}
