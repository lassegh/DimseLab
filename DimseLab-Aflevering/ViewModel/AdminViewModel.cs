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
    

    class AdminViewModel : INotifyPropertyChanged
    {
        private bool _browseVisibility;
        private bool _myProjectsVisibility;
        private bool _myProfileVisibility;
        private bool _adminVisibility;
        private bool _editProjectVisibility;
        
        RelayCommand _saveButton;

        RelayCommand _loadButton;





        public AdminViewModel()
        {
            //RelayLoadButton = new RelayCommand(helper.LoadEverything);


            AdminButton = new RelayCommand(OpenAdmin);
        }

        private void OpenAdmin()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.AdminVisibility = true;
            BrowseVisibility = ModelController.Instance.BrowseVisibility;
            MyProjectsVisibility = ModelController.Instance.MyProjectsVisibility;
            MyProfileVisibility = ModelController.Instance.MyProfileVisibility;
            AdminVisibility = ModelController.Instance.AdminVisibility;
            EditProjectVisibility = ModelController.Instance.EditProjectVisibility;
        }

        public RelayCommand SaveButton
        {
            get { return _saveButton; }
            set { _saveButton = value; }
        }

        public RelayCommand LoadButton
        {
            get { return _loadButton; }
            set { _loadButton = value; }
        }

        public RelayCommand AdminButton { get; set; }

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
            get { return _adminVisibility; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
