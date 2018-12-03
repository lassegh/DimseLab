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
        private ModelController _mC = ModelController.Instance;

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

        public ModelController MC
        {
            get { return _mC; }
            set { _mC = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
