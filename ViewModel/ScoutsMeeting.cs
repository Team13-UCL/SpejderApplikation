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

namespace SpejderApplikation.ViewModel
{
    internal class ScoutsMeeting
    {
        public ObservableCollection<object> Mark {  get; set; }
        public int meetingID { get; set; }
        public int activityID { get; set; }
        public int badgeID { get; set; }
        public int unitID { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly Stop { get; set; }
        public DateOnly Date { get; set; }
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

        public byte[] BadgeData { get; set; } //billede
        public ImageSource Picture
        {
            get
            {
                if (BadgeData == null) return null;

                using (var ms = new MemoryStream(BadgeData))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            
        }
        public string BadgeName { get; set; }
        public string Activity { get; set; }
        public string Preparation { get; set; }
        public string Notes { get; set; }
        public string Unit { get; set; }
        public ScoutsMeeting(DateOnly date, TimeOnly start, TimeOnly stop, byte[] badge, string badgeName, 
                            string activity, string preparation, string notes, string unit,
                            int meetingID, int unitID, int badgeID, int activityID)
        {
            Date = date;
            Start = start;
            Stop = stop;
            BadgeData = badge;
            BadgeName = badgeName;
            Activity = activity;
            Preparation = preparation;
            Notes = notes;
            Unit = unit;
            this.badgeID = badgeID;
            this.activityID = activityID;
            this.meetingID = meetingID;
            this.unitID = unitID;

            Mark = new ObservableCollection<object>();
            Mark.Add(badge);
            Mark.Add(badgeName);
            
            // ved ikke lige hvordan jeg indkorporerer ID for de forskellige objekter.
            // Men det skal bruges til at opdatere de forskellige.

        }

        public ScoutsMeeting()
        {
            // opret ny Meeting, med forud indtastet Unit hardcoded til sprint 1
        }
        //public Meeting UpdateMeeting()
        //{
        //    if (dbMeeting.Date != Date)
        //    {
        //        return dbMeeting;
        //    }
        //    else return null;
        //    if(dbBadge.Picture != Badge || dbBadge.Name != BadgeName)
        //    {
        //        // opdater Badge database
        //    }
            
        //}
        //public Activity UpdateActivity()
        //{
        //    if (dbActivity.ActivityDescription != Activity || dbActivity.Preparation != Preparation || dbActivity.Notes != Notes)
        //    {
        //        return dbActivity;
        //    }
        //    else return null;
        //}
        //public Badge UpdateBadge()
        //{
        //    if (dbBadge.Picture != Badge || dbBadge.Name != BadgeName)
        //    {
        //        return dbBadge;
        //    }
        //    else return null;
        //}
    }
}
