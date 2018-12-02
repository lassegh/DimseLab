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



        public BrowseViewModel()
        {
            RelayAddUser = new RelayCommand(CallAddNewUser);

            // TODO oprettelse af denne type object skal ikke ske her
            // Placeholder Users
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));



            // Opretter instans af ModelController
            ModelController mc = new ModelController();

            // Load data
            mc.LoadEverything();

            // Henter Objecter fra liste til observableCollection
            foreach (Project project in mc.ProjectList)
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
                // TODO er det ikke kun i dette tilfælde, der skal bruges async?
                // Wrong email used
                var dialog = new MessageDialog("Invalid Email Used");
                await dialog.ShowAsync();
            }
            else
            {   // Adds new user
                // TODO oprettelse af denne type object skal ikke ske her
                UserList.Add(new User(UserInputFirstName, UserInputLastName, UserInputNumber, UserInputEmail));
            }
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
