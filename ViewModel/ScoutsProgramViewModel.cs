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

        public ObservableCollection<ScoutsMeeting> ScoutMeetings { get; set; }
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
        private ScoutsMeeting _selectedMeeting;
        public ScoutsMeeting SelectedMeeting 
        {
            get {  return _selectedMeeting; }
            set 
            { 
                _selectedMeeting = value;
                OnPropertyChanged();
                UpdatePanel();
            }
        }
        private void UpdatePanel()
        {
            Date = SelectedMeeting.Date;
            Start = SelectedMeeting.Start;
            Stop = SelectedMeeting.Stop;
        }

        public ScoutsProgramViewModel(IRepository<ScoutsMeeting> repository)
        {
            this.ScoutMeetingRepo = repository ?? throw new ArgumentNullException(nameof(repository));
            ScoutMeetings = new ObservableCollection<ScoutsMeeting>(ScoutMeetingRepo.GetAll());
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
