using Microsoft.Data.SqlClient;
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
    internal class ScoutsMeetingRepository : IRepository<ScoutsMeeting>
    {
        private readonly string _connectionString;
        public ScoutsMeetingRepository()
        {
            _connectionString = Connection.ConnectionString;
        }

        public int AddType(ScoutsMeeting entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteType(int id)
        {
            throw new NotImplementedException();
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
                        Byte[] picture;
                        if (reader["Picture"] != null)
                        { picture = reader["Picture"] as byte[]; }
                        else if (File.Exists(filePath) == true)
                        {
                            picture = File.ReadAllBytes(string.Concat(filePath, fileName));
                        }
                        else
                        {
                            picture = new byte[0];
                        }


                        entities.Add(new ScoutsMeeting(DateOnly.FromDateTime(dateTime),
                            TimeOnly.FromTimeSpan(start),
                            TimeOnly.FromTimeSpan(stop),
                            picture,
                            (string)reader["ActivityDescription"],
                            (string)reader["Notes"],
                            (string)reader["UnitName"], // Unit
                            (int)reader["MeetingID"],
                            (int)reader["UnitID"], // UnitID
                            (int)reader["BadgeID"],
                            (int)reader["ActivityID"]));
                        //{
                        //    meetingID = (int)reader["MeetingID"],
                        //    Date = (DateOnly)reader["Date"],
                        //    activityID = (int)reader["ActivityID"],
                        //    Activity = (string)reader["Activity"],
                        //    badgeID = (int)reader["BadgeID"],
                        //    Badge = (Byte[])reader["Picture"],
                        //    BadgeName = (string)reader["BadgeName"],

                        //});
                    }
                }
            }

            return entities;
        }

        public ScoutsMeeting GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
