using System;
using System.Collections.Generic;
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

        public LoginViewModel()
        {
            CreateLoginVisibility = false;
            LoginVisibility = true;

            LoginCommand = new RelayCommand(LoginMethod);
            CreateLoginCommand = new RelayCommand(CreateLoginMethod);
            ShiftVisibilityCommand = new RelayCommand(ShiftVisibility);
        }

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

        private bool PasswordsAreEqual()
        {
            if (PassWord == PassWordCheck)
            {
                return true;
            }
            return false;
        }

        private bool TryParsePhoneNumber(string phoneNumber)
        {
            return int.TryParse(phoneNumber, out _phoneNumberAsInt);
        }

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
