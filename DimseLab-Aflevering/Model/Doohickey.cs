using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimseLab_Aflevering.Model
{
    class Doohickey
    {
        private String name;

        
        public Doohickey(string inputName)
        {
            Name = inputName;
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
