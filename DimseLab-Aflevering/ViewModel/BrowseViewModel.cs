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


        // Opretter instans af ModelController
        private ModelController _mc = new ModelController();

        public BrowseViewModel()
        { 
            // TODO oprettelse af denne type object skal ikke ske her
            // Placeholder Users
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            

            // Load data
            Mc.LoadEverything();
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


        public ModelController Mc
        {
            get { return _mc; }
            set { _mc = value; }
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
