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
        // Opretter reference til ModelController
        private ModelController _mC = ModelController.Instance;
        
        RelayCommand _saveButton;

        RelayCommand _loadButton;

        private int _numberOfProjects;
        private int _numberOfActiveProjects;
        private int _numberOfUsers;
        private int _numberOfDoohickeys;
        private int _numberOfCurrentLoans;


        /// <summary>
        /// Constructor
        /// </summary>
        public AdminViewModel()
        {
            // Knap til visning af adminGrid
            AdminButton = new RelayCommand(OpenAdmin);
            CalculateStats();

        }

        /// <summary>
        /// Metode til visning af adminGrid
        /// </summary>
        private void OpenAdmin()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.AdminVisibility = true;



            CalculateStats();
        }

        private void CalculateStats()
        {


            NumberOfUsers = MC.UserList.Count;
            NumberOfProjects = MC.ProjectList.Count;
            NumberOfDoohickeys = MC.DoohickeyList.Count;


            NumberOfActiveProjects = 0; //reset
            //for at tælle antallet af aktive projekter
            foreach (Project PROJ in MC.ProjectList)
            {
                if (PROJ.IsFinished == false)
                {
                    NumberOfActiveProjects++;
                }
            }

            NumberOfCurrentLoans = 0; //reset
            //for at tælle antallet af aktive udlån
            foreach (Project PROJ in MC.ProjectList)
            {
                NumberOfCurrentLoans = NumberOfCurrentLoans + PROJ.BorrowedItems.Count;
            }
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

        public ModelController MC
        {
            get { return _mC; }
            set { _mC = value; }
        }

        public int NumberOfProjects
        {
            get { return _numberOfProjects; }
            set { _numberOfProjects = value; }
        }

        public int NumberOfUsers
        {
            get { return _numberOfUsers; }
            set { _numberOfUsers = value; }
        }

        public int NumberOfDoohickeys
        {
            get { return _numberOfDoohickeys; }
            set { _numberOfDoohickeys = value; }
        }

        public int NumberOfActiveProjects
        {
            get { return _numberOfActiveProjects; }
            set { _numberOfActiveProjects = value; }
        }

        public int NumberOfCurrentLoans
        {
            get { return _numberOfCurrentLoans; }
            set { _numberOfCurrentLoans = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
