using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class MenuViewModel : INotifyPropertyChanged
    {
        private bool _loggedIn;
        private bool _browseVisibility = false;
        private bool _myProjectsVisibility = false;
        private bool _myProfileVisibility = false;

        public RelayCommand BrowseButton { get; set; }
        public RelayCommand ManageProjectsButton { get; set; }
        public RelayCommand UserProfileButton { get; set; }
        public RelayCommand AdminButton { get; set; }

        public bool BrowseVisibility
        {
            get { return _browseVisibility; }
            set
            {
                _browseVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool MyProjectsVisibility
        {
            get { return _myProjectsVisibility; }
            set
            {
                _myProjectsVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool MyProfileVisibility
        {
            get { return _myProfileVisibility; }
            set
            {
                _myProfileVisibility = value;
                OnPropertyChanged();
            }
        }


        public MenuViewModel()
        {
            //TODO get loginToken
            if (_loggedIn)
            {
                //Show me browse
            }
            else
            {
                //Show me login
            }
            
            BrowseButton = new RelayCommand(OpenBrowse);
            ManageProjectsButton = new RelayCommand(OpenMyProjects);
            UserProfileButton = new RelayCommand(OpenUserProfile);
            AdminButton = new RelayCommand(OpenAdmin);
        }

        private void OpenBrowse()
        {
            BrowseVisibility = true;
            MyProjectsVisibility = false;
            MyProfileVisibility = false;
        }

        private void OpenMyProjects()
        {
            BrowseVisibility = false;
            MyProjectsVisibility = true;
            MyProfileVisibility = false;
        }

        private void OpenUserProfile()
        {
            BrowseVisibility = false;
            MyProjectsVisibility = false;
            MyProfileVisibility = true;
        }

        private void OpenAdmin()
        {
            BrowseVisibility = false;
            MyProjectsVisibility = false;
            MyProfileVisibility = false;
            // Admin to be visible
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
