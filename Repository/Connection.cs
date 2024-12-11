using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Static class for managing the application's database connection
namespace SpejderApplikation.Repository
{
    public static class Connection
    {
        // Property to store the connection string
        public static string ConnectionString { get; private set; }

        // Method to initialize the connection string
        public static void Initialize(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
