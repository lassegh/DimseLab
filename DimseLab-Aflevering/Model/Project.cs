using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;

namespace DimseLab_Aflevering.Model
{
    public class Project : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private DateTimeOffset _projectBeginDate;
        private DateTimeOffset _projectEndDate;

        private ObservableCollection<User> _projectMembers;
        private ObservableCollection<Doohickey> _borrowedItems;

        private bool isFinished;

        private ModelController _mc;

        public Project()
        {
            // Bruges ifm lagring
        }

        public Project(ModelController mc)
        {
            _mc = mc;
        }

        public Project(string inputName, string inputDescription, DateTimeOffset inputEndDate, int id)
        {
            Name = inputName;
            Description = inputDescription;
            ProjectBeginDate = DateTime.Now; //vælger nuværende dag og tidspunkt
            ProjectEndDate = inputEndDate; //FRONT END NIGGAS: BRUG DATEPICKER som input til denne her TODO valgte dato huskes ikke, når programmet hentes efter lukning
            IsFinished = false; //for den er jo ikke færdig når den lige er blevet skabt
            ProjectMembers = new ObservableCollection<User>();
            BorrowedItems = new ObservableCollection<Doohickey>();
            ID = id; // Sætter id på projekter
        }

        #region VariablesnProperties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int ID { get; set; } // denne kan ikke ses når der ikke er lavet encapsulate field på.. ihvertfald kan jeg ikke kalde den andre steder /Michael
        // Den er public så kald den lige så tosset du vil :-S    ...... prop => TAB forever

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTimeOffset ProjectBeginDate
        {
            get { return _projectBeginDate; }
            set { _projectBeginDate = value; }
        }

        public DateTimeOffset ProjectEndDate
        {
            get { return _projectEndDate; }
            set { _projectEndDate = value; }
        }

        public ObservableCollection<Doohickey> BorrowedItems
        {
            get { return _borrowedItems; }
            set { _borrowedItems = value; }
        }

        public bool IsFinished
        {
            get { return isFinished; }
            set
            {
                isFinished = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> ProjectMembers
        {
            get { return _projectMembers; }
            set { _projectMembers = value; }
        }

        #endregion

        #region Methods

        //før vi kan lukke et projekt, skal vi tjekke om det har nogle lån
        public bool CheckProjectForLoans()
        {
            if(BorrowedItems.Count == 0) //hvis der er 0 nul items i listen af lån
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //at slutte et projekt
        public void FinishProject()
        {
            IsFinished = true;
            Name = Name + " (afsluttet)";

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
