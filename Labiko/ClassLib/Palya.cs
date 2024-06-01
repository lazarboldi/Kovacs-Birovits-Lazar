using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    internal class Palya
    {







        private int[] _szoba = new int[2];

        public Palya(int magassag, int szelesseg)
        {

            _szoba[0] = magassag;
            _szoba[1] = szelesseg;




        }

        public char[,] Inicializalas(out int jatekosX, out int jatekosY)
        {
            char[,] palya = new char[_szoba[0], _szoba[1]];
            

            jatekosX = _szoba[0] / 2;
            jatekosY = _szoba[1] / 2;

            // Pálya feltöltése falakkal és üres területekkel // Lázár feladat
            for (int i = 0; i < _szoba[0]; i++)
            {
                for (int j = 0; j < _szoba[1]; j++)
                {
                    // Falak a pálya szélén
                    if (i == 0 || j == 0 || i == _szoba[0] - 1 || j == _szoba[1] - 1)
                    {
                        palya[i, j] = 'X';
                    }
                    else
                    {
                        palya[i, j] = '.';
                    }
                }
            }

            // Játékos elhelyezése a pálya közepén
            palya[jatekosX, jatekosY] = 'P';

            // Tárgyak és élőlények elhelyezése
            Random rand = new Random();
            int targyakSzama = 8;
            int elolenyekSzama = 5;

            for (int i = 0; i < targyakSzama; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(1, _szoba[0] - 1);
                    y = rand.Next(1, _szoba[1] - 1);
                } while (palya[x, y] != '.');

                palya[x, y] = 'T';
            }

            for (int i = 0; i < elolenyekSzama; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(1, _szoba[0] - 1);
                    y = rand.Next(1, _szoba[1] - 1);
                } while (palya[x, y] != '.');

                palya[x, y] = 'E';
            }

            return palya;
        }





    }
}
