using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Pages.MainPageCategories.Core;

namespace SnakeGame.Pages.MainPageCategories.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand SelectGameViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand RecordsViewCommand { get; set; }

        public SelectGameViewModel GameVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public RecordsViewModel RecordsVM{ get; set; }

        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set 
            { 
                currentView = value;
                OnPropertyChanged();
            }

        }

        public MainViewModel()
        {
            GameVM = new SelectGameViewModel();
            SettingsVM = new SettingsViewModel();
            RecordsVM = new RecordsViewModel();

            CurrentView = GameVM;

            SelectGameViewCommand = new RelayCommand(o =>
            {
                CurrentView = GameVM;
            });
            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });
            RecordsViewCommand = new RelayCommand(o =>
            {
                CurrentView = RecordsVM;
            });
        }
    }
}
