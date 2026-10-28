using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace AdoNetLesson7
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
            var students = new List<Student>();
            string query = "SELECT Id, FullName, GroupId FROM Students ORDER BY Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            GroupId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
                        });
                    }
                }
            }
            return students;
        }

        public List<Student> FindByName(string name)
        {
            var students = new List<Student>();
            string query = "SELECT Id, FullName, GroupId FROM Students WHERE FullName LIKE @Name ORDER BY Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", $"%{name}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                GroupId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return students;
        }
    }
}