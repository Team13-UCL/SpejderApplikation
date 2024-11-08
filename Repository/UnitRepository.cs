using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    public class UnitRepository : IRepository<Unit>
    {
        private readonly string _connectionString;
        public UnitRepository()
        {
            _connectionString = ConfigurationSettings.ConnectionString;
        }

        public int AddType(Unit entity)
        {
            string query = ""; //indtast SQL query her.
            int unitId = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue(<query variable>, <type.variable>);
                //en command.Parameter pr variabel
                connection.Open();
                command.ExecuteNonQuery();
            }
            return unitId;
        }

        public void DeleteType(int id)
        {
            string query = ""; //indtast SQL query her.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue(<query variable>, id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EditType(Unit entity)
        {
            string query = ""; //indtast SQL query her.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue(<query variable>, id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Unit> GetAll()
        {
            var entities = new List<Unit>();
            string query = ""; // indtast SQL query her.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(new Unit(

                            (int)reader[""],
                            (string)reader[""],
                            (string)reader[""],
                            (byte[])reader[""]));
                    }
                }
            }

            return entities;
        }

        public Unit GetByID(int id)
        {
            Unit entity = new Unit();
            string query = "";// indtast SQL query her.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue(<query variable>, id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        entity = new Unit();
                    }
                }
            }
            if (entity == null)
                return default;
            else
                return entity;
        }
    }
}
