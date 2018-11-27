using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace DimseLab_Aflevering.ViewModel
{
    class MenuViewModel
    {
        private bool _loggedIn;
        public RelayCommand BrowseButton { get; set; }
        public RelayCommand ManageProjectsButton { get; set; }
        public RelayCommand UserProfileButton { get; set; }

        public MenuViewModel()
        {
            //TODO get loginToken
            if (_loggedIn)
            {
                //Show me browse
            }
            else
            {
                //Show me login
            }
            
        }
    }
}
