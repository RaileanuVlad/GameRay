using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.DTOs;
using GameRay.Models;
using GameRay.Repositories.GameCategoryRepository;


namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCategoryController : ControllerBase
    {
        public IGameCategoryRepository IGameCategoryRepository { get; set; }

        public GameCategoryController(IGameCategoryRepository repository)
        {
            IGameCategoryRepository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameCategory>> Get()
        {
            return IGameCategoryRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<GameCategory> Get(int id)
        {
            return IGameCategoryRepository.Get(id);
        }

        [HttpPost]
        public GameCategory Post(GameCategoryDTO value)
        {
            GameCategory model = new GameCategory()
            {
                GameId = value.GameId,
                CategoryId = value.CategoryId
            };
            return IGameCategoryRepository.Create(model);
        }

        [HttpPut("{id}")]
        public GameCategory Put(int id, GameCategoryDTO value)
        {
            GameCategory model = IGameCategoryRepository.Get(id);
            if (value.GameId >= 0)
            {
                model.GameId = value.GameId;
            }

            if (value.CategoryId >= 0)
            {
                model.CategoryId = value.CategoryId;
            }

            return IGameCategoryRepository.Update(model);
        }

        [HttpDelete("{id}")]
        public GameCategory Delete(int id)
        {
            GameCategory gameCategory = IGameCategoryRepository.Get(id);
            return IGameCategoryRepository.Delete(gameCategory);

        }
    }
}
