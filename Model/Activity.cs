using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Activity class represents a task or event in the application
namespace SpejderApplikation.Model
{
    public class Activity
    {
        public int _activityID { get; private set; } // Unique identifier for the activity
        public string ActivityDescription { get; set; } // Detailed description of the activity
        public string Preparation { get; set; } // Required preparation for the activity
        public string Notes { get; set; } // Additional notes for the activity
        public string BriefDescription { get; set; } // Short description or title of the activity

        // Constructor to initialize all properties
        public Activity(int ID, string description, string preparation, string notes, string Activity)
        {
            _activityID = ID;
            ActivityDescription = description;
            Preparation = preparation;
            Notes = notes;
            BriefDescription = Activity;
        }

        // Parameterless constructor for default initialization
        public Activity(){ }

        // Updates the activity ID
        public void UpdateID(int ID)
        {
            _activityID = ID;
        }
    }
}
