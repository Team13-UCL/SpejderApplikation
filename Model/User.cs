using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// User class represents a user of the application
namespace SpejderApplikation.Model
{
    class User
    {
        public int UserID { get; set; } // Unique identifier for the user
        public string UserName { get; set; } // Username of the user
        public string Password { get; set; } // Password of the user
        public bool IsLeader { get; set; } // Indicates if the user is a leader
    }
}
