﻿using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
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
        public ScoutsMeetingRepository()
        {
            _connectionString = Connection.ConnectionString;
        }
        public ScoutsMeetingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void EditType(ScoutsMeeting entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ScoutsMeeting> GetAll()
        {
            var entities = new List<ScoutsMeeting>();
            string filePath = Directory.GetCurrentDirectory();
            string fileName = "\\KFUM.png"; // har et basis KFUM mærke i projektets mappe
            string query = "spGetAllScoutmeetings"; // indtast SQL query her.
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

            return entities;
        }


        public ScoutsMeeting GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int AddType(ScoutsMeeting entity, int ID)
        {
            throw new NotImplementedException();
        }

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
    }
}
