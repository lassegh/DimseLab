using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        // Opretter reference til ModelController
        private ModelController _mC = ModelController.Instance;

        // Knap til at logge ud
        private RelayCommand _logOutCommand;

        // Knap til at gemme ændringer
        private RelayCommand _saveChangesCommand;

        // private fields for password change
        private string _currentPassword;
        private string _newPassword;
        private string _newPasswordCheck;

        // Telefonnumre
        private string _phoneNumberInput = ModelController.Instance.CurrentUser.Number.ToString();
        private int _phoneNumberAsInt;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserViewModel()
        {
            // Knap til visning af userGrid
            UserProfileButton = new RelayCommand(OpenUserProfile);

            // Log ud knap
            LogOutCommand = new RelayCommand(LogOutMethod);

            // Knap til at gemme ændringer
            SaveChangesCommand = new RelayCommand(SaveChangesMethod);
        }

        /// <summary>
        /// Gemmer ændringer der er lavet i min profil
        /// </summary>
        private async void SaveChangesMethod()
        {
            if (NewPassword != null)
            {
                if (PasswordChecker())
                {
                    MC.CurrentUser.Password = NewPassword;
                }
                else
                {
                    var dialog = new MessageDialog("Passwords does not match");
                    await dialog.ShowAsync();
                }
            }
           

            if (TryParsePhoneNumber(PhoneNumberInput))
            {
                MC.CurrentUser.Number = _phoneNumberAsInt;
            }
            else
            {
                var dialog = new MessageDialog("Please enter your 8 digits phonenumber");
                await dialog.ShowAsync();
            }

            // Gem alt
            MC.SaveEverything();
        }

        
        private bool TryParsePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 8)
            {
                return false;
            }
            return int.TryParse(phoneNumber, out _phoneNumberAsInt);
            
        }

        private bool PasswordChecker()
        {
            if (NewPassword.Equals(NewPasswordCheck) && CurrentPassword.Equals(MC.CurrentUser.Password)) return true;
            return false;
        }

        /// <summary>
        /// Metode til at logge ud
        /// </summary>
        private void LogOutMethod()
        {
            MC.CurrentUser = null;
            ((Frame)Window.Current.Content).Navigate(typeof(LoginView));
        }

        /// <summary>
        /// Metode til visning af userGrid
        /// </summary>
        private void OpenUserProfile()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.MyProfileVisibility = true;

        }

        #region Properties

        public RelayCommand UserProfileButton { get; set; }

        public ModelController MC
        {
            get { return _mC; }
            set { _mC = value; }
        }

        public RelayCommand LogOutCommand
        {
            get { return _logOutCommand; }
            set { _logOutCommand = value; }
        }

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                OnPropertyChanged();
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }

        public string NewPasswordCheck
        {
            get { return _newPasswordCheck; }
            set
            {
                _newPasswordCheck = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveChangesCommand
        {
            get { return _saveChangesCommand; }
            set { _saveChangesCommand = value; }
        }

        public string PhoneNumberInput
        {
            get { return _phoneNumberInput; }
            set
            {
                _phoneNumberInput = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotifyProperty

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
