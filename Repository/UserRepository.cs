using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SpejderApplikation.Model;
using SpejderApplikation.ViewModel;

namespace SpejderApplikation.Repository
{
    internal class UserRepo : IRepository<User>
    {

        private readonly string _connectionString;
        public UserRepo()
        {
            _connectionString = Connection.ConnectionString;
        }

        public int AddOrEditType(User entity, int ID)
        {
            throw new NotImplementedException();
        }

        public int AddType(User entity, int ID)
        {
            throw new NotImplementedException();
        }

        public void ConnectTypes(User entity, ScoutsMeeting JoinedEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteType(User entity)
        {
            throw new NotImplementedException();
        }

        public void EditType(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            string query = "SELECT * FROM [Users]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserID = (int)reader["UserID"],
                            UserName = (string)reader["UserName"],
                            Password = (string)reader["Password"],
                            IsLeader = (bool)reader["IsLeader"]
                        });
                    }
                }
            }

            return users;
        }

        public User GetByID(int ID)
        {
            User user = null;
            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", ID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = (int)reader["UserID"],
                            UserName = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            IsLeader = (bool)reader["IsLeader"]
                        };
                    }
                }
            }

            return user;
        }

        //public void Update(User entity)
        //{
        //    string query = "UPDATE Users " +
        //                   "SET Username = @Username, Password = @Password " +
        //                   "WHERE UserID = @UserID";

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@UserID", entity.UserID);
        //        command.Parameters.AddWithValue("@Username", entity.UserName);
        //        command.Parameters.AddWithValue("@Password", entity.Password);
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}


    }
}
