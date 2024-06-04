using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public enum TargyTipus
    {
        Pisztoly,
        Ak,
        Kotszer,
        Koktel,
        Joint,
        Uveg,
        Femcso,
        Kes,
        Sisak,
        Melleny
    }

    public class Targy
    {
        public TargyTipus Tipus { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Targy(TargyTipus tipus, int x, int y)
        {
            Tipus = tipus;
            X = x;
            Y = y;
        }
    }
}
