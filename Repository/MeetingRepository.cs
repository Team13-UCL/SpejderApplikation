using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    internal class MeetingRepository : IRepository<Meeting>
    {
        private readonly string _connectionString;
        public MeetingRepository()
        {
            _connectionString = Connection.ConnectionString;
        }

        public int AddType(Meeting entity)
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

        public void EditType(Meeting entity)
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

        public IEnumerable<Meeting> GetAll()
        {
            var entities = new List<Meeting>();
            string query = "SELECT * FROM MEETING"; // indtast SQL query her.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime dateTime = (DateTime)reader["Date"];
                        TimeSpan start = (TimeSpan)reader["Start"];
                        TimeSpan stop = (TimeSpan)reader["Stop"];
                        entities.Add(new Meeting(

                            (int)reader["MeetingID"],
                            DateOnly.FromDateTime(dateTime),
                            TimeOnly.FromTimeSpan(start),
                            TimeOnly.FromTimeSpan(stop)));
                    }
                }
            }

            return entities;
        }

        public Meeting GetByID(int id)
        {
            Meeting entity = null; // Start with null to handle cases where no record is found.
            string query = "SELECT * FROM Meeting WHERE MeetingID = @MeetingID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MeetingID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int meetingID = reader.IsDBNull(reader.GetOrdinal("MeetingID"))
                                        ? 0
                                        : reader.GetInt32(reader.GetOrdinal("MeetingID"));

                        DateTime date = reader.IsDBNull(reader.GetOrdinal("Date"))
                                        ? DateTime.MinValue
                                        : reader.GetDateTime(reader.GetOrdinal("Date"));

                        TimeSpan start = reader.IsDBNull(reader.GetOrdinal("Start"))
                                         ? TimeSpan.Zero
                                         : reader.GetTimeSpan(reader.GetOrdinal("Start"));

                        TimeSpan stop = reader.IsDBNull(reader.GetOrdinal("Stop"))
                                        ? TimeSpan.Zero
                                        : reader.GetTimeSpan(reader.GetOrdinal("Stop"));

                        // Creating the Meeting object with the retrieved data.
                        entity = new Meeting(meetingID, DateOnly.FromDateTime(date), TimeOnly.FromTimeSpan(start), TimeOnly.FromTimeSpan(stop));
                    }
                }
            }

            return entity; // If entity is null, the caller should handle the null value appropriately.
        }
    }
}
