using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using ado.net.Models;

namespace ado.net.Data
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
            string query = "SELECT Id, Name FROM Groups ORDER BY Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Group>(query).AsList();
            }
        }

        public Group GetById(int id)
        {
            string query = "SELECT Id, Name FROM Groups WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Group>(query, new { Id = id });
            }
        }

        public List<Group> FindByName(string name)
        {
            string query = "SELECT Id, Name FROM Groups WHERE Name LIKE @Name ORDER BY Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Group>(query, new { Name = $"%{name}%" }).AsList();
            }
        }

        public Group GetByExactName(string exactName)
        {
            string query = "SELECT Id, Name FROM Groups WHERE Name = @Name";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingle<Group>(query, new { Name = exactName });
            }
        }
    }
}