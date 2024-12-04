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

namespace SpejderApplikation.ViewModel
{
    public class ScoutsMeeting : ViewModelBase
    {
        public int meetingID { get; set; }
        public int activityID { get; set; }
        public int badgeID { get; set; }
        public int unitID { get; set; }
        public TimeOnly _start;
        public TimeOnly Start 
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Time));
            }
        }
        public TimeOnly _stop;
        public TimeOnly Stop 
        {
            get => _stop;
            set
            {
                _stop = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Time));
            }
        }
        //public string Time { get { return $"{Start:HH:mm} - {Stop:HH:mm}"; } }
        private byte[] _badgeData;
        public byte[] BadgeData
        {
            get => _badgeData;
            set
            {
                if (_badgeData != value)
                {
                    _badgeData = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Picture)); // notificerer at Picture er ændret
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
        public string Time
        {
            get { return $"{Start:HH:mm} - {Stop:HH:mm}"; }
            set
            {
                // Forvent format som "HH:mm - HH:mm"
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
        public string BadgeName { get; set; } //behøves ikke
        private string _activity;
        public string Activity 
        { 
            get => _activity; 
            set
            {
                _activity = value;
                OnPropertyChanged();
            }
        }
        public string Preparation { get; set; }
        private string _notes;
        public string Notes 
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }
        public string Unit { get; set; }
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

        public ScoutsMeeting()
        {
            // opret ny Meeting, med forud indtastet Unit hardcoded til sprint 1
        }
        public void UpdateID(int ActivityID, int BadgeID, int UnitID, int MeetingID)
        {
            badgeID = BadgeID;
            activityID = ActivityID;
            unitID = UnitID;
            meetingID = MeetingID;
        }

    }
}
