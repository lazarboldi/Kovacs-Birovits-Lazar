using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class Karakter
    {
        public string Nev { get; private set; }
        public int Eletero { get; private set; }
        public List<Targy> Inventarium { get; set; } // Hozzáférés módosítása

        public Karakter(string nev, int eletero)
        {
            Nev = nev;
            Eletero = eletero;
            Inventarium = new List<Targy>(); // Alapértelmezett inicializálás
        }

        public void Sebzes(int sebzes)
        {
            Eletero -= sebzes;
            if (Eletero < 0)
                Eletero = 0;
        }

        public void Gyogyitas(int gyogyitas)
        {
            Eletero += gyogyitas;
        }

        public void TargyHozzaad(Targy targy)
        {
            Inventarium.Add(targy);
        }

        public void TargyEltavolit(Targy targy)
        {
            Inventarium.Remove(targy);
        }
    }
}
