using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameRay.DTOs;
using GameRay.Models;
using GameRay.Repositories.GameRepository;
using GameRay.Repositories.GameCategoryRepository;
using GameRay.Repositories.CategoryRepository;
using GameRay.Repositories.DeveloperRepository;
using GameRay.Repositories.UserGameRepository;

namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public IGameRepository IGameRepository { get; set; }
        public ICategoryRepository ICategoryRepository { get; set; }
        public IGameCategoryRepository IGameCategoryRepository { get; set; }
        public IDeveloperRepository IDeveloperRepository { get; set; }
        public IUserGameRepository IUserGameRepository { get; set; }
        public GameController(IGameRepository gameRepository, ICategoryRepository categoryRepository, IGameCategoryRepository gameCategoryRepository, 
                                IDeveloperRepository developerRepository, IUserGameRepository userGameRepository)
        {
            IGameRepository = gameRepository;
            ICategoryRepository = categoryRepository;
            IGameCategoryRepository = gameCategoryRepository;
            IDeveloperRepository = developerRepository;
            IUserGameRepository = userGameRepository;
        }


        [HttpGet]
        public List<GameDetailsDTO> Get()
        {
            List<Game> Games = IGameRepository.GetAll();
            List<GameDetailsDTO> MyGames = new List<GameDetailsDTO>();
            IEnumerable<Category> MyCategories = ICategoryRepository.GetAll();
            IEnumerable<Developer> MyDevelopers = IDeveloperRepository.GetAll();
            IEnumerable<GameCategory> MyGameCategories = IGameCategoryRepository.GetAll();
            IEnumerable<UserGame> MyUserGames = IUserGameRepository.GetAll();


            foreach (Game MyGame in Games)
            {
                GameDetailsDTO MyGameDetailsDTO = new GameDetailsDTO()
                {
                    Id = MyGame.Id,
                    Name = MyGame.Name,
                    Price = MyGame.Price,
                    Description = MyGame.Description,
                    MinReq = MyGame.MinReq,
                    RecReq = MyGame.RecReq,
                    ImgLink = MyGame.ImgLink,
                    BanLink = MyGame.BanLink,
                    Platform = MyGame.Platform,
                    Views = MyGame.Views,
                    Sale = MyGame.Sale,
                    DeveloperId = MyGame.DeveloperId,
                    DeveloperName = MyDevelopers.SingleOrDefault(x => x.Id == MyGame.DeveloperId).Name,
                    CopiesSold = MyUserGames.Where(x => x.GameId == MyGame.Id).Count()
                };
                IEnumerable<GameCategory> MyMyGameCategories = MyGameCategories.Where(x => x.GameId == MyGame.Id);

                if (MyMyGameCategories != null)
                {
                    List<string> CategoryNameList = new List<string>();
                    List<int> CategoryIdList = new List<int>();
                    foreach (GameCategory MyMyGameCategory in MyMyGameCategories)
                    {
                        Category MyMyCategory = MyCategories.SingleOrDefault(x => x.Id == MyMyGameCategory.CategoryId);
                        CategoryIdList.Add(MyMyCategory.Id);
                        CategoryNameList.Add(MyMyCategory.Name);
                    }
                    MyGameDetailsDTO.CategoryId = CategoryIdList;
                    MyGameDetailsDTO.CategoryName = CategoryNameList;
                }
                MyGames.Add(MyGameDetailsDTO);
            }


            return MyGames;
        }

        [HttpGet("platform/{platform}")]
        public List<GameDetailsDTO> GetPlatform(string platform)
        {
            List<Game> Games = IGameRepository.GetAll();
            List<GameDetailsDTO> MyGames = new List<GameDetailsDTO>();
            IEnumerable<Category> MyCategories = ICategoryRepository.GetAll();
            IEnumerable<Developer> MyDevelopers = IDeveloperRepository.GetAll();
            IEnumerable<GameCategory> MyGameCategories = IGameCategoryRepository.GetAll();
            IEnumerable<UserGame> MyUserGames = IUserGameRepository.GetAll();

            foreach (Game MyGame in Games)
            {
                if(!String.Equals(MyGame.Platform, platform, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                GameDetailsDTO MyGameDetailsDTO = new GameDetailsDTO()
                {
                    Id = MyGame.Id,
                    Name = MyGame.Name,
                    Price = MyGame.Price,
                    Description = MyGame.Description,
                    MinReq = MyGame.MinReq,
                    RecReq = MyGame.RecReq,
                    ImgLink = MyGame.ImgLink,
                    BanLink = MyGame.BanLink,
                    Platform = MyGame.Platform,
                    Views = MyGame.Views,
                    Sale = MyGame.Sale,
                    DeveloperId = MyGame.DeveloperId,
                    DeveloperName = MyDevelopers.SingleOrDefault(x => x.Id == MyGame.DeveloperId).Name,
                    CopiesSold = MyUserGames.Where(x => x.GameId == MyGame.Id).Count()
                };
                IEnumerable<GameCategory> MyMyGameCategories = MyGameCategories.Where(x => x.GameId == MyGame.Id);

                if (MyMyGameCategories != null)
                {
                    List<string> CategoryNameList = new List<string>();
                    List<int> CategoryIdList = new List<int>();
                    foreach (GameCategory MyMyGameCategory in MyMyGameCategories)
                    {
                        Category MyMyCategory = MyCategories.SingleOrDefault(x => x.Id == MyMyGameCategory.CategoryId);
                        CategoryIdList.Add(MyMyCategory.Id);
                        CategoryNameList.Add(MyMyCategory.Name);
                    }
                    MyGameDetailsDTO.CategoryId = CategoryIdList;
                    MyGameDetailsDTO.CategoryName = CategoryNameList;
                }
                MyGames.Add(MyGameDetailsDTO);
            }

            return MyGames;
        }


        [HttpGet("{id}")]
        public GameDetailsDTO Get(int id)
        {
            Game Game = IGameRepository.Get(id);
            IEnumerable<UserGame> MyUserGames = IUserGameRepository.GetAll();
            GameDetailsDTO MyGame = new GameDetailsDTO()
            {
                Name = Game.Name,
                Price = Game.Price,
                Description = Game.Description,
                MinReq = Game.MinReq,
                RecReq = Game.RecReq,
                ImgLink = Game.ImgLink,
                BanLink = Game.BanLink,
                Platform = Game.Platform,
                Views = Game.Views,
                Sale = Game.Sale,
                DeveloperId = Game.DeveloperId,
                DeveloperName = IDeveloperRepository.Get(Game.DeveloperId).Name,
                CopiesSold = MyUserGames.Where(x => x.GameId == id).Count()
            };
            IEnumerable<GameCategory> MyGameCategories = IGameCategoryRepository.GetAll().Where(x => x.GameId == Game.Id);
            if (MyGameCategories != null)
            {
                List<string> CategoryNameList = new List<string>();
                List<int> CategoryIdList = new List<int>();
                foreach (GameCategory MyGameCategory in MyGameCategories)
                {
                    Category MyCategory = ICategoryRepository.GetAll().SingleOrDefault(x => x.Id == MyGameCategory.CategoryId);
                    CategoryIdList.Add(MyCategory.Id);
                    CategoryNameList.Add(MyCategory.Name);
                }
                MyGame.CategoryId = CategoryIdList;
                MyGame.CategoryName = CategoryNameList;
            }
 
            return MyGame;
        }


        [HttpPost]
        public IActionResult Post(GameDTO value)
        {
            Game model = new Game()
            {
                Name = value.Name,
                Price = value.Price,
                Description = value.Description,
                MinReq = value.MinReq,
                RecReq = value.RecReq,
                ImgLink = value.ImgLink,
                BanLink = value.BanLink,
                Platform = value.Platform,
                Views = value.Views,
                Sale = value.Sale,
                DeveloperId = value.DeveloperId
            };
            IGameRepository.Create(model);
            for (int i = 0; i < value.CategoryId.Count; i++)
            {
                GameCategory GameCategory = new GameCategory()
                {
                    GameId = model.Id,
                    CategoryId = value.CategoryId[i]
                };
                IGameCategoryRepository.Create(GameCategory);
            }
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, GameDTO value)
        {
            Game model = IGameRepository.Get(id);
            if (!string.IsNullOrWhiteSpace(value.Name))
            {
                model.Name = value.Name;
            }
            if (value.Price >= 0.0)
            {
                model.Price = value.Price;
            }
            if (!string.IsNullOrWhiteSpace(value.Description))
            {
                model.Description = value.Description;
            }
            if (!string.IsNullOrWhiteSpace(value.MinReq))
            {
                model.MinReq = value.MinReq;
            }
            if (!string.IsNullOrWhiteSpace(value.RecReq))
            {
                model.RecReq = value.RecReq;
            }
            if (!string.IsNullOrWhiteSpace(value.ImgLink))
            {
                model.ImgLink = value.ImgLink;
            }
            if (!string.IsNullOrWhiteSpace(value.BanLink))
            {
                model.BanLink = value.BanLink;
            }
            if (!string.IsNullOrWhiteSpace(value.Platform))
            {
                model.Platform = value.Platform;
            }
            if (value.Views >= 0)
            {
                model.Views = value.Views;
            }
            if (value.Sale >= 0)
            {
                model.Sale = value.Sale;
            }
            if (value.DeveloperId >= 0)
            {
                model.DeveloperId = value.DeveloperId;
            }

            IGameRepository.Update(model);

            if (value.CategoryId != null)
            {
                IEnumerable<GameCategory> MyGameCategories = IGameCategoryRepository.GetAll().Where(x => x.GameId == id);
                foreach (GameCategory MyGameCategory in MyGameCategories)
                {
                    IGameCategoryRepository.Delete(MyGameCategory);
                }
                for (int i = 0; i < value.CategoryId.Count; i++)
                {
                    GameCategory GameCategory = new GameCategory()
                    {
                        GameId = model.Id,
                        CategoryId = value.CategoryId[i]
                    };
                    IGameCategoryRepository.Create(GameCategory);
                }
            }
            return Ok();
        }

        [HttpPut("increment/{id}")]
        public IActionResult PutViews(int id)
        {
            Game model = IGameRepository.Get(id);
            model.Views = model.Views + 1;
            IGameRepository.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public Game Delete(int id)
        {
            Game game = IGameRepository.Get(id);
            return IGameRepository.Delete(game);
        }
    }
}
