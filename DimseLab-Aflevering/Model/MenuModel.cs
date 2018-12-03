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
    class MenuModel : INotifyPropertyChanged
    {
        
        // Objecter af views holdes i denne liste
        private List<ViewController> _viewVisibility;
        private static MenuModel _instance = null;

        private MenuModel()
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

            if (ModelController.Instance.LoggedIn)
            {
                //Show me browse
                ShowView("MyProjects");
            }
            else
            {
                //Show me login
                ShowView("MyProfile");

                // TODO Disable menu buttons
            }
        }

        /// <summary>
        /// Viser et bestemt view
        /// </summary>
        /// <param name="name">Navnet på det view, der skal vises</param>
        public void ShowView(string name)
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



        

        

        public List<ViewController> ViewVisibility
        {
            get { return _viewVisibility; }
            set
            {
                _viewVisibility = value;
                OnPropertyChanged();
            }
        }

        public static MenuModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MenuModel();
                }
                return _instance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
