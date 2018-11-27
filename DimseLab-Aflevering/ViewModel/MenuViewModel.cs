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

        public RelayCommand BrowseButton { get; set; }
        public RelayCommand ManageProjectsButton { get; set; }
        public RelayCommand UserProfileButton { get; set; }

        public bool BrowseVisibility
        {
            get { return _browseVisibility; }
            set
            {
                _browseVisibility = value;
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
        }

        private void OpenBrowse()
        {
            BrowseVisibility = true;
        }

        private void OpenMyProjects()
        {
            BrowseVisibility = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
