using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using AdoNetLesson8.Models;

namespace AdoNetLesson8.Data
{
    public class GroupRepository
    {
        private readonly string _connectionString;

        public GroupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Group> GetAll()
        {
            var groups = new List<Group>();
            string query = "SELECT Id, Name FROM Groups ORDER BY Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        groups.Add(new Group
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return groups;
        }

        public List<Group> FindByName(string name)
        {
            var groups = new List<Group>();
            string query = "SELECT Id, Name FROM Groups WHERE Name LIKE @Name ORDER BY Id";

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
                            groups.Add(new Group
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return groups;
        }
    }
}