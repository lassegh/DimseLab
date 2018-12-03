using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        private bool _browseVisibility;
        private bool _myProjectsVisibility;
        private bool _myProfileVisibility;
        private bool _adminVisibility;
        private bool _editProjectVisibility;
        private String _firstName = ModelController.Instance.CurrentUser.FirstName;
        private String _lastName = ModelController.Instance.CurrentUser.LastName;
        private String _email = ModelController.Instance.CurrentUser.Email;

        public UserViewModel()
        {
            UserProfileButton = new RelayCommand(OpenUserProfile);
        }

        private void OpenUserProfile()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.MyProfileVisibility = true;
            BrowseVisibility = ModelController.Instance.BrowseVisibility;
            MyProjectsVisibility = ModelController.Instance.MyProjectsVisibility;
            MyProfileVisibility = ModelController.Instance.MyProfileVisibility;
            AdminVisibility = ModelController.Instance.AdminVisibility;
            EditProjectVisibility = ModelController.Instance.EditProjectVisibility;
        }

        public RelayCommand UserProfileButton { get; set; }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public bool BrowseVisibility
        {
            get { return _browseVisibility; }
            set { _browseVisibility = value; }
        }

        public bool MyProjectsVisibility
        {
            get { return _myProjectsVisibility; }
            set { _myProjectsVisibility = value; }
        }

        public bool MyProfileVisibility
        {
            get { return _myProfileVisibility; }
            set { _myProfileVisibility = value; }
        }

        public bool AdminVisibility
        {
            get { return _adminVisibility; }
            set { _adminVisibility = value; }
        }

        public bool EditProjectVisibility
        {
            get { return _editProjectVisibility; }
            set { _editProjectVisibility = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
