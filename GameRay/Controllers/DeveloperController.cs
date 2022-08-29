using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRay.DTOs;
using GameRay.Models;
using GameRay.Repositories.DeveloperRepository;


namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        public IDeveloperRepository IDeveloperRepository { get; set; }
        public DeveloperController(IDeveloperRepository repository)
        {
            IDeveloperRepository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Developer>> Get()
        {
            return IDeveloperRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Developer> Get(int id)
        {
            return IDeveloperRepository.Get(id);
        }


        [HttpPost]
        public Developer Post(DeveloperDTO value)
        {
            Developer model = new Developer()
            {
                Name = value.Name,
                Website = value.Website,
                Country = value.Country
            };
            return IDeveloperRepository.Create(model);
        }

        [HttpPut("{id}")]
        public Developer Put(int id, DeveloperDTO value)
        {
            Developer model = IDeveloperRepository.Get(id);
            if (!string.IsNullOrWhiteSpace(value.Name))
            {
                model.Name = value.Name;
            }
            if (!string.IsNullOrWhiteSpace(value.Website))
            {
                model.Website = value.Website;
            }
            if (!string.IsNullOrWhiteSpace(value.Country))
            {
                model.Country = value.Country;
            }
            return IDeveloperRepository.Update(model);
        }

        [HttpDelete("{id}")]
        public Developer Delete(int id)
        {
            Developer developer = IDeveloperRepository.Get(id);
            return IDeveloperRepository.Delete(developer);
        }
    }
}