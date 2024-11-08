using Microsoft.Data.SqlClient;
using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    internal class ScoutsMeetingRepository : IRepository<ScoutsMeeting>
    {
        private readonly string _connectionString;
        public ScoutsMeetingRepository()
        {
            _connectionString = ConfigurationSettings.ConnectionString;
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
            string query = ""; // indtast SQL query her.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(new ScoutsMeeting();
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
