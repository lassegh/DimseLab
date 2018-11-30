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
        private RelayCommand relaySaveButton;

        private RelayCommand relayLoadButton;


        public RelayCommand RelaySaveButton
        {
            get { return relaySaveButton; }
            set { relaySaveButton = value; }
        }

        public RelayCommand RelayLoadButton
        {
            get { return relayLoadButton; }
            set { relayLoadButton = value; }
        }



        public AdminViewModel()
        {
            var helper = new Helper();
            RelaySaveButton = new RelayCommand(helper.SaveEverything);
            //RelayLoadButton = new RelayCommand(helper.LoadEverything);
            
        }
        
    }
}
