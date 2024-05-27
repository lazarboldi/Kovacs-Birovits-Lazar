﻿using LandingPageMenuDemo;
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
        static int palyaMeret = 20; // Növeltük a pálya méretét
        public int jatekosX, jatekosY;

        public int[,] szobak =
        {
            {1, 2 },
            {3, 4 },
            { 5 ,6 },
            { 7, 8 }

        };

        


        

        


        public void Foresz(out int jX, out int jY)
        {



            Palya palya_ = new Palya(szobak[0, 1], szobak[0, 0]);

            char[,] szoba = palya_.Inicializalas(out int jatekosX, out int jatekosY);

            jX = jatekosX;
            jY = jatekosY;
                
            KirajzolPalya(szoba);    // Birovits feladat

            while (true)
            {
                Kezeles(Console.ReadKey(true).Key, jX, jY);
                KirajzolPalya(szoba);    // Birovits feladat
            }
        }
        //public void Inicializalas()
        //{
        //    palya = new char[palyaMeret, palyaMeret];
        //    jatekosX = palyaMeret / 2;
        //    jatekosY = palyaMeret / 2;

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

        public void KirajzolPalya(char[,] szoba)
        {
            Console.Clear();
            WriteLine("Escape a kilépéshez...\n");
            for (int i = 0; i < szoba.GetLength(0); i++)
            {
                for (int j = 0; j < szoba.GetLength(1); j++)
                {
                    Console.Write(szoba[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Kezeles(ConsoleKey key, int jX, int jY)
        {
            // Játékos mozgatása mindenhova    Lázár feladat
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if ( jX> 1)
                    {
                        Mozgas(-1, 0);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (jX< palyaMeret - 2)
                    {
                        Mozgas(1, 0);
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (jY> 1)
                    {
                        Mozgas(0, -1);
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (jY< palyaMeret - 2)
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
            if (palya[jatekosX + dx, jatekosY + dy] != 'E' && palya[jatekosX + dx, jatekosY + dy] != 'X')
            {
                palya[jatekosX, jatekosY] = '.';
                jatekosX += dx;
                jatekosY += dy;
                palya[jatekosX, jatekosY] = 'P';
            }else if (true)
            {
                
            }
        }
    }
}
