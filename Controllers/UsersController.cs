using AuthAPI.Models;
using AuthAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        [Authorize]// Uses existing JWT Config
        public class UsersController: ControllerBase
        {
            private readonly IUserRepository _repository;

            public UsersController(IUserRepository repository)
            {
                _repository = repository;
            }
            [HttpGet]
            public IActionResult GetAll()
            {
                return Ok(_repository.GetAll());
            }
            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var user = _repository.GetById(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }

            [HttpPost]
            public IActionResult Add(UserProfile user)
            {
                _repository.Add(user);
                return Ok();
            }
        }
    }
