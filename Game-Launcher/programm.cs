using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Launcher
{
    public class Programm
    {
        protected string name;
        protected string pfad;
        
        public string Name { get { return name; } }
        public string Pfad { get { return pfad; } }

        public Programm(string name, string pfad)
        {
            this.name = name;
            this.pfad = pfad;
        }
    }
}
