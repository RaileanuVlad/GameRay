using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.DTOs;
using GameRay.Models;
using GameRay.Repositories.CategoryRepository;


namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategoryRepository ICategoryRepository { get; set; }

        public CategoryController(ICategoryRepository repository)
        {
            ICategoryRepository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return ICategoryRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            return ICategoryRepository.Get(id);
        }

        [HttpPost]
        public Category Post(CategoryDTO value)
        {
            Category model = new Category
            {
                Name = value.Name
            };
            return ICategoryRepository.Create(model);
        }

        [HttpPut("{id}")]
        public Category Put(int id, CategoryDTO value)
        {
            Category model = ICategoryRepository.Get(id);
            if (!string.IsNullOrWhiteSpace(value.Name))
            {
                model.Name = value.Name;
            }
            return ICategoryRepository.Update(model);
        }

        [HttpDelete("{id}")]
        public Category Delete(int id)
        {
            Category model = ICategoryRepository.Get(id);
            return ICategoryRepository.Delete(model);
        }
    }
}
