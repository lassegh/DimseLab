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
        public RelayCommand BrowseButton { get; set; }
        public RelayCommand ManageProjectsButton { get; set; }
        public RelayCommand UserProfileButton { get; set; }

        public MenuViewModel()
        {
            
        }
    }
}
