using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    public class BadgeRepository : IRepository<Badge>
    {
        private readonly string _connectionString;
        string filePath = Directory.GetCurrentDirectory();
        string fileName = "\\KFUM.png"; // har et basis KFUM mærke i projektets mappe
        public BadgeRepository()
        {
            _connectionString = Connection.ConnectionString;
        }
        public BadgeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void DeleteType(Badge entity)
        {
            throw new NotImplementedException();
        }

        public void EditType(Badge entity)
        {
            string query = "spEditBadge @BadgeID, @Name, @Description, @Picture, @Link"; //indtast SQL query her.
            int EntityID = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BadgeID", entity._badgeID);
                command.Parameters.AddWithValue("@Name", entity.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Description", entity.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Picture", entity.Picture ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Link", entity.Link ?? (object)DBNull.Value);
                connection.Open();
                command.ExecuteNonQuery();
            }
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
            string query = "spSelectBadge @BadgeID";

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

        public int AddType(Badge entity, int ID)
        {
            string query = "EXEC spAddBadge @ActivityID, @BadgeName, @Description, @Picture, @Link, @BadgeID, @NewBadgeID OUTPUT";

            int BadgeID = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Tilføj input-parametre
                command.Parameters.AddWithValue("@ActivityID", ID);
                command.Parameters.AddWithValue("@BadgeName", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);

                var pictureParam = new SqlParameter("@Picture", SqlDbType.VarBinary);
                if (entity.Picture != null && entity.Picture.Length > 0)
                {
                    pictureParam.Value = entity.Picture;
                }
                else
                {
                    pictureParam.Value = DBNull.Value;
                }
                command.Parameters.Add(pictureParam);

                command.Parameters.AddWithValue("@Link", entity.Link ?? (object)DBNull.Value); // Håndter null for Link
                command.Parameters.AddWithValue("@BadgeID", entity._badgeID);

                // Output-parameter
                SqlParameter outputParam = new SqlParameter("@NewBadgeID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                connection.Open();
                command.ExecuteNonQuery();

                // Hent værdien fra output-parametret
                BadgeID = (int)outputParam.Value;
            }

            return BadgeID;
        }

        public void ConnectTypes(Badge entity, ScoutsMeeting JoinedEntity)
        {
            string query = "spEditActivityBadge @BadgeID, @NewBadgeID, @ActivityID"; //indtast SQL query her.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BadgeID", JoinedEntity.badgeID);
                command.Parameters.AddWithValue("@NewBadgeID", entity._badgeID);
                command.Parameters.AddWithValue("@ActivityID", JoinedEntity.activityID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
