﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;

namespace DimseLab_Aflevering.ViewModel
{
    class MyProjectsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Project> _myProjects = new ObservableCollection<Project>();
        private Project _selectedProject;



        public MyProjectsViewModel()
        {
            var helper = new Helper();
            var projects = helper.ReadProjectData();

            foreach (var project in projects)
            {
                if (project.ProjectMembers.Any(x => x.Email == helper.CurrentUser.Email))
                {
                    MyProjects.Add(project);
                }
                
            }

        }
        



        #region Get & Set Properties

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                // skal der ikke kaldes OnPropertyChange her? /Michael
            }
        }

        public ObservableCollection<Project> MyProjects
        {
            get { return _myProjects; }
            set { _myProjects = value; }
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
