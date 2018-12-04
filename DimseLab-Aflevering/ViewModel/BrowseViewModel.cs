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

        // Kopierer user listen
        private ObservableCollection<User> _userList = ModelController.Instance.UserList;

        // Kopierer projektListen fra ModelController
        private ObservableCollection<Project> _projectList = ModelController.Instance.ProjectList;

        // Kopierer doohickey listen
        private ObservableCollection<Doohickey> _doohickeyList = ModelController.Instance.DoohickeyList;

        /// <summary>
        /// Constructor
        /// </summary>
        public BrowseViewModel()
        { 
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

        public ObservableCollection<Doohickey> DoohickeyList
        {
            get { return _doohickeyList; }
            set { _doohickeyList = value; }
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
