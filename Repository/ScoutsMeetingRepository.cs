using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    public class ScoutsMeetingRepository : IRepository<ScoutsMeeting>
    {
        private readonly string _connectionString;

        // Constructor initializes connection string from a static class or passed argument
        public ScoutsMeetingRepository()
        {
            _connectionString = Connection.ConnectionString;
        }
        public ScoutsMeetingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // EditType method is not implemented
        public void EditType(ScoutsMeeting entity)
        {
            throw new NotImplementedException();
        }

        // GetAll retrieves all scout meetings from the database
        public IEnumerable<ScoutsMeeting> GetAll()
        {
            var entities = new List<ScoutsMeeting>(); // List to hold the retrieved entities
            string filePath = Directory.GetCurrentDirectory();
            string fileName = "\\KFUM.png"; // Path to default KFUM image
            string query = "spGetAllScoutmeetings"; // SQL query to get all scout meetings
            using (SqlConnection connection = new SqlConnection(_connectionString)) // SQL query to get all scout meetings
            {
                SqlCommand command = new SqlCommand(query, connection); // Execute query and read results
                {
                    connection.Open(); // Open the connection

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                            // Read data from reader and handle potential null values
                        DateTime dateTime = (DateTime)reader["Date"];
                        TimeSpan start = (TimeSpan)reader["Start"];
                        TimeSpan stop = (TimeSpan)reader["Stop"];
                        Byte[] picture = reader.IsDBNull(reader.GetOrdinal("Picture")) ? null : (Byte[])reader["Picture"];
                        
                        //if (reader["Picture"] != null)
                        //{ picture = reader["Picture"] as byte[]; }
                        //else if (File.Exists(filePath) == true)
                        //{
                        //    picture = File.ReadAllBytes(string.Concat(filePath, fileName));
                        //}
                        //else
                        //{
                        //    picture = new byte[0];
                        //}
                        string Activity = reader.IsDBNull(reader.GetOrdinal("Activity")) ? string.Empty : (string)reader["Activity"];
                        string Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : (string)reader["Notes"];
                        string UnitName = reader.IsDBNull(reader.GetOrdinal("UnitName")) ? string.Empty : (string)reader["UnitName"];
                        int unitID = reader.IsDBNull(reader.GetOrdinal("UnitID")) ? 0 : (int)reader["UnitID"];
                        int MeetingID = reader.IsDBNull(reader.GetOrdinal("MeetingID")) ? 0 : (int)reader["MeetingID"];
                        int BadgeID = reader.IsDBNull(reader.GetOrdinal("BadgeID")) ? 0 : (int)reader["BadgeID"];
                        int ActivityID = reader.IsDBNull(reader.GetOrdinal("ActivityID")) ? 0 : (int)reader["ActivityID"];

                        entities.Add(new ScoutsMeeting(DateOnly.FromDateTime(dateTime),
                            TimeOnly.FromTimeSpan(start),
                            TimeOnly.FromTimeSpan(stop),
                            picture,
                            Activity,
                            Notes,
                            UnitName,
                            MeetingID,
                            unitID,
                            BadgeID,
                            ActivityID));
                    }
                }
            }

            return entities; // Return the list of scout meetings
            }
        }


        // GetByID retrieves a single scout meeting by its ID
        public ScoutsMeeting GetByID(int id)
        {
            ScoutsMeeting entity = null; // Ensure initialization.
            string query = "spGetScoutmeetingByID"; // Name of the stored procedure.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ActivityID", id);

                connection.Open(); // Open connection to database

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Read and map data to object fields
                        DateTime dateTime = reader.IsDBNull(reader.GetOrdinal("Date")) 
                            ? DateTime.MinValue 
                            : reader.GetDateTime(reader.GetOrdinal("Date"));
                        DateOnly Date = DateOnly.FromDateTime(dateTime);
                        TimeOnly start = reader.IsDBNull(reader.GetOrdinal("Start")) 
                            ? TimeOnly.MinValue 
                            : TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("Start)")));
                        TimeOnly stop = reader.IsDBNull(reader.GetOrdinal("Stop")) 
                            ? TimeOnly.MinValue 
                            : TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("Stop")));
                        byte[] picture = reader.IsDBNull(reader.GetOrdinal("BadgePicture")) 
                            ? null 
                            : (byte[])reader["BadgePicture"];

                        string activity = reader.IsDBNull(reader.GetOrdinal("ActivityDescription")) 
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("ActivityDescription"));
                        string notes = reader.IsDBNull(reader.GetOrdinal("Notes")) 
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("Notes"));
                        string unitName = reader.IsDBNull(reader.GetOrdinal("UnitName")) 
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("UnitName"));

                        int unitID = reader.IsDBNull(reader.GetOrdinal("UnitID")) 
                            ? 0 
                            : reader.GetInt32(reader.GetOrdinal("UnitID"));
                        int meetingID = reader.IsDBNull(reader.GetOrdinal("MeetingID")) 
                            ? 0 
                            : reader.GetInt32(reader.GetOrdinal("MeetingID"));
                        int badgeID = reader.IsDBNull(reader.GetOrdinal("BadgeID")) 
                            ? 0 
                            : reader.GetInt32(reader.GetOrdinal("BadgeID"));
                        int activityID = reader.IsDBNull(reader.GetOrdinal("ActivityID")) 
                            ? 0 
                            : reader.GetInt32(reader.GetOrdinal("ActivityID"));

                        // Create the entity object based on the fetched data
                        entity = new ScoutsMeeting(
                            DateOnly.FromDateTime(dateTime),
                            start,
                            stop,
                            picture,
                            activity,
                            notes,
                            unitName,
                            meetingID,
                            unitID,
                            badgeID,
                            activityID
                        );
                    }
                }
            }

            return entity ?? default; // Return default if no record found.
        }

        // AddType method is not implemented
        public int AddType(ScoutsMeeting entity, int ID)
        {
            throw new NotImplementedException();
        }

        // DeleteType deletes a scout meeting record from the database
        public void DeleteType(ScoutsMeeting entity)
        {
            string query = "spDeleteScoutMeeting @ActivityID, @MeetingID, @UnitID, @BadgeID";
            if(entity.meetingID == null)
                entity.meetingID = 0;
            if(entity.unitID == null)
                entity.unitID = 0;
            if(entity.badgeID == null)
                entity.badgeID = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ActivityID", entity.activityID);
                command.Parameters.AddWithValue("@MeetingID", entity.meetingID);
                command.Parameters.AddWithValue("@UnitID", entity.unitID);
                command.Parameters.AddWithValue("@BadgeID", entity.badgeID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // ConnectTypes method is not implemented
        public void ConnectTypes(ScoutsMeeting entity, ScoutsMeeting JoinedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
