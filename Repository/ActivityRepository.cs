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

        public void DeleteType(Activity entity)
        {
            throw new NotImplementedException();
        }

        public void AddOrEditType(Activity entity)
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
            string query = "spSelectActivity @ActivityId";// indtast SQL query her.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Activityid", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int ActivityID = reader.IsDBNull(reader.GetOrdinal("ActivityID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ActivityID"));
                        string Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : reader.GetString(reader.GetOrdinal("Notes"));
                        string Activity = reader.IsDBNull(reader.GetOrdinal("ActivityDescription")) ? string.Empty : reader.GetString(reader.GetOrdinal("ActivityDescription"));
                        string Preparation = reader.IsDBNull(reader.GetOrdinal("Preparation")) ? string.Empty : reader.GetString(reader.GetOrdinal("Preparation"));
                        entity = new Activity(ActivityID, Activity, Preparation, Notes);
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
