using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimseLab_Aflevering.Model
{
    class Project
    {

        //Imma region
        #region VariablesnProperties

        private string _name;
        private string _description;
        private DateTime _projectBeginDate;
        private DateTime _projectEndDate;

        private List<Doohickey> _borrowedItems;

        private bool isFinished;


        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        #endregion

        public Project(string inputName, string inputDescription, DateTime inputEndDate)
        {
            Name = inputName;
            Description = inputDescription;
            ProjectBeginDate = DateTime.Now; //vælger nuværende dag og tidspunkt
            ProjectEndDate = inputEndDate; //FRONT END NIGGAS: BRUG DATEPICKER som input til denne her
            IsFinished = false; //for den er jo ikke færdig når den lige er blevet skabt
        }

    }
}
