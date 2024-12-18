﻿using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace SpejderApplikation.Repository
{
    public class UnitRepository : IRepository<Unit>
    {
        private readonly string _connectionString;
        public UnitRepository()
        {
            _connectionString = Connection.ConnectionString;
        }
        public UnitRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void DeleteType(Unit entity)
        {
            string query = ""; //indtast SQL query her.

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);                
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EditType(Unit entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Unit> GetAll()
        {
            var entities = new List<Unit>();
            string query = "SELECT * FROM Unit"; // indtast SQL query her.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(new Unit(
                            (int)reader["UnitID"],
                            (string)reader["UnitName"],
                            (string)reader["Description"],
                            (string)reader["Link"]));
                    }
                }
            }

            return entities;
        }

        public Unit GetByID(int id)
        {
            Unit entity = new Unit();
            string query = "spSelectUnit @UnitID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UnitID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    { // uses a tenary operator to check for null values
                        int unitID = reader.IsDBNull(reader.GetOrdinal("UnitID")) ? 0 : (int)reader["UnitID"];
                        string unitName = reader.IsDBNull(reader.GetOrdinal("UnitName")) ? string.Empty : (string)reader["UnitName"];
                        string description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : (string)reader["Description"];
                        string unitLink = reader.IsDBNull(reader.GetOrdinal("Link")) ? string.Empty : (string)reader["Link"];                        
                        entity = new Unit(unitID, unitName, description, unitLink);
                    }
                }
            }
            if (entity == null)
                return default;
            else
                return entity;
        }

        public int AddType(Unit entity, int ID)
        {
            string query = "EXEC spAddUnit @ActivityID, @UnitID";

            if (entity._unitID == null || entity._unitID == 0)
                return 0;
            int ActivityID = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Tilføj input-parametre
                command.Parameters.AddWithValue("@ActivityID", ID);
                command.Parameters.AddWithValue("@UnitID", entity._unitID);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return entity._unitID;
        }

        public void ConnectTypes(Unit entity, ScoutsMeeting JoinedEntity)
        {
            string query = "spEditUnit @UnitID, @NewUnitID, @ActivityID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UnitID", JoinedEntity.unitID);
                command.Parameters.AddWithValue("@NewUnitID", entity._unitID);
                command.Parameters.AddWithValue("@ActivityID", JoinedEntity.activityID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
