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
        IRepository<Unit> UnitRepo;
        IRepository<Meeting> MeetingRepo;
        // Repository for resten af repo klasserne.

        ObservableCollection<ScoutsMeeting> Meetings { get; set; }
        private ScoutsMeeting _selectedMeeting;
        public ScoutsMeeting SelectedMeeting 
        {
            get {  return _selectedMeeting; }
            set 
            { 
                SelectedMeeting = value;
                OnPropertyChanged();
            }
        }
        public void NewMeeting()
        {
            SelectedMeeting = new ScoutsMeeting();
            Meetings.Add(SelectedMeeting);
        }
        public void EditMeeting()
        {
            Meeting meeting = SelectedMeeting.UpdateMeeting();
            MeetingRepo.EditType(meeting);
            SelectedMeeting.UpdateActivity();
        }
        public void DeleteMeeting()
        {

        }
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMeeting(), CanExecute => SelectedMeeting != null); // tildeles til Delete knappen
        public RelayCommand EditCommand => new RelayCommand(execute => EditMeeting(), canExecute => SelectedMeeting != null); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
