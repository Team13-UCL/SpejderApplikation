using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Meeting class represents a scheduled meeting
namespace SpejderApplikation.Model
{
    public class Meeting
    {
        public int _meetingID { get; private set; } // Unique identifier for the meeting
        public DateOnly Date { get; set; } // Date of the meeting
        public TimeOnly Start { get; set; } // Start time of the meeting
        public TimeOnly Stop { get; set; } // End time of the meeting

        // Constructor to initialize all properties
        public Meeting(int id, DateOnly date, TimeOnly start, TimeOnly stop)
        {
            _meetingID = id;
            Date = date;
            Start = start;
            Stop = stop;
        }

        // Parameterless constructor for default initialization
        public Meeting()
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
        }

        // Updates the meeting ID
        public void UpdateID(int ID)
        {
            _meetingID = ID;
        }
    }
}
