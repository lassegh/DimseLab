using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimseLab_Aflevering.Model
{
    class Helper
    {

        List<Project> _projectList = new List<Project>();

        List<Doohickey> _doohickeyList = new List<Doohickey>();

        List<User> _userList = new List<User>();


        public Helper()
        {
          
            


        }


        public void WriteProjectData()
        {

        }

        public void ReadProjectData()
        {

        }

        public void WriteDoohickeyData()
        {

        }

        public void ReadDoohickeyData()
        {

        }

        public void WriteUserData()
        {

        }

        public void ReadUserData()
        {

        }

        public List<User> UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }

        public List<Doohickey> DoohickeyList
        {
            get { return _doohickeyList; }
            set { _doohickeyList = value; }
        }

        public List<Project> ProjectList
        {
            get { return _projectList; }
            set { _projectList = value; }
        }
    }
}
