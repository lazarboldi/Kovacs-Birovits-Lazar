using System;
using ClassLib;

namespace ClassLib
{
    public class FoldonLevoTargy
    {
        public Targy Targy { get; private set; }
        public int PozicioX { get; private set; }
        public int PozicioY { get; private set; }

        public FoldonLevoTargy(Targy targy, int pozicioX, int pozicioY)
        {
            Targy = targy;
            PozicioX = pozicioX;
            PozicioY = pozicioY;
        }

        public void Felvesz(Fiok karakter)
        {
            if (Targy != null)
            {
                karakter.AddItem(Targy);
                Targy = null; // A tárgy eltűnik a földről miután felvették
            }
        }
    }
}
