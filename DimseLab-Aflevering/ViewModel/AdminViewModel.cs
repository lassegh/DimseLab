using System;
using System.Collections.Generic;
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
    

    class AdminViewModel : INotifyPropertyChanged
    {
        private ModelController _mC = ModelController.Instance;
        
        RelayCommand _saveButton;

        RelayCommand _loadButton;

        public AdminViewModel()
        {
            //RelayLoadButton = new RelayCommand(helper.LoadEverything);


            AdminButton = new RelayCommand(OpenAdmin);
        }

        private void OpenAdmin()
        {
            ModelController.Instance.SetAllInvisible();
            ModelController.Instance.AdminVisibility = true;
        }

        public RelayCommand SaveButton
        {
            get { return _saveButton; }
            set { _saveButton = value; }
        }

        public RelayCommand LoadButton
        {
            get { return _loadButton; }
            set { _loadButton = value; }
        }

        public RelayCommand AdminButton { get; set; }

        public ModelController MC
        {
            get { return _mC; }
            set { _mC = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
