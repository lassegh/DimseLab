using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private int _phoneNumberAsInt;
        private string _passWord;
        private string _passWordCheck;

        private bool _createLoginVisibility;
        private bool _loginVisibility;

        private RelayCommand _loginCommand;
        private RelayCommand _createLoginCommand;
        private RelayCommand _shiftVisibilityCommand;

        private ObservableCollection<User> _uList = ModelController.Instance.UserList;

        /// <summary>
        /// Constructor
        /// </summary>
        public LoginViewModel()
        {
            CreateLoginVisibility = false;
            LoginVisibility = true;

            LoginCommand = new RelayCommand(LoginMethod);
            CreateLoginCommand = new RelayCommand(CreateLoginMethod);
            ShiftVisibilityCommand = new RelayCommand(ShiftVisibility);
        }

        /// <summary>
        /// Tjekker om logininformationer er korrekte, og logger på eller viser besked
        /// </summary>
        private async void LoginMethod()
        {
            bool userFound = false;

            if (PassWord != null && Email != null)
            {
                foreach (User user in ModelController.Instance.UserList)
                {
                    if (user.Email.ToLower().Equals(Email.ToLower()) && user.Password.Equals(PassWord))
                    {
                        ModelController.Instance.CurrentUser = user;
                        userFound = true;
                        ModelController.Instance.SetAllInvisible();
                        ModelController.Instance.BrowseVisibility = true; // Viser browse ved login
                        ModelController.Instance.SaveEverything();
                        ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
                        break;
                    }
                }
            }

            if (!userFound)
            {
                var dialog = new MessageDialog("Invalid password. Please try again.");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Ved oprettelse af bruger - tjekker om passwords er ens
        /// </summary>
        /// <returns>bool</returns>
        private bool PasswordsAreEqual()
        {
            if (PassWord == PassWordCheck)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tjekker om tlf nummer er tal og om det er 8 cifre langt
        /// </summary>
        /// <param name="phoneNumber">Tlf som string</param>
        /// <returns>bool</returns>
        private bool TryParsePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 8)
            {
                return false;
            }
            return int.TryParse(phoneNumber, out _phoneNumberAsInt);
        }

        /// <summary>
        /// Metode for at oprette login
        /// </summary>
        private async void CreateLoginMethod()
        {
            if (PasswordsAreEqual() && TryParsePhoneNumber(PhoneNumber))
            {
                if (!Email.Contains("@easj.dk".ToLower()))
                {
                    // Wrong email used
                    var dialog = new MessageDialog("Invalid Email Used");
                    await dialog.ShowAsync();
                }
                else
                {   // Adds new user, sets currentUser as the new user, sends user back to program
                    User user = new User(FirstName, LastName, _phoneNumberAsInt, Email, PassWord);
                    ModelController.Instance.UserList.Add(user);
                    ModelController.Instance.CurrentUser = user;
                    ModelController.Instance.SaveEverything();
                    ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
                }
            }
            else if (!PasswordsAreEqual())
            {
                // ask user to type to similar passwords
                var dialog = new MessageDialog("Passwords does not match");
                await dialog.ShowAsync();
            }
            else
            {
                // Wrong phone number
                var dialog = new MessageDialog("Please type a phone number");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Skifter mellem login og oprettelse af login
        /// </summary>
        private void ShiftVisibility()
        {
            if (LoginVisibility)
            {
                LoginVisibility = false;
                CreateLoginVisibility = true;
            }
            else
            {
                LoginVisibility = true;
                CreateLoginVisibility = false;
            }
        }

        #region Properties

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
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

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        public RelayCommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }

        public string PassWordCheck
        {
            get { return _passWordCheck; }
            set { _passWordCheck = value; }
        }

        public bool CreateLoginVisibility
        {
            get { return _createLoginVisibility; }
            set
            {
                _createLoginVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool LoginVisibility
        {
            get { return _loginVisibility; }
            set
            {
                _loginVisibility = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ShiftVisibilityCommand
        {
            get { return _shiftVisibilityCommand; }
            set { _shiftVisibilityCommand = value; }
        }

        public RelayCommand CreateLoginCommand
        {
            get { return _createLoginCommand; }
            set { _createLoginCommand = value; }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
