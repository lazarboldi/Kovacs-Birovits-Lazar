using LandingPageMenuDemo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ClassLib
{
    public class Jatek
    {
        public char[,] palya;
        /*static int palyaMeret = 20*/ // Növeltük a pálya méretét
        public int JatekosX;
        public int JatekosY;
        public int Magassag;
        public int Szelesseg;
        public int ID;



        public int[,] szobak =
        {
           {1, 5, 7 },
           {2, 5, 7 },


           {3, 5, 7 },
           {4, 5, 7 }
        };

        internal Palya? palyaObj;

        public void Foresz()
        {
            
            ID = szobak[0, 0];
            Magassag = szobak[0, 1];
            Szelesseg= szobak[0, 2];



            palyaObj = new(ID, Magassag, Szelesseg);

            palya = palyaObj.Inicializalas(out int jatekosX, out int jatekosY);
            JatekosX = jatekosX;
            JatekosY = jatekosY;



            // Birovits feladat
            KirajzolPalya();    // Birovits feladat

            while (true)
            {
                Kezeles(Console.ReadKey(true).Key);
                KirajzolPalya();    // Birovits feladat
            }
        }
        //public void Inicializalas()
        //{
        //    palya = new char[palyaMeret, palyaMeret];
        //    JatekosX = palyaMeret / 2;
        //    JatekosY = palyaMeret / 2;

        //    // Pálya feltöltése falakkal és üres területekkel // Lázár feladat
        //    for (int i = 0; i < palyaMeret; i++)
        //    {
        //        for (int j = 0; j < palyaMeret; j++)
        //        {
        //            // Falak a pálya szélén
        //            if (i == 0 || j == 0 || i == palyaMeret - 1 || j == palyaMeret - 1)
        //            {
        //                palya[i, j] = 'X';
        //            }
        //            else
        //            {
        //                palya[i, j] = '.';
        //            }
        //        }
        //    }

        //    // Játékos elhelyezése a pálya közepén
        //    palya[jatekosX, jatekosY] = 'P';

        //    // Tárgyak és élőlények elhelyezése
        //    Random rand = new Random();
        //    int targyakSzama = 8;
        //    int elolenyekSzama = 5;
            
        //    for (int i = 0; i < targyakSzama; i++)
        //    {
        //        int x, y;
        //        do
        //        {
        //            x = rand.Next(1, palyaMeret - 1);
        //            y = rand.Next(1, palyaMeret - 1);
        //        } while (palya[x, y] != '.');

        //        palya[x, y] = 'T';
        //    }

        //    for (int i = 0; i < elolenyekSzama; i++)
        //    {
        //        int x, y;
        //        do
        //        {
        //            x = rand.Next(1, palyaMeret - 1);
        //            y = rand.Next(1, palyaMeret - 1);
        //        } while (palya[x, y] != '.');

        //        palya[x, y] = 'E';
        //    }
        //}

        public void KirajzolPalya()
        {
            Console.Clear();
            WriteLine($"Escape a kilépéshez...       Fiókod: {Mentes.valasztottFiokNev}\n");
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    Console.Write(palya[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Kezeles(ConsoleKey key)
        {
            // Játékos mozgatása mindenhova    Lázár feladat
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (JatekosX> 1)
                    {
                        Mozgas(-1, 0);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (JatekosX < palya.GetLength(0) - 2)
                    {
                        Mozgas(1, 0);
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (JatekosY > 1)
                    {
                        Mozgas(0, -1);
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (JatekosY < palya.GetLength(1) - 2)
                    {
                        Mozgas(0, 1);
                    }
                    break;
                case ConsoleKey.Escape:
                    ResetColor();
                    Jatekmenet jatekmenet = new Jatekmenet();
                    jatekmenet.MainFuttatasa();
                    break;
            }
        }

        public void Mozgas(int dx, int dy)
        {
            // Játékos mozgatása csak üres területekre vagy tárgyakra Lázár feladat
            if (palya[JatekosX+ dx, JatekosY+ dy] != 'E' && palya[JatekosX + dx, JatekosY + dy] != 'X')
            {
                palya[JatekosX, JatekosY] = '.';
                JatekosX += dx;
                JatekosY += dy;
                palya[JatekosX, JatekosY] = 'P';
            }else if (palya[JatekosX, JatekosY + dy] == 'A' && ID == 1)
            {
                ID = szobak[1, 0];
                Magassag = szobak[1, 1];
                Szelesseg= szobak[1, 2];

                palyaObj = new(ID, Magassag, Szelesseg);
                palya = palyaObj.Inicializalas(out int jx, out int jy);
                JatekosX = jx;
                JatekosY = jy;



            }
            else if (palya[JatekosX, JatekosY - dy] == 'A' && ID == 2)
            {
                ID = szobak[0, 0];
                Magassag = szobak[0, 1];
                Szelesseg = szobak[0, 2];

                palyaObj = new(ID, Magassag, Szelesseg);
                palya = palyaObj.Inicializalas(out int jx, out int jy);
                JatekosX = jx;
                JatekosY = jy;



            }
            else if (palya[JatekosX, JatekosY + dy] == 'A' && ID == 2)
            {
                ID = szobak[2, 0];
                Magassag = szobak[2, 1];
                Szelesseg = szobak[2, 2];

                palyaObj = new(ID, Magassag, Szelesseg);
                palya = palyaObj.Inicializalas(out int jx, out int jy);
                JatekosX = jx;
                JatekosY = jy;



            }
            else if (palya[JatekosX, JatekosY - dy] == 'A' && ID == 3)
            {
                ID = szobak[1, 0];
                Magassag = szobak[1, 1];
                Szelesseg = szobak[1, 2];

                palyaObj = new(ID, Magassag, Szelesseg);
                palya = palyaObj.Inicializalas(out int jx, out int jy);
                JatekosX = jx;
                JatekosY = jy;



            }
            else if (palya[JatekosX - dx, JatekosY] == 'A' && ID == 3)
            {
                ID = szobak[3, 0];
                Magassag = szobak[3, 1];
                Szelesseg = szobak[3, 2];

                palyaObj = new(ID, Magassag, Szelesseg);
                palya = palyaObj.Inicializalas(out int jx, out int jy);
                JatekosX = jx;
                JatekosY = jy;



            }
            else if (palya[JatekosX, JatekosY + dy] == 'A' && ID == 4)
            {
                ID = szobak[2, 0];
                Magassag = szobak[2, 1];
                Szelesseg = szobak[2, 2];

                palyaObj = new(ID, Magassag, Szelesseg);
                palya = palyaObj.Inicializalas(out int jx, out int jy);
                JatekosX = jx;
                JatekosY = jy;



            }

        }
    }
}
