using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class MenuViewModel : INotifyPropertyChanged
    {
        
        // Objecter af views holdes i denne liste
        private List<ViewController> _viewVisibility;

        public RelayCommand BrowseButton { get; set; }
        public RelayCommand ManageProjectsButton { get; set; }
        public RelayCommand UserProfileButton { get; set; }
        public RelayCommand AdminButton { get; set; }
        public RelayCommand<int> SelectedProjectCommand{ get; set; }

        private ModelController _mC = new ModelController();

        public List<ViewController> ViewVisibility
        {
            get { return _viewVisibility; }
            set
            {
                _viewVisibility = value;
                OnPropertyChanged();
            }
        }

        public ModelController MC
        {
            get { return _mC; }
            set
            {
                _mC = value; 
                OnPropertyChanged();
            }
        }


        public MenuViewModel()
        {
            // Lægger objecter for views i liste
            _viewVisibility = new List<ViewController>()
            {
                new ViewController("Browse"),
                new ViewController("MyProjects"),
                new ViewController("MyProfile"),
                new ViewController("Admin"),
                new ViewController("EditProject")
            };

            if (MC.LoggedIn)
            {
                //Show me browse
                OpenMyProjects();
            }
            else
            {
                //Show me login
                OpenUserProfile();

                // TODO Disable menu buttons
            }
            
            BrowseButton = new RelayCommand(OpenBrowse);
            ManageProjectsButton = new RelayCommand(OpenMyProjects);
            UserProfileButton = new RelayCommand(OpenUserProfile);
            AdminButton = new RelayCommand(OpenAdmin);
            SelectedProjectCommand = new RelayCommand<int>(OnClickProjectInList );
        }

        /// <summary>
        /// Viser et bestemt view
        /// </summary>
        /// <param name="name">Navnet på det view, der skal vises</param>
        private void ShowView(string name)
        {
            foreach (var view in ViewVisibility)
            {
                if (view.Name.Equals(name))
                {
                    view.Visible = true;
                }
                else view.Visible = false;
            }
        }

        private void OpenBrowse()
        {
            ShowView("Browse");
        }

        private void OpenMyProjects()
        {
            ShowView("MyProjects");
        }

        private void OpenUserProfile()
        {
            ShowView("MyProfile");
        }

        private void OpenAdmin()
        {
            ShowView("Admin");
        }

        public void OnClickProjectInList(int i)
        {
            ShowView("EditProject");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
