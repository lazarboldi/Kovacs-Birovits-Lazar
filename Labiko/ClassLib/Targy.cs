using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public enum TargyTipus
    {
        PisztolyLoves,
        AkLovesz,
        Kotszer,
        Koktel,
        Joint,
        Uveg,
        Femcso,
        Kes,
        Melleny,
        Sisak
    }

    public class Targy
    {
        public TargyTipus Tipus { get; set; }
        public int Mennyiseg { get; set; }

        public Targy(TargyTipus tipus, int mennyiseg)
        {
            Tipus = tipus;
            Mennyiseg = mennyiseg;
        }
    }
}
