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
        // Opretter reference til ModelController
        private ModelController _mC = ModelController.Instance;

        // User liste - TODO denne er midlertidig, da den skal komme fra modelController på samme måde som projektlisten
        ObservableCollection<User> _userList = new ObservableCollection<User>();

        // Kopierer projektListen fra ModelController
        private ObservableCollection<Project> _projectList = ModelController.Instance.ProjectList;

        /// <summary>
        /// Constructor
        /// </summary>
        public BrowseViewModel()
        { 
            // TODO oprettelse af denne type object skal ikke ske her, men i userViewModel
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));

            // Knap til visning af browseGrid
            BrowseButton = new RelayCommand(BrowseButtonMethod);
        }

        /// <summary>
        /// Metode til visning af BrowseGrid
        /// </summary>
        void BrowseButtonMethod()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.BrowseVisibility = true;

;
        }


        #region Properties

        public ObservableCollection<User> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<Project> ProjectList
        {
            get { return _projectList; }
            set { _projectList = value; }
        }

        public RelayCommand BrowseButton { get; set; }

        public ModelController MC
        {
            get { return _mC; }
            set
            {
                _mC = value;
                OnPropertyChanged();
            }
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
