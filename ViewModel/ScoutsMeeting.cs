using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpejderApplikation.ViewModel
{
    internal class ScoutsMeeting
    {
        public int meetingID { get; set; }
        public int activityID { get; set; }
        public int badgeID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly Stop { get; set; }
        public string Time { get { return $"{Start:HH:mm} - {Stop:HH:mm}"; } }
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
        public string Responsibility { get; set; }
        public string Unit { get; set; }
        public ScoutsMeeting(DateOnly date, byte[] badge, string badgeName, 
                            string activity, string preparation, string notes, string responsibility, string unit,
                            int meetingID, int unitID, int badgeID, int activityID)
        {
            Date = date;
            BadgeData = badge;
            BadgeName = badgeName;
            Activity = activity;
            Preparation = preparation;
            Notes = notes;
            // Responsibility hvor hører det til? det er på WF, men ikke i DCD
            Unit = unit;
            
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
