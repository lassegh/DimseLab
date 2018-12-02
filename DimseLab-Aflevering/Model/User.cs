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

        public User()
        {
            // Bruges ifm lagring
        }

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

        /*
        // A fix for the async method "AddNewUser"
        #region FixAsyncAddNewUser

        public void CallAddNewUser()
        {
            AddNewUser();
        }

        #endregion

        
        //Method that adds a new user or otherwise tells the user if they used a wrong email
        public async Task AddNewUser()
        {
            if (UserInputEmail.Contains("@easj.dk".ToLower()) && string.IsNullOrWhiteSpace(UserInputEmail) || string.IsNullOrWhiteSpace(UserInputFirstName) || string.IsNullOrWhiteSpace(UserInputLastName))
            {
                // TODO er det ikke kun i dette tilfælde, der skal bruges async?
                // Wrong email used
                var dialog = new MessageDialog("Invalid Email Used");
                await dialog.ShowAsync();
            }
            else
            {   // Adds new user
                // TODO oprettelse af denne type object skal ikke ske her
                UserList.Add(new User(UserInputFirstName, UserInputLastName, UserInputNumber, UserInputEmail));
            }
        }
        */

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
