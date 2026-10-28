using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using ado.net.Models;

namespace ado.net.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAll()
        {
            string query = "SELECT Id, FullName, GroupId FROM Students ORDER BY Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Student>(query).AsList();
            }
        }

        public Student GetById(int id)
        {
            string query = "SELECT Id, FullName, GroupId FROM Students WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Student>(query, new { Id = id });
            }
        }

        public List<Student> FindByName(string name)
        {
            string query = "SELECT Id, FullName, GroupId FROM Students WHERE FullName LIKE @Name ORDER BY Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Student>(query, new { Name = $"%{name}%" }).AsList();
            }
        }

        public Student GetByExactName(string fullName)
        {
            string query = "SELECT Id, FullName, GroupId FROM Students WHERE FullName = @Name";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingle<Student>(query, new { Name = fullName });
            }
        }

        // Пункт 4 и 5: Метод добавления студента
        public int CreateStudent(Student student)
        {
            // OUTPUT INSERTED.Id позволяет получить сгенерированный базой данных ID
            string query = @"
                INSERT INTO Students (FullName, GroupId) 
                OUTPUT INSERTED.Id 
                VALUES (@FullName, @GroupId)";

            using (var connection = new SqlConnection(_connectionString))
            {
                // QuerySingle<int> ожидает ровно один результат — ID новой строки
                return connection.QuerySingle<int>(query, student);
            }
        }
    }
}