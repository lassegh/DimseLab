using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Model;

namespace DimseLab_Aflevering
{
    public class User
    {
        private String _firstName;
        private String _lastName;
        private int _number;
        private string _email;
        private ModelController _mc;

        public User(ModelController mc)
        {
            _mc = mc;
        }

        public User(string inputFirstName, string inputLastName, int inputNumber, string inputEmail)
        {
            FirstName = inputFirstName;
            LastName = inputLastName;
            Number = inputNumber;
            Email = inputEmail;

        }

        #region Get and Set Properties

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

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        #endregion

    }
}
