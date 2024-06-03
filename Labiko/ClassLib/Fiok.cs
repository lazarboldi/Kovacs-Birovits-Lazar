using System;
using System.Collections.Generic;

namespace ClassLib
{
    public class Fiok
    {
        public string Nev { get; set; }
        public string Jelszo { get; set; }
        public int Eletero { get; set; }
        public int PisztolyLoszer { get; set; }
        public int AkLoszer { get; set; }
        public int Kotszer { get; set; }
        public int Koktel { get; set; }
        public int Joint { get; set; }
        public bool Melleny { get; set; }
        public bool Sisak { get; set; }
        public DateTime LetrehozasIdeje { get; set; }

        public Fiok()
        {
            Eletero = 100; // Default health
        }

        public void AddItem(Targy targy)
        {
            switch (targy.Tipus)
            {
                case TargyTipus.PisztolyLoves:
                    PisztolyLoszer += 15;
                    break;
                case TargyTipus.AkLovesz:
                    AkLoszer += 30;
                    break;
                case TargyTipus.Melleny:
                    if (!Melleny) Melleny = true;
                    break;
                case TargyTipus.Sisak:
                    if (!Sisak) Sisak = true;
                    break;
                case TargyTipus.Kotszer:
                    Kotszer++;
                    break;
                case TargyTipus.Koktel:
                    Koktel++;
                    break;
                case TargyTipus.Joint:
                    Joint++;
                    break;
                default:
                    throw new ArgumentException("Unknown item type");
            }
        }
    }
}
