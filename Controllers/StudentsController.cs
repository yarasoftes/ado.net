using Microsoft.AspNetCore.Mvc;
using CollegeWebAPI.Data;
using CollegeWebAPI.Models;

namespace CollegeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repository;

        public StudentsController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _repository = new StudentRepository(connectionString);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _repository.GetById(id);
            if (student == null) return NotFound("Студент не найден");
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            int newId = _repository.CreateStudent(student);
            student.Id = newId;
            return CreatedAtAction(nameof(GetById), new { id = newId }, student);
        }
    }
}