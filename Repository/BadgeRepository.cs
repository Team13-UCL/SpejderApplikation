﻿using Microsoft.Data.SqlClient;
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
    // Repository for handling database interactions related to Badges
    public class BadgeRepository : IRepository<Badge> //inherit from IRepository
    {
        private readonly string _connectionString;        
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
                // Adding parameters for the stored procedure
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
                        int badgeID = reader.IsDBNull(reader.GetOrdinal("BadgeID"))
                            ? 0
                            : reader.GetInt32(reader.GetOrdinal("BadgeID"));
                        string name = reader.IsDBNull(reader.GetOrdinal("BadgeName")) 
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("BadgeName"));
                        string description = reader.IsDBNull(reader.GetOrdinal("Description"))
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("Description"));
                        Byte[] picture = reader.IsDBNull(reader.GetOrdinal("Picture")) ? 
                            new byte[0] : 
                            (byte[])reader["Picture"];
                        string link = reader.IsDBNull(reader.GetOrdinal("Link")) 
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("Link"));

                        entities.Add(new Badge(badgeID, name, description, picture, link));
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
                        // Map database fields to Badge properties
                        int badgeID = reader.IsDBNull(reader.GetOrdinal("BadgeID")) 
                            ? 0
                            : reader.GetInt32(reader.GetOrdinal("BadgeID"));
                        string name = reader.IsDBNull(reader.GetOrdinal("BadgeName"))
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("BadgeName"));
                        string description = reader.IsDBNull(reader.GetOrdinal("Description"))
                            ? string.Empty 
                            : reader.GetString(reader.GetOrdinal("Description"));
                        Byte[] picture = reader.IsDBNull(reader.GetOrdinal("Picture"))
                            ? new byte[0]
                            : (byte[])reader["Picture"];
                        string link = reader.IsDBNull(reader.GetOrdinal("Link"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("Link"));

                        entity = new Badge(badgeID, name, description, picture, link);
                    }
                }
            }

            return entity ?? new Badge(); // return entity or a new Badge object if entity is null
        }

        public int AddType(Badge entity, int ID)
        {
            if (entity.Name == null || entity.Name == "")
                return 0;

            string query = "EXEC spAddBadge @ActivityID, @BadgeName, @Description, @Picture, @Link, @BadgeID, @NewBadgeID OUTPUT";

            int BadgeID = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Adding input parameters
                command.Parameters.AddWithValue("@ActivityID", ID);
                command.Parameters.AddWithValue("@BadgeName", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);

                // Handle picture input
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

                // Retrieve output value
                BadgeID = (int)outputParam.Value;
            }

            return BadgeID;
        }   

        public void ConnectTypes(Badge entity, ScoutsMeeting JoinedEntity)
        {
            string query = "spEditActivityBadge @BadgeID, @NewBadgeID, @ActivityID";

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
