using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CollegeWebAPI
{
    // 1. МОДЕЛИ
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // 2. РЕПОЗИТОРИИ
    public class StudentRepository
    {
        private readonly string _connectionString;
        public StudentRepository(string connectionString) => _connectionString = connectionString;

        public List<Student> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    StudentID AS Id, 
                    FirstName + ' ' + LastName AS FullName, 
                    GroupID AS GroupId 
                FROM Students 
                ORDER BY StudentID";
                
            return connection.Query<Student>(query).AsList();
        }

        public Student GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    StudentID AS Id, 
                    FirstName + ' ' + LastName AS FullName, 
                    GroupID AS GroupId 
                FROM Students 
                WHERE StudentID = @Id";
                
            return connection.QueryFirstOrDefault<Student>(query, new { Id = id });
        }
    }

    public class GroupRepository
    {
        private readonly string _connectionString;
        public GroupRepository(string connectionString) => _connectionString = connectionString;

        public List<Group> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            // Если таблица Groups тоже имеет другие столбцы, это может выдать ошибку. 
            // Но обычно она стандартная.
            return connection.Query<Group>("SELECT Id, Name FROM Groups ORDER BY Id").AsList();
        }

        public Group GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Group>("SELECT Id, Name FROM Groups WHERE Id = @Id", new { Id = id });
        }
    }

    // 3. КОНТРОЛЛЕРЫ
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repository;

        public StudentsController(IConfiguration configuration)
        {
            _repository = new StudentRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _repository.GetById(id);
            return res == null ? NotFound() : Ok(res);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly GroupRepository _repository;

        public GroupsController(IConfiguration configuration)
        {
            _repository = new GroupRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _repository.GetById(id);
            return res == null ? NotFound() : Ok(res);
        }
    }
}