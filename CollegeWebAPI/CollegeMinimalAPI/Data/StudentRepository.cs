using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;

namespace CollegeMinimalAPI
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
            using var connection = new SqlConnection(_connectionString);
            
            // Читаем ваши реальные столбцы и склеиваем Имя + Фамилию через пробел
            string query = @"
                SELECT 
                    StudentID AS Id, 
                    FirstName + ' ' + LastName AS FullName, 
                    GroupID AS GroupId 
                FROM Students 
                ORDER BY StudentID";
                
            return connection.Query<Student>(query).AsList();
        }
    }
}