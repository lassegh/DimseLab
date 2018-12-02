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
        private ModelController _mc;

        private RelayCommand _relayAddProject;

        public MyProjectsViewModel()
        {
            RelayAddProject = new RelayCommand(AddNewProject);

            // Opretter instans af ModelController
            _mc = new ModelController();

            LoadAndUpdateData();
        }

        private void LoadAndUpdateData()
        {
            // Load data
            _mc.LoadEverything();

            MyProjects.Clear(); //Tømmer listen, så der ikke opstår dubletter

            // Loops though every project and compares if the email fits the current users email. 
            foreach (Project project in _mc.ProjectList)
            {
                // Compares emails to test and show that it works, since we dont have a login system yet
                if (project.ProjectMembers.Any(x => x.Email == _mc.CurrentUser.Email))
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
                // Udregner id til næste projekt
                int id = 0;
                foreach (Project project in _mc.ProjectList)
                {
                    if (project.ID > id)
                    {
                        id = project.ID;
                    }

                    id++;
                }

                // Add Project
                Project newProject = new Project(InputProjectName, InputProjectDescribtion, InputProjectDate, id);
                newProject.ProjectMembers.Add(_mc.CurrentUser);
                _mc.ProjectList.Add(newProject);
                
                _mc.SaveEverything();
            }
            LoadAndUpdateData();
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
