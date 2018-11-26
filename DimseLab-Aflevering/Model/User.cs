using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimseLab_Aflevering
{
    class User
    {
        private String _firstName;
        private String _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }



        public User(string inputFirstName, string inputLastName)
        {
            FirstName = inputFirstName;
            LastName = inputLastName;
        }
    }
}
