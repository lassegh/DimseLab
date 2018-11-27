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
        // User
        ObservableCollection<User> _userList = new ObservableCollection<User>();

        private string _userInputFirstName;
        private string _userInputLastName;
        private int _userInputNumber;
        private string _userInputEmail;

        private RelayCommand _relayAddUser;


        // Project
        ObservableCollection<Project> _projectList = new ObservableCollection<Project>();

        private string _projectInputName;
        private string _projectInputDescribtion;

        private DateTime _projectBeginDate;
        private DateTime _projectEndDate;

        private bool _projectIsFinished;

        private RelayCommand _relayAddProject;



        public BrowseViewModel()
        {
            RelayAddUser = new RelayCommand(CallAddNewUser);

            // Placeholder Users
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));

            RelayAddProject = new RelayCommand(AddNewProject);

            // Moved all dummy data for projects to "helper class" since this is were it will come from in the future.
            var helper = new Helper();
            var projects = helper.ReadProjectData();

            foreach (var project in projects)
            {
                ProjectList.Add(project);
            }


        }

        // A fix for the async method "AddNewUser"
        #region FixAsyncAddNewUser

        public void CallAddNewUser()
        {
            AddNewUser();
        }

        #endregion


        //Method that adds a new user or otherwise tells the user if they used a wrong email
        public async Task AddNewUser()
        {
            if (UserInputEmail.Contains("@easj.dk".ToLower()) && string.IsNullOrWhiteSpace(UserInputEmail) || string.IsNullOrWhiteSpace(UserInputFirstName) || string.IsNullOrWhiteSpace(UserInputLastName))
            {
                // Wrong email used
                var dialog = new MessageDialog("Invalid Email Used");
                await dialog.ShowAsync();
            }
            else
            {   // Adds new user
                UserList.Add(new User(UserInputFirstName, UserInputLastName, UserInputNumber, UserInputEmail));
            }
        }

        // Adds a new project to "ProjectList"
        public void AddNewProject()
        {
            ProjectList.Add(new Project(ProjectInputName, ProjectInputDescribtion, ProjectEndDate));
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

        public RelayCommand RelayAddUser
        {
            get { return _relayAddUser; }
            set { _relayAddUser = value; }
        }


        public ObservableCollection<Project> ProjectList
        {
            get { return _projectList; }
            set
            {
                _projectList = value;
                OnPropertyChanged();
            }
        }

        public string ProjectInputName
        {
            get { return _projectInputName; }
            set { _projectInputName = value; }
        }

        public string ProjectInputDescribtion
        {
            get { return _projectInputDescribtion; }
            set { _projectInputDescribtion = value; }
        }

        public DateTime ProjectBeginDate
        {
            get { return _projectBeginDate; }
            set { _projectBeginDate = value; }
        }

        public DateTime ProjectEndDate
        {
            get { return _projectEndDate; }
            set { _projectEndDate = value; }
        }

        public bool ProjectIsFinished
        {
            get { return _projectIsFinished; }
            set { _projectIsFinished = value; }
        }

        public RelayCommand RelayAddProject
        {
            get { return _relayAddProject; }
            set { _relayAddProject = value; }
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
