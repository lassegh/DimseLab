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
        private ModelController _mC = ModelController.Instance;
        private ObservableCollection<Project> _myProjects = ModelController.Instance.ProjectList;
        private DateTime _projectEndDate;

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
        }

        public void OnClickProjectInList(int i)
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.EditProjectVisibility = true;
            ModelController.Instance.SendSpecificProjectToIndexNul(i);
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
