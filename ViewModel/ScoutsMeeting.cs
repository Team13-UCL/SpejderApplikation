using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.ViewModel
{
    internal class ScoutsMeeting
    {
        private Meeting dbMeeting;
        private Unit dbUnit;
        private Badge dbBadge;
        private Activity dbActivity;
        public DateOnly Date { get; set; }
        public byte[] Badge { get; set; } //billede
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
            dbMeeting.Date = date;
            Badge = badge;
            dbBadge.Picture = badge;
            BadgeName = badgeName;
            dbBadge.Name = badgeName;
            Activity = activity;
            dbActivity.ActivityDescription = activity;
            Preparation = preparation;
            dbActivity.Preparation = preparation;
            Notes = notes;
            dbActivity.Notes = notes;
            // Responsibility hvor hører det til? det er på WF, men ikke i DCD
            Unit = unit;
            dbUnit.UnitName = Unit;
            
            // ved ikke lige hvordan jeg indkorporerer ID for de forskellige objekter.
            // Men det skal bruges til at opdatere de forskellige.

        }
        public ScoutsMeeting()
        {
            // opret ny Meeting, med forud indtastet Unit hardcoded til sprint 1
        }
        public void UpdateMeeting()
        {
            if (dbMeeting.Date != Date)
            { 
                //opdater meeting database
            }
            if(dbBadge.Picture != Badge || dbBadge.Name != BadgeName)
            {
                // opdater Badge database
            }
            if(dbActivity.ActivityDescription != Activity ||
                dbActivity.Preparation != Preparation ||
                dbActivity.Notes != Notes)
            {
                // opdater Activity database
            }
        }
    }
}
