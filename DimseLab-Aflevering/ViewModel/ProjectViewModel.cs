using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class ProjectViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Project> _projectList = new ObservableCollection<Project>();

        private string _inputName;
        private string _inputDescribtion;

        private DateTime _projectBeginDate;
        private DateTime _projectEndDate;

        private string _isFinished;

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

        public void AddNewProject()
        {
            ProjectList.Add(new Project(InputName, InputDescribtion, ProjectEndDate));
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

        public string InputName
        {
            get { return _inputName; }
            set { _inputName = value; }
        }

        public string InputDescribtion
        {
            get { return _inputDescribtion; }
            set { _inputDescribtion = value; }
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

        public string IsFinished
        {
            get { return _isFinished; }
            set { _isFinished = value; }
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
