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
        private DateTime _projectEndDate;// TODO sæt slutdato til d.d. + 30 dage

        // Knap til tilføjelse af projekt
        private RelayCommand _relayAddProject;

        // Knap til afslutning af projekt
        private RelayCommand _endProjectCommand;

        // Knap til opdatering af redigering
        private RelayCommand _saveEditingCommand;

        // Knap til tilføjelse af dims til projekt
        private RelayCommand<int> _addDoohickeyToProjectCommand;

        // Knap til tilføjelse af bruger til projekt
        private RelayCommand<string> _addUserToProjectCommand;

        // Knap, der fjerner dims fra projekt
        private RelayCommand<int> _removeDoohickeyFromProjectCommand;

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

            // Knap til tilføjelse af dims til projekt
            AddDoohickeyToProjectCommand = new RelayCommand<int>(AddDoohickeyToProject);

            // Knap til tilføjelse af bruger til projekt
            AddUserToProjectCommand = new RelayCommand<string>(AddUserToProject);

            // Knap, der fjerner dims fra projekt
            RemoveDoohickeyFromProjectCommand = new RelayCommand<int>(RemoveDoohickeyFromProject);

            //Opdaterer liste
            UpdateData();
        }

        /// <summary>
        /// Fjerner lånt dims fra projekt
        /// </summary>
        /// <param name="id">dimsens id</param>
        private void RemoveDoohickeyFromProject(int id)
        {
            int doohickeyPosition = 0;
            for (int i = 0; i < MC.CurrentProject.BorrowedItems.Count; i++)
            {
                if (MC.CurrentProject.BorrowedItems[i].ID == id)
                {
                    doohickeyPosition = i;
                }
            }
            MC.CurrentProject.BorrowedItems.RemoveAt(doohickeyPosition);
            MC.SaveEverything();
        }

        /// <summary>
        /// Tilføjer bruger til projekt
        /// </summary>
        /// <param name="mail">mailen på den bruger, der tilføjes</param>
        private void AddUserToProject(string mail)
        {
            foreach (User user in MC.UserList)
            {
                if (user.Email.Equals(mail))
                {
                    MC.CurrentProject.ProjectMembers.Add(user);
                }
            }
            MC.SaveEverything();
        }

        /// <summary>
        /// Tilføjer en dims til et projekt
        /// </summary>
        /// <param name="id">id'et på dimsen</param>
        private void AddDoohickeyToProject(int id)
        {
            foreach (Doohickey doohickey in MC.DoohickeyList)
            {
                if (doohickey.ID == id)
                {
                    MC.CurrentProject.BorrowedItems.Add(doohickey);
                }
            }
            MC.SaveEverything();
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
            
            // Kalder metoden, der sender brugeren tilbage til MyProjects
            OpenMyProjects();
            ModelController.Instance.SaveEverything(); // Her gemmes - Der gemmes til disk.
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
                if (project.ProjectMembers.Any(x => x.Email.ToLower() == ModelController.Instance.CurrentUser.Email.ToLower()))
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

        public RelayCommand SaveEditingCommand
        {
            get { return _saveEditingCommand; }
            set { _saveEditingCommand = value; }
        }

        public RelayCommand<int> AddDoohickeyToProjectCommand
        {
            get { return _addDoohickeyToProjectCommand; }
            set { _addDoohickeyToProjectCommand = value; }
        }

        public RelayCommand<string> AddUserToProjectCommand
        {
            get { return _addUserToProjectCommand; }
            set { _addUserToProjectCommand = value; }
        }

        public RelayCommand<int> RemoveDoohickeyFromProjectCommand
        {
            get { return _removeDoohickeyFromProjectCommand; }
            set { _removeDoohickeyFromProjectCommand = value; }
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
