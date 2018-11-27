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
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        ObservableCollection<User> _userList = new ObservableCollection<User>();

        private string _userInputFirstName;
        private string _userInputLastName;
        private int _userInputNumber;
        private string _userInputEmail;

        private RelayCommand _relayAddUser;


        public UserViewModel()
        {
            RelayAddUser = new RelayCommand(callAddNewUser);

            // Placeholder Users
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));

        }

        // A fix for the async method "AddNewUser"
        #region FixAsyncAddNewUser

        public void callAddNewUser()
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


        #region Get & Set Propertys

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
