using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class BrowseViewModel : INotifyPropertyChanged
    {
        private bool _browseVisibility;
        private bool _myProjectsVisibility;
        private bool _myProfileVisibility;
        private bool _adminVisibility;
        private bool _editProjectVisibility;

        // User
        ObservableCollection<User> _userList = new ObservableCollection<User>();

        private ObservableCollection<Project> _projectList = ModelController.Instance.ProjectList;

        private string _userInputFirstName;
        private string _userInputLastName;
        private int _userInputNumber;
        private string _userInputEmail;

        public BrowseViewModel()
        { 
            // TODO oprettelse af denne type object skal ikke ske her
            // Placeholder Users
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));

            BrowseButton = new RelayCommand(BrowseButtonMethod);
        }

        void BrowseButtonMethod()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.BrowseVisibility = true;
            BrowseVisibility = ModelController.Instance.BrowseVisibility;
            MyProjectsVisibility = ModelController.Instance.MyProjectsVisibility;
            MyProfileVisibility = ModelController.Instance.MyProfileVisibility;
            AdminVisibility = ModelController.Instance.AdminVisibility;
            EditProjectVisibility = ModelController.Instance.EditProjectVisibility;
        }


        #region Get & Set Properties

        public ObservableCollection<User> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }

        public string UserInputFirstName
        {
            get { return _userInputFirstName; }
            set { _userInputFirstName = value; }
        }

        public string UserInputLastName
        {
            get { return _userInputLastName; }
            set { _userInputLastName = value; }
        }

        public int UserInputNumber
        {
            get { return _userInputNumber; }
            set { _userInputNumber = value; }
        }

        public string UserInputEmail
        {
            get { return _userInputEmail; }
            set { _userInputEmail = value; }
        }

        public ObservableCollection<Project> ProjectList
        {
            get { return _projectList; }
            set { _projectList = value; }
        }

        public RelayCommand BrowseButton { get; set; }

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

        public bool AdminVisibility
        {
            get
            {
                return _adminVisibility;
                OnPropertyChanged();
            }
            set
            {
                _adminVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool EditProjectVisibility
        {
            get { return _editProjectVisibility; }
            set
            {
                _editProjectVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region NotifyPropChange

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
