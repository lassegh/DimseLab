﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
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

        /// <summary>
        /// Constructor
        /// </summary>
        public UserViewModel()
        {
            // Knap til visning af userGrid
            UserProfileButton = new RelayCommand(OpenUserProfile);

            // Log ud knap
            LogOutCommand = new RelayCommand(LogOutMethod);
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
