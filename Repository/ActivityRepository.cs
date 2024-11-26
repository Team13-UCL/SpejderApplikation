using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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

        public int AddOrEditType(Activity entity, int ID)
        {
            string query = "spAddOrEditActivity @ActivityID, @ActivityDescription, @Preparation, @Notes, @Activity"; //indtast SQL query her.
            int EntityID = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ActivityID", entity._activityID);
                command.Parameters.AddWithValue("@ActivityDescription", entity.ActivityDescription);
                command.Parameters.AddWithValue("@Preparation", entity.Preparation);
                command.Parameters.AddWithValue("@Notes", entity.Notes);
                command.Parameters.AddWithValue("@Activity", entity.BriefDescription);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) 
                {
                    EntityID = reader.IsDBNull(reader.GetOrdinal("ActivityID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ActivityID"));
                }
            }
            return EntityID;
        }
        public IEnumerable<Activity> GetAll()
        {
            throw new NotImplementedException();
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
                        string BriefDescription = reader.IsDBNull(reader.GetOrdinal("Activity")) ? string.Empty : reader.GetString(reader.GetOrdinal("Activity"));
                        entity = new Activity(ActivityID, Activity, Preparation, Notes, BriefDescription);
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
