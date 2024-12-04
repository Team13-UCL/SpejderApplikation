using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Model
{
    class User
    {
        public int UserID { get; set; }        
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsLeader { get; set; }
    }
}
