﻿using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    public class MeetingRepository : IRepository<Meeting>
    {
        private readonly string _connectionString;
        public MeetingRepository()
        {
            _connectionString = Connection.ConnectionString;
        }
        public MeetingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void DeleteType(Meeting entity)
        {
            throw new NotImplementedException();
        }
        public void EditType(Meeting entity)
        {
            string query = "spEditMeeting @MeetingID, @Date, @Start, @Stop"; //indtast SQL query her.
            int EntityID = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MeetingID", entity._meetingID);
                command.Parameters.AddWithValue("@Date", entity.Date);
                command.Parameters.AddWithValue("@Start", entity.Start);
                command.Parameters.AddWithValue("@Stop", entity.Stop);
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
            string query = "spSelectMeeting @MeetingID";

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

        public int AddType(Meeting entity, int ID)
        {
            string query = "EXEC spAddMeeting @ActivityID, @Date, @Start, @Stop, @MeetingID, @NewMeetingID OUTPUT";

            int MeetingID = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Tilføj input-parametre
                command.Parameters.AddWithValue("@Date", entity.Date);
                command.Parameters.AddWithValue("@Start", entity.Start);
                command.Parameters.AddWithValue("@Stop", entity.Stop);
                command.Parameters.AddWithValue("@MeetingID", entity._meetingID);
                command.Parameters.AddWithValue("@ActivityID", ID);
                SqlParameter outputParam = new SqlParameter("@NewMeetingID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);
                connection.Open();
                command.ExecuteNonQuery();
                MeetingID = (int)outputParam.Value;
            }
            return MeetingID; ;
        }

        public void ConnectTypes(Meeting entity, ScoutsMeeting JoinedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
