using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    internal class Palya
    {







        private int[] _szoba = new int[3];

        public char[,] Palyachar;


        public Palya(int id, int magassag, int szelesseg)
        {

            _szoba[0] = id;
            _szoba[1] = magassag;
            _szoba[2] = szelesseg;




        }

        public char[,] Inicializalas(out int jatekosX, out int jatekosY)
        {
            Palyachar = new char[_szoba[1], _szoba[2]];
            


            jatekosX = _szoba[1] / 2;
            jatekosY = _szoba[2] / 2;

            // Pálya feltöltése falakkal és üres területekkel // Lázár feladat
            for (int i = 0; i < _szoba[0]; i++)
            {
                for (int j = 0; j < _szoba[1]; j++)
                {
                    // Falak a pálya szélén
                    if (i == 0 || j == 0 || i == _szoba[0] - 1 || j == _szoba[1] - 1)
                    {
                        Palyachar[i, j] = 'X';
                    }else
                    {
                        Palyachar[i, j] = '.';
                    }




                }
            }

            // Játékos elhelyezése a pálya közepén
            Palyachar[jatekosX, jatekosY] = 'P';

            // Tárgyak és élőlények elhelyezése
            Random rand = new Random();
            int targyakSzama = 8;
            int elolenyekSzama = 5;

            for (int i = 0; i < targyakSzama; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(1, _szoba[1] - 1);
                    y = rand.Next(1, _szoba[2] - 1);
                } while (Palyachar[x, y] != '.');

                Palyachar[x, y] = 'T';
            }

            for (int i = 0; i < elolenyekSzama; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(1, _szoba[1] - 1);
                    y = rand.Next(1, _szoba[2] - 1);
                } while (Palyachar[x, y] != '.');

                Palyachar[x, y] = 'E';
            }

            return Palyachar;
        }





    }
}
