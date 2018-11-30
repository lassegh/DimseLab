using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;

namespace DimseLab_Aflevering.Model
{
    class Project : INotifyPropertyChanged
    {

        //Imma region
        #region VariablesnProperties

        private string _name;
        private string _description;
        private DateTime _projectBeginDate;
        private DateTime _projectEndDate;

        private List<User> _projectMembers;
        private List<Doohickey> _borrowedItems;

        private bool isFinished;


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTime ProjectBeginDate
        {
            get { return _projectBeginDate; }
            set { _projectBeginDate = value; }
        }

        public DateTime ProjectEndDate
        {
            get { return _projectEndDate; }
            set { _projectEndDate = value; }
        }

        public List<Doohickey> BorrowedItems
        {
            get { return _borrowedItems; }
            set { _borrowedItems = value; }
        }

        public bool IsFinished
        {
            get { return isFinished; }
            set { isFinished = value; }
        }

        public List<User> ProjectMembers
        {
            get { return _projectMembers; }
            set { _projectMembers = value; }
        }

        #endregion

        public Project()
        {
            
        }

        public Project(string inputName, string inputDescription, DateTime inputEndDate)
        {
            Name = inputName;
            Description = inputDescription;
            ProjectBeginDate = DateTime.Now; //vælger nuværende dag og tidspunkt
            ProjectEndDate = inputEndDate; //FRONT END NIGGAS: BRUG DATEPICKER som input til denne her
            IsFinished = false; //for den er jo ikke færdig når den lige er blevet skabt
            ProjectMembers = new List<User>();
        }



        #region Methods

        //før vi kan lukke et projekt, skal vi tjekke om det har nogle lån
        public void CheckProjectForLoans()
        {
            if(BorrowedItems.Count == 0) //hvis der er 0 nul items i listen af lån
            {
                FinishProject(); //så kalder vi koden der afslutter projektet
            }
            else
            {
                Debug.Write("ERROR CODE LARS: Projektet har stadig udlån");
            }
        }


        //at slutte et projekt
        public void FinishProject()
        {
            ProjectMembers.Clear(); //fjerner alle elementer fra listen
            IsFinished = true;

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
