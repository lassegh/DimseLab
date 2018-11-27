using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class ProjectViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Project> _projectList = new ObservableCollection<Project>();

        private string _projectInputName;
        private string _projectInputDescribtion;

        private DateTime _projectBeginDate;
        private DateTime _projectEndDate;

        private bool _projectIsFinished;

        private RelayCommand _relayAddProject;

        public ProjectViewModel()
        {
            RelayAddProject = new RelayCommand(AddNewProject);

            // Projects created as placeholders
            ProjectList.Add(new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.Today));
            ProjectList.Add(new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.Today));
            ProjectList.Add(new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.Today));
            ProjectList.Add(new Project("Robotic Arm", "We are developing a intelligent robotic arm", DateTime.Today));

        }

        // Adds a new project to "ProjectList"
        public void AddNewProject()
        {
            ProjectList.Add(new Project(ProjectInputName, ProjectInputDescribtion, ProjectEndDate));
        }



        #region Get & Set Proppertys

        public ObservableCollection<Project> ProjectList
        {
            get { return _projectList; }
            set
            {
                _projectList = value; 
                OnPropertyChanged();
            }
        }

        public string ProjectInputName
        {
            get { return _projectInputName; }
            set { _projectInputName = value; }
        }

        public string ProjectInputDescribtion
        {
            get { return _projectInputDescribtion; }
            set { _projectInputDescribtion = value; }
        }

        public DateTime ProjectBeginDate
        {
            get { return _projectBeginDate; }
            set { _projectBeginDate = value; }
        }

        public DateTime ProjectEndDate
        {
            get { return _projectEndDate; }
            set { _projectEndDate = value; }
        }

        public bool ProjectIsFinished
        {
            get { return _projectIsFinished; }
            set { _projectIsFinished = value; }
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
