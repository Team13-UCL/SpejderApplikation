using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Model
{
    internal class Meeting
    {
        private int _meetingID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly Stop { get; set; }

        public TimeOnly hej { get; set; }
        public Meeting(int id, DateOnly date, TimeOnly start, TimeOnly stop)
        {
            _meetingID = id;
            Date = date;
            Start = start;
            Stop = stop;
        }
        public Meeting()
        {
            
        }
    }
}
