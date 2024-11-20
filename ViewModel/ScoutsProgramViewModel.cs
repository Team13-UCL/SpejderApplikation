using SpejderApplikation.Model;
using SpejderApplikation.MVVM;
using SpejderApplikation.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.ViewModel
{
    internal class ScoutsProgramViewModel : ViewModelBase
    {
        IRepository<ScoutsMeeting> ScoutMeetingRepo;
        IRepository<Meeting> MeetingRepo;
        IRepository<Badge> BadgeRepo;
        IRepository<Activity> ActivityRepo;
        IRepository<Unit> UnitRepo;

        public ObservableCollection<ScoutsMeeting> ScoutMeetings { get; set; }
        public ObservableCollection<Meeting> Meetings { get; set; }
        private DateOnly _date;
        public DateOnly Date 
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(); // Afgørende for at UI opdateres når værdier ændres
            } 
        }
        private TimeOnly _start;
        public TimeOnly Start 
        {
            get { return _start; }
            set 
            { 
                _start = value;
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
            set { _badgeDescription = value;
                OnPropertyChanged();
            }
        }
        private string _badgeLink;

        public string BadgeLink
        {
            get { return _badgeLink; }
            set {
                _badgeLink = value;
                OnPropertyChanged();
            }
        }
        private string _activity;
        public string Activity
        {
            get { return _activity; }
            set { _activity = value;
                OnPropertyChanged();
            }
        }
        private string _preparation;

        public string Preparation
        {
            get { return _preparation; }
            set { _preparation = value;
                OnPropertyChanged();
            }
        }
        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { _notes = value;
                OnPropertyChanged();
            }
        }
        private Meeting _selectedMeeting;

        public Meeting SelectedMeeting
        {
            get { return _selectedMeeting; }
            set 
            { 
                _selectedMeeting = value;
                Date = _selectedMeeting.Date;
                Start = _selectedMeeting.Start;
                Stop = _selectedMeeting.Stop;
            }
        }
        private Activity _selectedActivity;

        public Activity SelectedActivity
        {
            get { return _selectedActivity; }
            set 
            { 
                _selectedActivity = value;
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
                BadgeName = _selectedBadge.Name;
                BadgeDescription = _selectedBadge.Description;
                BadgeLink = _selectedBadge.Link;
            }
        }


        private ScoutsMeeting _selectedScoutMeeting;
        public ScoutsMeeting SelectedScoutMeeting 
        {
            get {  return _selectedScoutMeeting; }
            set 
            { 
                _selectedScoutMeeting = value;
                OnPropertyChanged();
                SelectedMeeting = MeetingRepo.GetByID(SelectedScoutMeeting.meetingID);
                SelectedActivity = ActivityRepo.GetByID(SelectedScoutMeeting.activityID);
                SelectedBadge = BadgeRepo.GetByID(SelectedScoutMeeting.badgeID);

            }
        }

        public ScoutsProgramViewModel(IRepository<ScoutsMeeting> repository, IRepository<Meeting> meetingRepo, IRepository<Badge> BadgeRepo, IRepository<Activity> ActivityRepo)
        {
            this.ScoutMeetingRepo = repository ?? throw new ArgumentNullException(nameof(repository));
            ScoutMeetings = new ObservableCollection<ScoutsMeeting>(ScoutMeetingRepo.GetAll());
            this.MeetingRepo = meetingRepo ?? throw new ArgumentNullException(nameof(meetingRepo));
            Meetings = new ObservableCollection<Meeting>(MeetingRepo.GetAll());
            this.BadgeRepo = BadgeRepo ?? throw new ArgumentNullException(nameof(BadgeRepo));
            this.ActivityRepo = ActivityRepo ?? throw new ArgumentNullException(nameof(ActivityRepo));
        }// ScoutMeetings og Meetings bliver initialiseret gennem ObserableCollections og flydt med data hentet fra vores respositories
        public void NewMeeting()
        {
            SelectedScoutMeeting = new ScoutsMeeting();
            ScoutMeetings.Add(SelectedScoutMeeting);
        }
        public void EditMeeting()
        {
            //Meeting meeting = SelectedMeeting.UpdateMeeting();
            //MeetingRepo.EditType(meeting);
            //SelectedMeeting.UpdateActivity();
        }
        public void DeleteMeeting()
        {

        }
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMeeting(), CanExecute => SelectedScoutMeeting != null); // tildeles til Delete knappen
        public RelayCommand EditCommand => new RelayCommand(execute => EditMeeting(), canExecute => SelectedScoutMeeting != null); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
