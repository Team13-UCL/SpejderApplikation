using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SpejderApplikation.MVVM;
using SpejderApplikation.DataHandler;

// ScoutsMeeting ViewModel represents a detailed meeting in the application and implements the INotifyPropertyChanged interface for data binding
namespace SpejderApplikation.ViewModel
{
    public class ScoutsMeeting : ViewModelBase
    {
        public int meetingID { get; set; } // Unique identifier for the meeting
        public int activityID { get; set; } // Associated activity identifier
        public int badgeID { get; set; } // Badge identifier related to the meeting
        public int unitID { get; set; } // Unit identifier related to the meeting

        public TimeOnly _start; // Start time of the meeting
        public TimeOnly Start 
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Time)); // Notify Time property as it depends on Start
            }
        }

        public TimeOnly _stop; // End time of the meeting
        public TimeOnly Stop 
        {
            get => _stop;
            set
            {
                _stop = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Time)); // Notify Time property as it depends on Stop
            }
        }
        //public string Time { get { return $"{Start:HH:mm} - {Stop:HH:mm}"; } }


        private byte[] _badgeData; // Stores badge data in byte format
        public byte[] BadgeData
        {
            get => _badgeData;
            set
            {
                if (_badgeData != value)
                {
                    _badgeData = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Picture)); // Notify Picture property as it depends on BadgeData
                }
            }
        }
        private DateOnly _date;
        public DateOnly Date 
        { 
            get => _date; 
            set 
            { 
                _date = value;
                OnPropertyChanged();
            } 
        }
        // Displays the time range as a string
        public string Time
        {
            get { return $"{Start:HH:mm} - {Stop:HH:mm}"; }
            set
            {
                // Parses the input string to extract start and stop times
                var times = value.Split(" - ");
                if (times.Length == 2 &&
                    TimeOnly.TryParse(times[0], out var startTime) &&
                    TimeOnly.TryParse(times[1], out var stopTime))
                {
                    Start = startTime;
                    Stop = stopTime;
                }
            }
        }

        // Converts badge data to an image source for display
        public ImageSource Picture
        {
            get
            {
                if (BadgeData == null) return null;

                try
                {
                    var imageHandling = new ImageHandling();
                    return imageHandling.LoadSvg(BadgeData);
                }
                catch (Exception)
                {
                    // Log the exception if necessary
                    return null;
                }
            }
        }

        public string BadgeName { get; set; } // Name of the badge associated with the meeting


        private string _activity; // Activity description
        public string Activity 
        { 
            get => _activity; 
            set
            {
                _activity = value;
                OnPropertyChanged();
            }
        }
        public string Preparation { get; set; } // Preparation details for the activity


        private string _notes; // Notes related to the meeting
        public string Notes 
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }
        public string Unit { get; set; } // Unit name associated with the meeting

        // Constructor to initialize all properties
        public ScoutsMeeting(DateOnly date, TimeOnly start, TimeOnly stop, byte[] badge, 
                            string activity, string notes, string unit,
                            int meetingID, int unitID, int badgeID, int activityID)
        {
            Date = date;
            Start = start;
            Stop = stop;
            BadgeData = badge;
            Activity = activity;
            Notes = notes;
            Unit = unit;
            this.badgeID = badgeID;
            this.activityID = activityID;
            this.meetingID = meetingID;
            this.unitID = unitID;

        }

        // Default constructor
        public ScoutsMeeting()
        {
            // opret ny Meeting, med forud indtastet Unit hardcoded til sprint 1
        }

        // Updates identifiers for the meeting
        public void UpdateID(int ActivityID, int BadgeID, int UnitID, int MeetingID)
        {
            badgeID = BadgeID;
            activityID = ActivityID;
            unitID = UnitID;
            meetingID = MeetingID;
        }

    }
}
