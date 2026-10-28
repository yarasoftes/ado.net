using Microsoft.AspNetCore.Mvc;
using CollegeWebAPI.Data;
using CollegeWebAPI.Models;

namespace CollegeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly GroupRepository _repository;

        public GroupsController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _repository = new GroupRepository(connectionString);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var group = _repository.GetById(id);
            if (group == null) return NotFound("Группа не найдена");
            return Ok(group);
        }
    }
}