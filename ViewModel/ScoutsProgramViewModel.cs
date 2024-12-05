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
    public class ScoutsProgramViewModel : ViewModelBase
    {
        IRepository<ScoutsMeeting> ScoutMeetingRepo;
        IRepository<Meeting> MeetingRepo;
        IRepository<Badge> BadgeRepo;
        IRepository<Activity> ActivityRepo;
        IRepository<Unit> UnitRepo;
        private readonly ImageHandling _imageHandling;
        
        public ObservableCollection<ScoutsMeeting> ScoutMeetings { get; set; }
        public ObservableCollection<Badge> Badges { get; set; }
        public ObservableCollection<Unit> Units { get; set; }
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
                    }
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DateTime)); // Notify that DateTime has changed
                }
            }
        }

        public DateTime DateTime // skal bruges fordi datepicker ikke kan binde til DateOnly
        {
            get { return new DateTime(_date.Year, _date.Month, _date.Day); }
            set
            {
                Date = DateOnly.FromDateTime(value);
            }
        }
        private TimeOnly _start;
        public TimeOnly Start
        {
            get { return _start; }
            set
            {
                _start = value;
                SelectedScoutMeeting.Start = value;
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
                OnPropertyChanged();
            }
        }
        private string _badgeName;

        public string BadgeName
        {
            get { return _badgeName; }
            set
            {
                _badgeName = value;
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
                OnPropertyChanged();
            }
        }


        private string _activity;
        public string Activity
        {
            get { return _activity; }
            set
            {
                _activity = value;
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
                OnPropertyChanged();
            }
        }
        private string _unitName;

        public string UnitName
        {
            get { return _unitName; }
            set
            {
                _unitName = value;
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
                    UnitName = string.Empty; // handler null case
                    UnitDescription = string.Empty;
                    UnitLink = string.Empty;
                }
                
                
            }
        }

        private Meeting _selectedMeeting;

        public Meeting SelectedMeeting
        {
            get { return _selectedMeeting; }
            set
            {
                _selectedMeeting = value;
                if (_selectedMeeting != null)
                {
                    Date = _selectedMeeting.Date;
                    Start = _selectedMeeting.Start;
                    Stop = _selectedMeeting.Stop;
                }
                else
                {
                    Date = default; // handler nullcase
                    Start = default;
                    Stop = default;
                }
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
                ActivityTeaser = _selectedActivity.BriefDescription;
                Activity = _selectedActivity.ActivityDescription;
                Preparation = _selectedActivity.Preparation;
                Notes = _selectedActivity.Notes;
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
                Add(SelectedActivity, SelectedMeeting, SelectedUnit, SelectedBadge);
                _selectedScoutMeeting = value;
                OnPropertyChanged();

                SelectedMeeting = MeetingRepo.GetByID(SelectedScoutMeeting.meetingID);
                SelectedActivity = ActivityRepo.GetByID(SelectedScoutMeeting.activityID);
                //SelectedBadge = BadgeRepo.GetByID(SelectedScoutMeeting.badgeID); //gammel kode
                //SelectedUnit = UnitRepo.GetByID(SelectedScoutMeeting.unitID); // gammel kode
                SelectedBadge = Badges.FirstOrDefault(b => b._badgeID == _selectedScoutMeeting.badgeID); // ellers crasher det med instance fejl
                SelectedUnit = Units.FirstOrDefault(u => u._unitID == _selectedScoutMeeting.unitID); // ellers crasher det med instance fejl
                Date = _selectedScoutMeeting.Date; // sætter datoen til det valgte møde

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
        public void EditMeeting(ScoutsMeeting sm)
        {
            ActivityRepo.EditType(SelectedActivity);
            BadgeRepo.EditType(SelectedBadge);
            MeetingRepo.EditType(SelectedMeeting);
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

                //SelectedScoutMeeting = ScoutMeetings.First(); // henter det første møde i listen ellers er det null og crasher, lol det virker nu åbenbart uden den              

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
        public RelayCommand EditCommand => new RelayCommand(execute => EditMeeting(SelectedScoutMeeting), canExecute => SelectedMeeting != null); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
