using SpejderApplikation.DataHandler;
using SpejderApplikation.Model;
using SpejderApplikation.MVVM;
using SpejderApplikation.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Media;


namespace SpejderApplikation.ViewModel
{
    // ViewModel responsible for managing and interacting with Scout Meetings
    public class ScoutsProgramViewModel : ViewModelBase
    {
        // Repositories for managing data access
        IRepository<ScoutsMeeting> ScoutMeetingRepo;
        IRepository<Meeting> MeetingRepo;
        IRepository<Badge> BadgeRepo;
        IRepository<Activity> ActivityRepo;
        IRepository<Unit> UnitRepo;
        private readonly ImageHandling _imageHandling;
        
        
        public ObservableCollection<ScoutsMeeting> ScoutMeetings { get; set; }
        public ObservableCollection<Badge> Badges { get; set; }
        public ObservableCollection<Unit> Units { get; set; }

        // Date management properties
        private DateOnly _date;
        public DateOnly Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    if (SelectedScoutMeeting != null)
                    {
                        SelectedScoutMeeting.Date = value;
                        if (SelectedMeeting != null)
                        {
                            SelectedMeeting.Date = value;
                        }
                    }
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DateTime)); // Notify that DateTime has changed
                }
            }
        }

        // DateTime property for UI binding compatibility with DatePicker
        public DateTime DateTime
        {
            get { return new DateTime(_date.Year, _date.Month, _date.Day); }
            set
            {
                Date = DateOnly.FromDateTime(value);

            }
        }

        // Time management properties
        private TimeOnly _start;
        public TimeOnly Start
        {
            get { return _start; }
            set
            {
                _start = value;
                SelectedScoutMeeting.Start = value;
                
                if (SelectedMeeting != null)
                {
                    SelectedMeeting.Start = value;
                }
                OnPropertyChanged();
            }
        }
        private TimeOnly _stop;
        public TimeOnly Stop
        {
            get { return _stop; }
            set
            {
                _stop = value;
                SelectedScoutMeeting.Stop = value;
                
                if (SelectedMeeting != null)
                {
                    SelectedMeeting.Stop = value;
                }
                OnPropertyChanged();
            }
        }

        // Badge-related properties for managing badge details
        private string _badgeName;
        public string BadgeName
        {
            get { return _badgeName; }
            set
            {
                _badgeName = value;
                
                if (SelectedBadge != null)
                {
                    SelectedBadge.Name = value;
                }
                OnPropertyChanged();
            }
        }
        private string _badgeDescription;

        public string BadgeDescription
        {
            get { return _badgeDescription; }
            set
            {
                _badgeDescription = value;
                
                if (SelectedBadge != null)
                {
                    SelectedBadge.Description = value;
                }
                OnPropertyChanged();
            }
        }
        private string _badgeLink;

        public string BadgeLink
        {
            get { return _badgeLink; }
            set
            {
                _badgeLink = value;
                
                if (SelectedBadge != null)
                {
                    SelectedBadge.Link = value;
                }
                OnPropertyChanged();
            }
        }
        private byte[] _badgeData;
        public byte[] BadgeData
        {
            get { return _badgeData; }
            set
            {
                _badgeData = value;
               
                if (SelectedBadge != null)
                {
                    SelectedBadge.Picture = value;
                }
                OnPropertyChanged();

            }
        }

        private ImageSource _picture;
        public ImageSource Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                //SelectedBadge.Picture = value;
                OnPropertyChanged();
            }
        }
        private string _activityTeaser;

        public string ActivityTeaser
        {
            get { return _activityTeaser; }
            set 
            { 
                _activityTeaser = value;
                SelectedScoutMeeting.Activity = value;
                
                if (SelectedActivity != null)
                {
                    SelectedActivity.BriefDescription = value;
                }

                OnPropertyChanged();
            }
        }

        // Activity-related properties
        private string _activity;
        public string Activity
        {
            get { return _activity; }
            set
            {
                _activity = value;
                
                if (SelectedActivity != null)
                {
                    SelectedActivity.ActivityDescription = value;
                }
                OnPropertyChanged();
            }
        }
        private string _preparation;

        public string Preparation
        {
            get { return _preparation; }
            set
            {
                _preparation = value;
                
                if (SelectedActivity != null)
                {
                    SelectedActivity.Preparation = value;
                }
                OnPropertyChanged();
            }
        }
        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                SelectedScoutMeeting.Notes = value;
                
                if (SelectedActivity != null)
                {
                    SelectedActivity.Notes = value;
                }
                OnPropertyChanged();
            }
        }

        // Unit-related properties
        private string _unitName;
        public string UnitName
        {
            get { return _unitName; }
            set
            {
                _unitName = value;
                
                if (SelectedUnit != null)
                {
                    SelectedUnit.UnitName = value;
                }
                OnPropertyChanged();
            }
        }
        private string _unitDescription;

        public string UnitDescription
        {
            get { return _unitDescription; }
            set
            {
                _unitDescription = value;
                
                if (SelectedUnit != null)
                {
                    SelectedUnit.Description = value;
                }
                OnPropertyChanged();
            }
        }

        private string _unitLink;

        public string UnitLink
        {
            get { return _unitLink; }
            set
            {
                _unitLink = value;
                
                if (SelectedUnit != null)
                {
                    SelectedUnit.Link = value;
                }
                OnPropertyChanged();
            }
        }

        private Unit _selectedUnit;

        public Unit SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                OnPropertyChanged();
                if (_selectedUnit != null)
                {
                    UnitName = _selectedUnit.UnitName;
                    UnitLink = _selectedUnit.Link;
                    UnitDescription = _selectedUnit.Description;
                }
                else
                {
                    UnitName = string.Empty; // handle null cases
                    UnitDescription = string.Empty;
                    UnitLink = string.Empty;
                }
                
                
            }
        }

        // Meeting-related properties
        private Meeting _selectedMeeting;
        public Meeting SelectedMeeting
        {
            get { return _selectedMeeting; }
            set
            {
                _selectedMeeting = value;
                if (_selectedMeeting != null)
                {
                    // Assign the date and times from the selected meeting
                    Date = _selectedMeeting.Date;
                    Start = _selectedMeeting.Start;
                    Stop = _selectedMeeting.Stop;
                }
                else
                {
                    // Reset properties to default values if no meeting is selected
                    Date = default; // handler nullcase
                    Start = default;
                    Stop = default;
                }
                // Notify the UI about the property change
                OnPropertyChanged();
            }
        }


        private Activity _selectedActivity;
        public Activity SelectedActivity
        {
            get { return _selectedActivity; }
            set
            {
                _selectedActivity = value;
                OnPropertyChanged();
                if (_selectedActivity != null)
                {
                    ActivityTeaser = _selectedActivity.BriefDescription;
                    Activity = _selectedActivity.ActivityDescription;
                    Preparation = _selectedActivity.Preparation;
                    Notes = _selectedActivity.Notes;
                }
            }
        }
        private Badge _selectedBadge;

        public Badge SelectedBadge
        {
            get { return _selectedBadge; }
            set
            {
                _selectedBadge = value;
                OnPropertyChanged();


                if (_selectedBadge != null)
                {
                    // Update related properties with the selected activity's data
                    BadgeName = _selectedBadge.Name;
                    BadgeDescription = _selectedBadge.Description;
                    BadgeLink = _selectedBadge.Link;
                    BadgeData = _selectedBadge.Picture;
                }
                else
                {
                    BadgeName = string.Empty; // handler null case
                    BadgeDescription = string.Empty;
                    BadgeLink = string.Empty;
                    BadgeData = new byte[0];
                }
            }
        }
        

        private ScoutsMeeting _selectedScoutMeeting;
        public ScoutsMeeting SelectedScoutMeeting
        {
            get { return _selectedScoutMeeting; }
            set
            {
                if (_selectedScoutMeeting != null)
                {
                    if (SelectedActivity != null && SelectedMeeting != null && SelectedUnit != null && SelectedBadge != null)
                    {
                        Add(SelectedActivity, SelectedMeeting, SelectedUnit, SelectedBadge);
                    }
                }

                _selectedScoutMeeting = value;
                OnPropertyChanged();

                if (_selectedScoutMeeting != null)
                {
                    SelectedMeeting = MeetingRepo.GetByID(_selectedScoutMeeting.meetingID);
                    SelectedActivity = ActivityRepo.GetByID(_selectedScoutMeeting.activityID);
                    SelectedBadge = BadgeRepo.GetByID(_selectedScoutMeeting.badgeID);
                    SelectedUnit = UnitRepo.GetByID(_selectedScoutMeeting.unitID);
                    Date = _selectedScoutMeeting.Date;
                }
            }
        }

        public ScoutsProgramViewModel(IRepository<ScoutsMeeting> repository,
                                        IRepository<Meeting> meetingRepo,
                                        IRepository<Badge> BadgeRepo,
                                        IRepository<Activity> ActivityRepo,
                                        IRepository<Unit> UnitRepo)
        {
            this.ScoutMeetingRepo = repository ?? throw new ArgumentNullException(nameof(repository));
            ScoutMeetings = new ObservableCollection<ScoutsMeeting>(ScoutMeetingRepo.GetAll());
            this.MeetingRepo = meetingRepo ?? throw new ArgumentNullException(nameof(meetingRepo));
            this.BadgeRepo = BadgeRepo ?? throw new ArgumentNullException(nameof(BadgeRepo));
            this.ActivityRepo = ActivityRepo ?? throw new ArgumentNullException(nameof(ActivityRepo));
            this.UnitRepo = UnitRepo ?? throw new ArgumentNullException(nameof(UnitRepo));
            _imageHandling = new ImageHandling();
            Badges = new ObservableCollection<Badge>(BadgeRepo.GetAll()); // Henter alle mærker fra databasen
            Units = new ObservableCollection<Unit>(UnitRepo.GetAll()); // Henter alle enheder fra databasen
            
            ShowOldActivities(); // Initialize the ScoutMeetings collection, kan måske ændres i metoden da vi allerede har scoutmeetings
        }// ScoutMeetings og Meetings bliver initialiseret gennem ObserableCollections og flydt med data hentet fra vores respositories
        public void NewMeeting()
        {
           
            SelectedScoutMeeting = new ScoutsMeeting();                       

            ScoutMeetings.Add(SelectedScoutMeeting);

            Date = DateOnly.FromDateTime(DateTime.Today); // Sætter datoen til i dag
        }
        public void EditMeeting(ScoutsMeeting scoutmeeting)
        {


            // Update Activity
            if (SelectedActivity._activityID == null || SelectedActivity._activityID == 0)
            {
                int ID = ActivityRepo.AddType(SelectedActivity, 0);
                scoutmeeting.activityID = ID;
               // SelectedActivity.UpdateID(ID);
                SelectedScoutMeeting.activityID = ID; 
            }
            else
            {
                ActivityRepo.EditType(SelectedActivity);
            }

            // Update Badge
            if (scoutmeeting.badgeID == null || scoutmeeting.badgeID == 0)
            {
                int ID = BadgeRepo.AddType(SelectedBadge, scoutmeeting.activityID);
                SelectedBadge.UpdateID(ID);
                
            }
            else if (SelectedBadge._badgeID != scoutmeeting.badgeID)
            {
                BadgeRepo.ConnectTypes(SelectedBadge, scoutmeeting);
            }
            else
            {
                BadgeRepo.EditType(SelectedBadge);
            }

            // Update Meeting
            if (scoutmeeting.meetingID == null || scoutmeeting.meetingID == 0)
            {
                int ID = MeetingRepo.AddType(SelectedMeeting, scoutmeeting.activityID);
                SelectedMeeting.UpdateID(ID);
                
            }
            else if (SelectedMeeting._meetingID != scoutmeeting.meetingID)
            {
                MeetingRepo.ConnectTypes(SelectedMeeting, scoutmeeting);
            }
            else MeetingRepo.EditType(SelectedMeeting);

            // Update Unit
            if (scoutmeeting.unitID != null || scoutmeeting.unitID != 0 || scoutmeeting.unitID != SelectedUnit._unitID)
            {
                int ID = UnitRepo.AddType(SelectedUnit, scoutmeeting.activityID);
                SelectedUnit.UpdateID(ID);
                UnitRepo.ConnectTypes(SelectedUnit, scoutmeeting);
            }
        }



        public void DeleteMeeting()
        {
            if (SelectedScoutMeeting != null)
            {

                MessageBoxResult result = MessageBox.Show(
                    "Er du sikker på, at du vil slette mødet?",
                    "Bekræft sletning",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Fjern fra databasen
                    ScoutMeetingRepo.DeleteType(SelectedScoutMeeting);

                    // Fjern fra ObservableCollection
                    ScoutMeetings.Remove(SelectedScoutMeeting);

                    // Nulstil SelectedScoutMeeting
                    SelectedScoutMeeting = null;

                    // Eventuel opdatering af UI
                    MessageBox.Show("Mødet blev slettet.");

                }
            }
            else
            {
                MessageBox.Show("Ingen møde valgt til sletning.");
            }
        }


        private string link;
        public string Link
        {
            get { return link; }
            set
            {
                link = value;
                OnPropertyChanged();
            }
        }


        private async Task DownloadImage()
        {
            if (string.IsNullOrWhiteSpace(BadgeLink))
            {
                MessageBox.Show("manglende hjemmeside");
                return;
            }

            try
            {
                // Download the SVG image
                byte[] imageBytes = await _imageHandling.DownloadAndSaveImage(BadgeLink);

                if (imageBytes == null || imageBytes.Length == 0)
                {
                    MessageBox.Show("kunne ikke finde billedet");
                    return;
                }

                //den displayer med badgedata men skal måske os gemme i picture i badge????
                SelectedBadge.Picture = imageBytes; // Save the image bytes to the Picture property in the Badge object
                SelectedScoutMeeting.BadgeData = imageBytes;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private bool _showOld;
        public bool ShowOld
        {
            get { return _showOld; }
            set
            {
                _showOld = value;                
                ShowOldActivities();
            }
        }


        private void ShowOldActivities()
        {
            try
            {
                var specificDate = new DateOnly(2024, 5, 10); // For testing
                var today = DateOnly.FromDateTime(DateTime.Today); // For production

                var allMeetings = ScoutMeetingRepo.GetAll();

                if (ShowOld == true)
                {
                    ScoutMeetings = new ObservableCollection<ScoutsMeeting>(allMeetings);
                }
                else
                {                    
                    ScoutMeetings = new ObservableCollection<ScoutsMeeting>(allMeetings.Where(meeting => meeting.Date >= today)); //skal skifte til today hvis vi går live
                }

                SelectedScoutMeeting = ScoutMeetings.First(); // henter det første møde i listen ellers er det null og crasher         

                OnPropertyChanged(nameof(ScoutMeetings));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating meetings: {ex.Message}");
            }
        }

        private void FilterMeetingsByUnit(object unitName)
        {
           

            if (unitName is string unit) // unitName er en object, så vi skal konvertere til string
            {
                var filteredMeetings = ScoutMeetingRepo.GetAll().Where(meeting => meeting.Unit == unit); // Filter meetings by unit
                ScoutMeetings = new ObservableCollection<ScoutsMeeting>(filteredMeetings); // Update the ObservableCollection
                if (ScoutMeetings.Count > 0)    // Hvis der er møder i listen
                {
                    SelectedScoutMeeting = ScoutMeetings.First(); // Sætter det første møde i listen som SelectedScoutMeeting
                    OnPropertyChanged(nameof(ScoutMeetings)); 
                }
                else
                {
                    MessageBox.Show("Ingen møder fundet for denne enhed.");
                }
            }
        }

        public void Add(Activity activity, Meeting meeting, Unit unit, Badge badge)
        {
            int ActivityID, MeetingID, UnitID, BadgeID;
            if (activity._activityID == null || activity._activityID == 0)
                ActivityID = ActivityRepo.AddType(activity, 0);
            else { ActivityID = activity._activityID; }
            if(meeting._meetingID == null ||  meeting._meetingID == 0)
                MeetingRepo.AddType(meeting, ActivityID);
            if(unit._unitID == null || unit._unitID == 0)
                UnitRepo.AddType(unit, ActivityID);
            if(badge._badgeID == null || badge._badgeID == 0)
                BadgeRepo.AddType(badge, ActivityID);
        }
        public RelayCommand FilterMeetingsCommand => new RelayCommand(FilterMeetingsByUnit);
        public RelayCommand DownloadCommand => new RelayCommand(async execute => await DownloadImage(), canExecute => BadgeLink != null);
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMeeting(), CanExecute => SelectedMeeting != null); // tildeles til Delete knappen
        public RelayCommand EditCommand => new RelayCommand(execute => EditMeeting(SelectedScoutMeeting)); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
