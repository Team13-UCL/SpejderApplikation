using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    public class ActivityRepository : IRepository<Activity>
    {
        private readonly string _connectionString;

        // Default constructor initializing the connection string
        public ActivityRepository()
        {
            _connectionString = Connection.ConnectionString;
        }

        // Constructor allowing custom connection string
        public ActivityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to delete an activity (not implemented yet)
        public void DeleteType(Activity entity)
        {
            throw new NotImplementedException();
        }

        // Method to update an existing activity in the database
        public void EditType(Activity entity)
        {
            string query = "EXEC spEditActivity @ActivityID, @ActivityDescription, @Preparation, @Notes, @Activity"; //indtast SQL query her.
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Adding parameters for the stored procedure
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ActivityID", entity._activityID);
                command.Parameters.AddWithValue("@ActivityDescription", (object)entity.ActivityDescription ?? DBNull.Value);
                command.Parameters.AddWithValue("@Preparation", (object)entity.Preparation ?? DBNull.Value);
                command.Parameters.AddWithValue("@Notes", (object)entity.Notes ?? DBNull.Value);
                command.Parameters.AddWithValue("@Activity", (object)entity.BriefDescription ?? DBNull.Value);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Method to retrieve all activities (not implemented yet)
        public IEnumerable<Activity> GetAll()
        {
            throw new NotImplementedException();
        }

        // Method to retrieve a specific activity by its ID
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
                        // Map database fields to Activity properties
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

        // Method to add a new activity to the database
        public int AddType(Activity entity, int ID)
        {
            string query = "EXEC spAddActivity @Activity, @Description, @Preparation, @Notes, @ActivityID OUTPUT";

            int ActivityID = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add input-parametre
                command.Parameters.AddWithValue("@Activity", entity.BriefDescription);
                command.Parameters.AddWithValue("@Description", entity.ActivityDescription);
                command.Parameters.AddWithValue("@Preparation", entity.Preparation);
                command.Parameters.AddWithValue("@Notes", entity.Notes);
                // Adding output parameter
                SqlParameter outputParam = new SqlParameter("@ActivityID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);
                connection.Open();
                command.ExecuteNonQuery();
                // Retrieve the output value
                ActivityID = (int)outputParam.Value;
            }
            return ActivityID;
        }

        // Method to connect activities to other entities (not implemented yet)
        public void ConnectTypes(Activity entity, ScoutsMeeting JoinedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
