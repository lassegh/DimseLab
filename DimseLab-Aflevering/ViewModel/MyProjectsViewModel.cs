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
        private bool _browseVisibility;
        private bool _myProjectsVisibility;
        private bool _myProfileVisibility;
        private bool _adminVisibility;
        private bool _editProjectVisibility;
        private ObservableCollection<Project> _myProjects = ModelController.Instance.ProjectList;
        private DateTime _projectEndDate;
        private Project _currentProject = ModelController.Instance.CurrentProject;

        private string _inputProjectName;
        private string _inputProjectDescribtion;
        private DateTime _inputProjectDate;

        private RelayCommand _relayAddProject;

        public MyProjectsViewModel()
        {
            RelayAddProject = new RelayCommand(AddNewProject);

            ManageProjectsButton = new RelayCommand(OpenMyProjects);
            SelectedProjectCommand = new RelayCommand<int>(OnClickProjectInList);
        }

        private void OpenMyProjects()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.MyProjectsVisibility = true;
            BrowseVisibility = ModelController.Instance.BrowseVisibility;
            MyProjectsVisibility = ModelController.Instance.MyProjectsVisibility;
            MyProfileVisibility = ModelController.Instance.MyProfileVisibility;
            AdminVisibility = ModelController.Instance.AdminVisibility;
            EditProjectVisibility = ModelController.Instance.EditProjectVisibility;
        }

        public void OnClickProjectInList(int i)
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.EditProjectVisibility = true;
            BrowseVisibility = ModelController.Instance.BrowseVisibility;
            MyProjectsVisibility = ModelController.Instance.MyProjectsVisibility;
            MyProfileVisibility = ModelController.Instance.MyProfileVisibility;
            AdminVisibility = ModelController.Instance.AdminVisibility;
            EditProjectVisibility = ModelController.Instance.EditProjectVisibility;
            ModelController.Instance.SendSpecificProjectToIndexNul(i);

            //Her skal CurrentProject opdateres
            CurrentProject = ModelController.Instance.CurrentProject;
        }

        private void UpdateData()
        {
            // TODO filtrering virker ikke. MyProjects skal have filtreret de projekter fra, hvor brugeren IKKE indgår
            // Loops though every project and compares if the email fits the current users email. 
            foreach (Project project in MyProjects)
            {
                // Removes any project, that is not 'used' by the user, that is loggedIn
                if (project.ProjectMembers.Any(x => x.Email != ModelController.Instance.CurrentUser.Email))
                {
                    MyProjects.Remove(project);
                }
            }
        }

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
        }

        // TODO Når der tilføjes et projekt skal indtastningerne i GUI slettes


        #region Get & Set Properties

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

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ManageProjectsButton { get; set; }

        public RelayCommand<int> SelectedProjectCommand { get; set; }

        public bool BrowseVisibility
        {
            get { return _browseVisibility; }
            set { _browseVisibility = value; }
        }

        public bool MyProjectsVisibility
        {
            get { return _myProjectsVisibility; }
            set { _myProjectsVisibility = value; }
        }

        public bool MyProfileVisibility
        {
            get { return _myProfileVisibility; }
            set { _myProfileVisibility = value; }
        }

        public bool AdminVisibility
        {
            get { return _adminVisibility; }
            set { _adminVisibility = value; }
        }

        public bool EditProjectVisibility
        {
            get { return _editProjectVisibility; }
            set { _editProjectVisibility = value; }
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
