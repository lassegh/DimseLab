using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Storage;
using DimseLab_Aflevering.Annotations;

namespace DimseLab_Aflevering.Model
{
    public class ModelController : INotifyPropertyChanged
    {
        public bool LoggedIn { get; set; }

        List<Project> _projectList = new List<Project>();

        List<Doohickey> _doohickeyList = new List<Doohickey>();

        List<User> _userList = new List<User>();

        public User CurrentUser { get; set; }
        
        // Creates mediator pattern
        public User User;
        public Doohickey Doohickey;
        public Project Project; 

        public ModelController()
        {
            // TODO CurrentUser skal sættes til den bruger, der er logget på
            CurrentUser = new User("Lars", "Truelsen", 4612456, "lars@easj.dk");

            //TODO get loginToken - tjek om der er logget på
            LoggedIn = true;

            // Creates mediator pattern
            User = new User(this);
            Doohickey = new Doohickey(this);
            Project = new Project(this);

        }

        #region readingNwriting

        public void SaveEverything()
        {
            SaveProjectsAsync();
            /*
            WriteDoohickeyData();
            WriteUserData();*/
        }

        public void LoadEverything()
        {
            LoadProjectsAsync();
            /*
            LoadDoohickeyData();
            LoadUserData();*/
        }

        public async void SaveProjectsAsync()
        {
            Debug.WriteLine("Saving projects async...");
            await XMLReadWrite.SaveObjectToXml<List<Project>>(ProjectList, "ProjectModel.xml");
        }

        private async void LoadProjectsAsync()
        {
            Debug.WriteLine("loading projects async...");
            ProjectList = await XMLReadWrite.ReadObjectFromXmlFileAsync<List<Project>>("ProjectModel.xml");
        }

        /*
        public async Task WriteDoohickeyData()
        {
            await SaveObjectToXml(DoohickeyList, "DoohickeyData.xml");
        }

        public void ReadDoohickeyData()
        {

        }

        public async Task WriteUserData()
        {
            await SaveObjectToXml(UserList, "UserData.xml");
        }

        public void ReadUserData()
        {

        }*/

        #endregion




        public List<User> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }

        public List<Doohickey> DoohickeyList
        {
            get { return _doohickeyList; }
            set
            {
                _doohickeyList = value;
                OnPropertyChanged();
            }
        }

        public List<Project> ProjectList
        {
            get { return _projectList; }
            set
            {
                _projectList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
