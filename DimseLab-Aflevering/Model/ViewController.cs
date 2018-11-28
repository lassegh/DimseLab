using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab_Aflevering.Annotations;

namespace DimseLab_Aflevering.Model
{
    // Denne klasse er en hjælperklasse til MenuViewModel. Den er med til at gøre viewmodel mere overskuelig, da der ellers skulle kaldes en lang række af ændringer på bools, hver gang der skulle skiftes view.
    class ViewController : INotifyPropertyChanged
    {
        private string _name;
        private bool _visible;

        public ViewController(string name)
        {
            Name = name;
            Visible = false;
        }

        // Navn på view
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Visuel eller ej
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged();
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
