﻿using SpejderApplikation.Model;
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
    internal class ScoutsMeeting : ViewModelBase
    {
        public int meetingID { get; set; }
        public int activityID { get; set; }
        public int badgeID { get; set; }
        public int unitID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly Stop { get; set; }
        public string Time { get { return $"{Start:HH:mm} - {Stop:HH:mm}"; } }
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
        public ImageSource Picture
        {
            get
            {
                if (BadgeData == null) return null;

                var imageHandling = new ImageHandling();
                return imageHandling.LoadSvg(BadgeData);


            }
        }
        public string BadgeName { get; set; } //behøves ikke
        public string Activity { get; set; }
        public string Preparation { get; set; }
        public string Notes { get; set; }
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
