using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Model
{
    internal class Activity
    {
        public int _activityID { get; private set; }
        public string ActivityDescription { get; set; }
        public string Preparation { get; set; }
        public string Notes { get; set; }

        // Activity Konstruktor der initialiser alle egenskaber fra properties over os.
        public Activity(int ID, string description, string preparation, string notes)
        {
            _activityID = ID;
            ActivityDescription = description;
            Preparation = preparation;
            Notes = notes;
        }
        // ???
        public Activity()
        {
            
        }
        public void UpdateID(int ID)
        {
            _activityID = ID;
        }
    }
}
