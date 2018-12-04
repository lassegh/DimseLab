using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class MyProjectsViewModel : INotifyPropertyChanged
    {
        // Opretter reference til ModelController
        private ModelController _mC = ModelController.Instance;

        // Kopierer projektListen til myProjects
        private ObservableCollection<Project> _myProjects = new ObservableCollection<Project>();

        // Instanser ifm oprettelse af nyt projekt
        private string _inputProjectName;
        private string _inputProjectDescribtion;
        private DateTime _inputProjectDate;
        private DateTime _projectEndDate;

        // Knap til tilføjelse af projekt
        private RelayCommand _relayAddProject;

        // Knap til afslutning af projekt
        private RelayCommand _endProjectCommand;

        // Knap til opdatering af redigering
        private RelayCommand _saveEditingCommand;

        // String til søgning efter brugere til projekt
        private string _searchForUserString;

        // String til søgning efter dimser til projekt
        private string _searchForDoohickeyString;

        // Liste af søgte brugere
        private ObservableCollection<User> _searchUsers = new ObservableCollection<User>();

        /// <summary>
        /// Constructor
        /// </summary>
        public MyProjectsViewModel()
        {
            // Knap til tilføjelse af projekt
            RelayAddProject = new RelayCommand(AddNewProject);

            // Knap til visning af myProjectsGrid
            ManageProjectsButton = new RelayCommand(OpenMyProjects);

            // Knap til redigering af valgt projekt
            SelectedProjectCommand = new RelayCommand<int>(OnClickProjectInList);

            // Knap til afslutning af projekt
            EndProjectCommand = new RelayCommand(EndProject);

            // Knap til opdatering af redigering
            SaveEditingCommand = new RelayCommand(SaveProject);

            //Opdaterer liste
            UpdateData();

            // 
            
        }

        /// <summary>
        /// Gem redigering af projekt
        /// </summary>
        private void SaveProject()
        {
            // Gemmer til liste i ModelController
            foreach (Project project in MC.ProjectList)
            {
                if (project.ID == MC.CurrentProject.ID)
                {
                    project.BorrowedItems = MC.CurrentProject.BorrowedItems;
                    project.Description = MC.CurrentProject.Description;
                    project.ProjectMembers = MC.CurrentProject.ProjectMembers;
                }
            }

            // Gemmer til listen myProjects
            foreach (Project project in MyProjects)
            {
                if (project.ID == MC.CurrentProject.ID)
                {
                    project.BorrowedItems = MC.CurrentProject.BorrowedItems;
                    project.Description = MC.CurrentProject.Description;
                    project.ProjectMembers = MC.CurrentProject.ProjectMembers;
                }
            }
            ModelController.Instance.SaveEverything(); // Her gemmes - Der gemmes til disk.
            OpenMyProjects(); // Viser mine projekter
        }

        /// <summary>
        /// Metode til afslutning af projekt
        /// </summary>
        private void EndProject()
        {
            //kalder metoden inde i projektet for at afslutte det
            MC.CurrentProject.FinishProject();
        }

        /// <summary>
        /// Metode til visning af MyProjectsGrid
        /// </summary>
        private void OpenMyProjects()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.MyProjectsVisibility = true;
        }

        /// <summary>
        /// Metode til visning af redigeringsside af valgt projekt
        /// </summary>
        /// <param name="i">Id'et for valgte projekt</param>
        public void OnClickProjectInList(int i)
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.EditProjectVisibility = true;
            ModelController.Instance.SendSpecificProjectToCurrentProject(i);
        }

        /// <summary>
        /// Filtrerer MyProjects listen, så det kun er projekter jeg deltager i, der bliver vist
        /// </summary>
        private void UpdateData()
        {
            // MyProjects skal have filtreret de projekter, hvor brugeren indgår. Metode skal kaldes fra constructoren, så den kun køres ved programmets opstart.
            // Loops though every project and compares if the email fits the current users email. 
            foreach (Project project in MC.ProjectList)
            {
                // TODO Tjek om projekt er afsluttet
                // Removes any project, that is not 'used' by the user, that is loggedIn
                if (project.ProjectMembers.Any(x => x.Email == ModelController.Instance.CurrentUser.Email))
                {
                    MyProjects.Add(project);
                }
            }
        }

        /// <summary>
        /// Tilføjer nyt projekt til alle lister
        /// </summary>
        public void AddNewProject()
        {
            if (string.IsNullOrWhiteSpace(InputProjectName) || string.IsNullOrWhiteSpace(InputProjectDescribtion))
            {

            }
            else
            {
                // Udregner id til næste projekt
                int id = 0;
                foreach (Project project in ModelController.Instance.ProjectList)
                {
                    if (project.ID > id)
                    {
                        id = project.ID;
                    }

                    id++;
                }

                // Add Project
                Project newProject = new Project(InputProjectName, InputProjectDescribtion, InputProjectDate, id);
                newProject.ProjectMembers.Add(ModelController.Instance.CurrentUser);
                ModelController.Instance.ProjectList.Add(newProject);// Tilføjer projekt til hovedlisten
                MyProjects.Add(newProject);// Tilføjer projekt til MyProjects
                ModelController.Instance.SaveEverything(); // Her gemmes - Der gemmes til disk.
            }

            // TODO Når der tilføjes et projekt skal indtastningerne i GUI slettes
        }




        #region Properties

        public ObservableCollection<Project> MyProjects
        {
            get { return _myProjects; }
            set
            {
                _myProjects = value;
                OnPropertyChanged();
            }
        }

        public DateTime ProjectEndDate
        {
            get { return _projectEndDate; }
            set
            {
                _projectEndDate = value;
                OnPropertyChanged();
            }
        }

        public string InputProjectName
        {
            get { return _inputProjectName; }
            set { _inputProjectName = value; }
        }

        public string InputProjectDescribtion
        {
            get { return _inputProjectDescribtion; }
            set { _inputProjectDescribtion = value; }
        }

        public DateTime InputProjectDate
        {
            get { return _inputProjectDate; }
            set { _inputProjectDate = value; }
        }

        public RelayCommand RelayAddProject
        {
            get { return _relayAddProject; }
            set { _relayAddProject = value; }
        }

        public RelayCommand ManageProjectsButton { get; set; }

        public RelayCommand<int> SelectedProjectCommand { get; set; }

        public ModelController MC
        {
            get { return _mC; }
            set
            {
                _mC = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand EndProjectCommand
        {
            get { return _endProjectCommand; }
            set { _endProjectCommand = value; }
        }

        public string SearchForUserString
        {
            get { return _searchForUserString; }
            set
            {
                _searchForUserString = value;
                SearchUsers.Clear();
                foreach (User user in RegexSearch.SearchUsers(SearchForUserString, MC.UserList))
                {
                    SearchUsers.Add(user);
                }
                OnPropertyChanged();
            }
        }

        public string SearchForDoohickeyString
        {
            get { return _searchForDoohickeyString; }
            set { _searchForDoohickeyString = value; }
        }

        public RelayCommand SaveEditingCommand
        {
            get { return _saveEditingCommand; }
            set { _saveEditingCommand = value; }
        }

        public ObservableCollection<User> SearchUsers
        {
            get { return _searchUsers; }
            set { _searchUsers = value; }
        }

        #endregion


        #region NotifyPropChange

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }


}
