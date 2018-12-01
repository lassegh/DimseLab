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
        private ObservableCollection<Project> _myProjects = new ObservableCollection<Project>();
        private DateTime _projectEndDate;

        private string _inputProjectName;
        private string _inputProjectDescribtion;
        private DateTime _inputProjectDate;

        private RelayCommand _relayAddProject;

        public MyProjectsViewModel()
        {
            RelayAddProject = new RelayCommand(AddNewProject);

            Helper helper = new Helper(); // New instance of helper class to access all its functionality
            List<Project> projects = helper.ReadProjectData(); // We load in the entire "Database / Dummydata" list from helper to this variable

            // Loops though every project and compares if the email fits the current users email. 
            foreach (var project in projects)
            {   
                // Compares emails to test and show that it works, since we dont have a login system yet
                if (project.ProjectMembers.Any(x => x.Email == helper.CurrentUser.Email))
                {
                    MyProjects.Add(project); //adds this new filtered list to the "MyProject" List
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
                // TODO De næste to er allerede lavet og bør ikke laves på ny (af hensyn til hukommelse)
                var helper = new Helper();
                List<Project> projects = helper.ReadProjectData();

                // Udregner id til næste projekt
                int id = 0;
                foreach (Project project1 in projects)
                {
                    if (project1.ID > id)
                    {
                        id = project1.ID;
                    }

                    id++;
                }

                // Add Project
                var project = new Project(InputProjectName, InputProjectDescribtion, InputProjectDate, id);

                project.ProjectMembers.Add(new User("Lars", "Truelsen", 32324567, "Lars@easj.dk".ToLower()));

                MyProjects.Add(project);
            }
        }

        private void UIElement_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


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
