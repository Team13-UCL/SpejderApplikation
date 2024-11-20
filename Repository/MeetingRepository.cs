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
            Meeting entity = new Meeting();
            string query = "SELECT * FROM Meeting WHERE MeetingID = @MeetingID";// indtast SQL query her.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MeetingID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //int MeetingID = reader.IsDBNull(reader.GetOrdinal("ActivityID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ActivityID"));
                        //DateTime Date = reader.IsDBNull(reader.GetOrdinal("Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Date"));
                        //TimeOnly Activity = reader.IsDBNull(reader.GetOrdinal("ActivityDescription")) ? DateTime.MinValue : reader.GetString(reader.GetOrdinal("ActivityDescription"));
                        //TimeOnly Preparation = reader.IsDBNull(reader.GetOrdinal("Preparation")) ? string.Empty : reader.GetString(reader.GetOrdinal("Preparation"));
                        //entity = new Meeting(MeetingID, Date, Start, Stop);
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
