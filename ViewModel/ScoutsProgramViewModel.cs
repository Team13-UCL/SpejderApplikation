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
    internal class ScoutsProgramViewModel : ViewModelBase
    {
        IRepository<ScoutsMeeting> ScoutMeetingRepo;
        IRepository<Meeting> MeetingRepo;
        IRepository<Badge> BadgeRepo;
        IRepository<Activity> ActivityRepo;
        IRepository<Unit> UnitRepo;
        private readonly ImageHandling _imageHandling;


        // Skifter enhed i ScoutsProgramView


        // Command til at vælge en enhed (RelayCommand)
        public RelayCommand SelectUnitCommand => new RelayCommand(
            execute =>
            {
                if (execute is Unit selectedUnit)
                {
                    SelectedUnit = selectedUnit;
                }
            },
            canExecute => true);


        //programmet virker ikke med disse 
        //public ObservableCollection<Unit> Units { get; set; } = new ObservableCollection<Unit>();
        //public ObservableCollection<ScoutsMeeting> ScoutMeetings { get; set; } = new ObservableCollection<ScoutsMeeting>();




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

        public DateTime DateTime
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
                UnitName = _selectedUnit.UnitName;
                UnitLink = _selectedUnit.Link;
                UnitDescription = _selectedUnit.Description;
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
                    Date = default;
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
                BadgeName = _selectedBadge.Name;
                BadgeDescription = _selectedBadge.Description;
                BadgeLink = _selectedBadge.Link;
                BadgeData = _selectedBadge.Picture;


            }
        }


        private ScoutsMeeting _selectedScoutMeeting;
        public ScoutsMeeting SelectedScoutMeeting
        {
            get { return _selectedScoutMeeting; }
            set
            {
                _selectedScoutMeeting = value;
                OnPropertyChanged();
                SelectedMeeting = MeetingRepo.GetByID(SelectedScoutMeeting.meetingID);
                SelectedActivity = ActivityRepo.GetByID(SelectedScoutMeeting.activityID);
                SelectedBadge = BadgeRepo.GetByID(SelectedScoutMeeting.badgeID);
                SelectedUnit = UnitRepo.GetByID(_selectedScoutMeeting.unitID);
                Date = _selectedScoutMeeting.Date;

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
            Badges = new ObservableCollection<Badge>(BadgeRepo.GetAll());
            ShowOldActivities(); // Initialize the ScoutMeetings collection
        }// ScoutMeetings og Meetings bliver initialiseret gennem ObserableCollections og flydt med data hentet fra vores respositories
        public void NewMeeting()
        {
            SelectedScoutMeeting = new ScoutsMeeting();
            ScoutMeetings.Add(SelectedScoutMeeting);
        }
        public void EditMeeting(ScoutsMeeting sm)
        {
            int ActivityID = ActivityRepo.AddOrEditType(SelectedActivity, 0);
            int MeetingID = MeetingRepo.AddOrEditType(SelectedMeeting, ActivityID);
            int BadgeID = BadgeRepo.AddOrEditType(SelectedBadge, ActivityID);
            int UnitID = UnitRepo.AddOrEditType(SelectedUnit, ActivityID);
            SelectedScoutMeeting.UpdateID(ActivityID, BadgeID, UnitID, MeetingID);
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
                // OnPropertyChanged(); ikke nødvendig
                ShowOldActivities();
            }
        }


        private void ShowOldActivities()
        {
            try
            {
                var specificDate = new DateOnly(2024, 5, 10); // For testing

                var allMeetings = ScoutMeetingRepo.GetAll();

                if (ShowOld == true)
                {
                    ScoutMeetings = new ObservableCollection<ScoutsMeeting>(allMeetings);
                }
                else
                {
                    var today = DateOnly.FromDateTime(DateTime.Today);
                    ScoutMeetings = new ObservableCollection<ScoutsMeeting>(allMeetings.Where(meeting => meeting.Date >= specificDate));
                }

                SelectedScoutMeeting = ScoutMeetings.First(); // henter det første møde i listen ellers er det null og crasher                

                OnPropertyChanged(nameof(ScoutMeetings));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating meetings: {ex.Message}");
            }
        }

        public RelayCommand DownloadCommand => new RelayCommand(async execute => await DownloadImage(), canExecute => BadgeLink != null);
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMeeting(), CanExecute => SelectedMeeting != null); // tildeles til Delete knappen
        public RelayCommand EditCommand => new RelayCommand(execute => EditMeeting(SelectedScoutMeeting), canExecute => SelectedMeeting != null); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
