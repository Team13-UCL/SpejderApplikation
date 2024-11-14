using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    internal class ActivityRepository : IRepository<Activity>
    {
        private readonly string _connectionString;
        public ActivityRepository()
        {
            _connectionString = Connection.ConnectionString;
        }

        public int AddType(Activity entity)
        {
            string query = ""; //indtast SQL query her.
            int entityID = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue(<query variable>, <type.variable>);
                //en command.Parameter pr variabel
                connection.Open();
                command.ExecuteNonQuery();
            }
            return entityID;
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

        public void EditType(Activity entity)
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

        public IEnumerable<Activity> GetAll()
        {
            var entities = new List<Activity>();
            string query = ""; // indtast SQL query her.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(new Activity(

                            (int)reader[""],
                            (string)reader[""],
                            (string)reader[""],
                            (string)reader[""]));
                    }
                }
            }

            return entities;
        }

        public Activity GetByID(int id)
        {
            Activity entity = new Activity();
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
                        entity = new Activity();
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
