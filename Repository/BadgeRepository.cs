using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    internal class BadgeRepository : IRepository<Badge>
    {
        private readonly string _connectionString;
        string filePath = Directory.GetCurrentDirectory();
        string fileName = "\\KFUM.png"; // har et basis KFUM mærke i projektets mappe
        public BadgeRepository()
        {
            _connectionString = Connection.ConnectionString;
        }
        public int AddType(Badge entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteType(Badge badge)
        {
            throw new NotImplementedException();
        }

        public void EditType(Badge entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Badge> GetAll()
        {
            var entities = new List<Badge>();
            
            string query = "SELECT * FROM Badge"; // indtast SQL query her.
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


                        entities.Add(new Badge((int)reader["BadgeID"],
                                                (string)reader["BadgeName"],
                                                (string)reader["Description"],
                                                picture,
                                                (string)reader["Link"]));
                    }
                }
            }

            return entities;
        }

        public Badge GetByID(int id)
        {
            Badge entity = null;
            string query = "SELECT * FROM Badge WHERE BadgeID = @BadgeID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BadgeID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int badgeID = reader.IsDBNull(reader.GetOrdinal("BadgeID")) ? 0 : reader.GetInt32(reader.GetOrdinal("BadgeID"));
                        string name = reader.IsDBNull(reader.GetOrdinal("BadgeName")) ? string.Empty : reader.GetString(reader.GetOrdinal("BadgeName"));
                        string description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
                        Byte[] picture = reader.IsDBNull(reader.GetOrdinal("Picture")) ? new byte[0] : (byte[])reader["Picture"];
                        string link = reader.IsDBNull(reader.GetOrdinal("Link")) ? string.Empty : reader.GetString(reader.GetOrdinal("Link"));

                        entity = new Badge(badgeID, name, description, picture, link);
                    }
                }
            }

            return entity ?? new Badge(); // Returér en tom instans, hvis intet blev fundet.
        }
    }
}
