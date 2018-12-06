using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimseLab_Aflevering.Model
{
    public class Doohickey
    {
        private String name;
        private int _iD;
        private ModelController _mc;

        public Doohickey()
        {
            // Bruges ifm lagring til disk
        }

        public Doohickey(ModelController mc)
        {
            _mc = mc;
        }
        
        public Doohickey(string inputName, int id)
        {
            Name = inputName;
            ID = id;
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
    }
}
