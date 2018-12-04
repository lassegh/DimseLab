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
        // instans af singleton
        private static ModelController _instance = null;

        // instans af currentProject - bruges ifm redigering af projekter
        private Project _currentProject;

        // Views Visibility
        private bool _browseVisibility;
        private bool _myProjectsVisibility;
        private bool _myProfileVisibility;
        private bool _adminVisibility;
        private bool _editProjectVisibility;

        // instans af OC af projekter - bruges både i browse og i myProjects
        private ObservableCollection<Project> _projectList = new ObservableCollection<Project>();

        // instans af OC af dimser - bruges både i browse og i myProjects
        List<Doohickey> _doohickeyList = new List<Doohickey>();

        // instans af OC af brugere - bruges både i browse og i myProjects
        ObservableCollection<User> _userList = new ObservableCollection<User>();

        // Nuværende bruger - sættes til null, så der senere kan tjekkes om der er logget på
        private User _currentUser = null;

        // Liste af søgte brugere
        private ObservableCollection<User> _searchUsers = new ObservableCollection<User>();

        // Creates mediator pattern
        public User User;
        public Doohickey Doohickey;
        public Project Project;

        private ModelController()
        {
            // Creates mediator pattern
            User = new User(this);
            Doohickey = new Doohickey(this);
            Project = new Project(this);

            // Initierer visibility bools til false
            SetAllInvisible();

            // Opretter brugere
            HardcodedUsers();

            // Viser browse
            BrowseVisibility = true;

            // Load data
            LoadEverything();
        }

        public void SearchForUsers(String searchString)
        {
            SearchUsers.Clear(); // Listen med søgte brugere nulstilles
            foreach (User user in RegexSearch.SearchUsers(searchString, UserList)) // Der tilføjes brugere ifølge Regexsearch
            {
                SearchUsers.Add(user);
            }
        }

        // Laver brugere hardcoded
        private void HardcodedUsers()
        {
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Ungobungo", "BangoBong", 46375817, "Ungogabe@edu.easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
            UserList.Add(new User("Lars", "Truelsen", 46375817, "Lars@easj.dk"));
        }

        /// <summary>
        /// Sætter alle grids til invisible
        /// </summary>
        public void SetAllInvisible()
        {
            BrowseVisibility = false;
            MyProjectsVisibility = false;
            MyProfileVisibility = false;
            AdminVisibility = false;
            EditProjectVisibility = false;
        }

        /// <summary>
        /// Sætter projekt til currentProject, så det kan redigeres
        /// </summary>
        /// <param name="ID">Id'et på projektet</param>
        public void SendSpecificProjectToCurrentProject(int ID)
        {
            for (int i = 0; i < ProjectList.Count; i++)
            {
                if (ProjectList[i].ID == ID)
                {
                    CurrentProject = ProjectList[i];
                }
            }
            // TODO Hvis currentProject / projekt, der prøves tilgås er afsluttet, skal man ikke sendes til redigeringssiden
        }

        #region readingNwriting

        /// <summary>
        /// Gemmer alt til fil(er) (lister af projekter, dimser og brugere) 
        /// </summary>
        public void SaveEverything()
        {
            SaveProjectsAsync();
            /*
            WriteDoohickeyData();
            WriteUserData();*/
        }

        /// <summary>
        /// Henter alt fra fil(er) (lister af projekter, dimser og brugere) 
        /// </summary>
        public void LoadEverything()
        {
            LoadProjectsAsync();
            /*
            LoadDoohickeyData();
            LoadUserData();*/
        }

        /// <summary>
        /// Gemmer projektListe
        /// </summary>
        public async void SaveProjectsAsync()
        {
            Debug.WriteLine("Saving projects async...");
            await XMLReadWrite.SaveObjectToXml<ObservableCollection<Project>>(ProjectList, "ProjectModel.xml");
        }

        /// <summary>
        /// Henter projektListe
        /// </summary>
        private async void LoadProjectsAsync()
        {
            try
            {
                Debug.WriteLine("loading projects async...");
                ProjectList = await XMLReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Project>>("ProjectModel.xml");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                
            }
            
        }

        #endregion
        
        #region Properties

        public ObservableCollection<User> UserList
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

        public bool BrowseVisibility
        {
            get { return _browseVisibility; }
            set
            {
                _browseVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool MyProjectsVisibility
        {
            get { return _myProjectsVisibility; }
            set
            {
                _myProjectsVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool MyProfileVisibility
        {
            get { return _myProfileVisibility; }
            set
            {
                _myProfileVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool AdminVisibility
        {
            get { return _adminVisibility; }
            set
            {
                _adminVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool EditProjectVisibility
        {
            get { return _editProjectVisibility; }
            set
            {
                _editProjectVisibility = value;
                OnPropertyChanged();
            }
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    // TODO Send til login og slet næste linie
                    _currentUser = new User("Lars", "Truelsen", 4612456, "lars@easj.dk");
                }
                return _currentUser;
            }
        }

        public ObservableCollection<User> SearchUsers
        {
            get { return _searchUsers; }
            set { _searchUsers = value; }
        }

        #endregion

        #region INotifyProperty

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
