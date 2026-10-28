using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;

namespace CollegeMinimalAPI
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
            using var connection = new SqlConnection(_connectionString);
            
            // Используем реальные названия столбцов из вашей БД (GroupID, GroupName) 
            // и через AS переименовываем их в Id и Name для нашего класса Group
            string query = "SELECT GroupID AS Id, GroupName AS Name FROM Groups ORDER BY GroupID";
            
            return connection.Query<Group>(query).AsList();
        }
    }
}