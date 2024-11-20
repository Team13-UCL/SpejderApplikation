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
        // DateOnly og TimeOnly bruges fordi vi gerne vil specifikere tidspunkt og dato, og en dato kan også have flere møder

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
