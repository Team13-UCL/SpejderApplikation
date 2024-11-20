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

namespace SpejderApplikation.ViewModel
{
    internal class ScoutsProgramViewModel : ViewModelBase
    {
        IRepository<ScoutsMeeting> ScoutMeetingRepo;
        private readonly ImageHandling _imageHandling;

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


        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged();                
            }
        }


        private async Task DownloadImage()
        {
            if (string.IsNullOrWhiteSpace(Url)) 
            {
                MessageBox.Show("manglende hjemmeside");
                return;
            }

            try
            {
                // Download the SVG image
                string filePath = await _imageHandling.DownloadAndSaveImage(Url);

                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("kunne ikke finde billedet");
                    return;
                }

                // Display the SVG image
                //DownloadedImage.Source = _imageHandling.LoadSvg(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        public RelayCommand DownloadCommand => new RelayCommand(async execute => await DownloadImage(), canExecute => !string.IsNullOrWhiteSpace(Url));

        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMeeting(), CanExecute => SelectedMeeting != null); // tildeles til Delete knappen
        public RelayCommand EditCommand => new RelayCommand(execute => EditMeeting(), canExecute => SelectedMeeting != null); // Tildeles til Edit knappen
        public RelayCommand NewCommand => new RelayCommand(execute => NewMeeting()); // tildeles til "Nyt Møde" knappen.
    }
}
