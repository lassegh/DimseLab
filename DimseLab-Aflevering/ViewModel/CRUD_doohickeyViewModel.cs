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

namespace DimseLab_Aflevering.ViewModel
{
    class CRUD_doohickeyViewModel : INotifyPropertyChanged
    {
        // Opretter reference til ModelController
        private ModelController _mC = ModelController.Instance;

        // Kopierer user listen
        private ObservableCollection<User> _userList = ModelController.Instance.UserList;

        // Kopierer projektListen fra ModelController
        private ObservableCollection<Project> _projectList = ModelController.Instance.ProjectList;

        // Kopierer doohickey listen
        private ObservableCollection<Doohickey> _doohickeyList = ModelController.Instance.DoohickeyList;


        public event PropertyChangedEventHandler PropertyChanged;


        public CRUD_doohickeyViewModel()
        {
            //intet her endnu
        }



        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
