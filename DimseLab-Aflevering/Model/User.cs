﻿using System;
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
        private int _number;
        private string _email;



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


        public User(string inputFirstName, string inputLastName, int inputNumber, string inputEmail)
        {
            FirstName = inputFirstName;
            LastName = inputLastName;
            Number = inputNumber;
            Email = inputEmail;

        }
    }
}
