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
        IRepository<ScoutsMeeting> MeetingRepo;
        IRepository<UnitRepository> UnitRepo;
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
            
        }
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMovie(), CanExecute => SelectedMeeting != null); // tildeles til Delete knappen
        public RelayCommand EditCommand => new RelayCommand(execute => EditMovie(SelectedMovie), canExecute => SelectedMeeting != null); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
