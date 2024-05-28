using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgeSystem
{
    public class Kihivas
    {
        public string Feladatmegnevezes { get; init; }
        public string Feladat { get; init; }
        public string Teljesites { get; set; }

        public Kihivas(string adatsor)
        {
            string[] adatelemek = adatsor.Split(";");
            Feladatmegnevezes = adatelemek[0];
            Feladat = adatelemek[1];
            Teljesites = adatelemek[2];
        }
    }
}
