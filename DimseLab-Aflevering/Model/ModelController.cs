using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static ModelController _instance = null;
        private Project _currentProject;

        public bool LoggedIn { get; set; }

        ObservableCollection<Project> _projectList = new ObservableCollection<Project>();

        List<Doohickey> _doohickeyList = new List<Doohickey>();

        List<User> _userList = new List<User>();

        public User CurrentUser { get; set; }

        // Creates mediator pattern
        public User User;
        public Doohickey Doohickey;
        public Project Project;

        private ModelController()
        {
            // TODO CurrentUser skal sættes til den bruger, der er logget på
            CurrentUser = new User("Lars", "Truelsen", 4612456, "lars@easj.dk");

            //TODO get loginToken - tjek om der er logget på
            LoggedIn = true;

            // Creates mediator pattern
            User = new User(this);
            Doohickey = new Doohickey(this);
            Project = new Project(this);

            // Initierer visibility bools
            SetAllInvisible();

            // Viser browse
            BrowseVisibility = true;

            // Load data
            LoadEverything();

        }

        public void SetAllInvisible()
        {
            BrowseVisibility = false;
            MyProjectsVisibility = false;
            MyProfileVisibility = false;
            AdminVisibility = false;
            EditProjectVisibility = false;
        }

        public void SendSpecificProjectToIndexNul(int ID)
        {
            for (int i = 0; i < ProjectList.Count; i++)
            {
                if (ProjectList[i].ID == ID)
                {
                    CurrentProject = ProjectList[i];
                    Debug.WriteLine(CurrentProject);
                }
            }
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
            await XMLReadWrite.SaveObjectToXml<ObservableCollection<Project>>(ProjectList, "ProjectModel.xml");
        }

        private async void LoadProjectsAsync()
        {
            Debug.WriteLine("loading projects async...");
            ProjectList = await XMLReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Project>>("ProjectModel.xml");
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
            }
        }

        public List<Doohickey> DoohickeyList
        {
            get { return _doohickeyList; }
            set
            {
                _doohickeyList = value;
            }
        }

        public ObservableCollection<Project> ProjectList
        {
            get { return _projectList; }
            set { _projectList = value; }
        }

        public static ModelController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ModelController();
                }
                return _instance;
            }
        }

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                OnPropertyChanged();
            }
        }

        public bool BrowseVisibility { get; set; }

        public bool MyProjectsVisibility { get; set; }

        public bool MyProfileVisibility { get; set; }

        public bool AdminVisibility { get; set; }

        public bool EditProjectVisibility { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
