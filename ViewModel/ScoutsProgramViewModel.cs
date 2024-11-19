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
                OnPropertyChanged();
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


        private ScoutsMeeting _selectedMeeting;
        public ScoutsMeeting SelectedMeeting 
        {
            get {  return _selectedMeeting; }
            set 
            { 
                _selectedMeeting = value;
                OnPropertyChanged();
                UpdatePanel(_selectedMeeting.meetingID, _selectedMeeting.badgeID, _selectedMeeting.unitID, _selectedMeeting.activityID);
            }
        }
        private void UpdatePanel(int meeting, int badge, int unit, int activity)
        {
            Date = SelectedMeeting.Date;
            Start = SelectedMeeting.Start;
            Stop = SelectedMeeting.Stop;
            Badge SelectedBadge = BadgeRepo.GetByID(badge);
            BadgeName = SelectedBadge.Name;
            BadgeDescription = SelectedBadge.Description;
            BadgeLink = SelectedBadge.Link;
        }

        public ScoutsProgramViewModel(IRepository<ScoutsMeeting> repository, IRepository<Meeting> meetingRepo, IRepository<Badge> BadgeRepo)
        {
            this.ScoutMeetingRepo = repository ?? throw new ArgumentNullException(nameof(repository));
            ScoutMeetings = new ObservableCollection<ScoutsMeeting>(ScoutMeetingRepo.GetAll());
            this.MeetingRepo = meetingRepo ?? throw new ArgumentNullException(nameof(meetingRepo));
            Meetings = new ObservableCollection<Meeting>(MeetingRepo.GetAll());
            this.BadgeRepo = BadgeRepo ?? throw new ArgumentNullException(nameof(BadgeRepo));

        }
        public void NewMeeting()
        {
            SelectedMeeting = new ScoutsMeeting();
            ScoutMeetings.Add(SelectedMeeting);
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
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMeeting(), CanExecute => SelectedMeeting != null); // tildeles til Delete knappen
        public RelayCommand EditCommand => new RelayCommand(execute => EditMeeting(), canExecute => SelectedMeeting != null); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
