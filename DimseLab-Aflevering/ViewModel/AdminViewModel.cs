using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Model;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    

    class AdminViewModel
    {
        RelayCommand _saveButton;

        RelayCommand _loadButton;





        public AdminViewModel()
        {
            var helper = new Helper();
            _saveButton = new RelayCommand(helper.SaveEverything);
            //RelayLoadButton = new RelayCommand(helper.LoadEverything);
            
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
    }
}
