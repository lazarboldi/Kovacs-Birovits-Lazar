﻿// SZFT5 LáBiKo
// Kovács Ádám Attila, Birovits Bence, Lázár Boldizsár

using System;

class Program
{
    static char[,] palya;
    static int palyaMeret = 7; // Növeltük a pálya méretét
    static int jatekosX, jatekosY;

    static void Main()
    {
        Inicializalas();
        KirajzolPalya();

        while (true)
        {
            Kezeles(Console.ReadKey(true).Key);
            KirajzolPalya();
        }
    }

    static void Inicializalas()
    {
        palya = new char[palyaMeret, palyaMeret];
        jatekosX = palyaMeret / 2;
        jatekosY = palyaMeret / 2;

        // Pálya feltöltése falakkal és üres területekkel
        for (int i = 0; i < palyaMeret; i++)
        {
            for (int j = 0; j < palyaMeret; j++)
            {
                // Falak a pálya szélén
                if (i == 0 || j == 0 || i == palyaMeret - 1 || j == palyaMeret - 1)
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
        int targyakSzama = 3;
        int elolenyekSzama = 2;

        for (int i = 0; i < targyakSzama; i++)
        {
            int x, y;
            do
            {
                x = rand.Next(1, palyaMeret - 1);
                y = rand.Next(1, palyaMeret - 1);
            } while (palya[x, y] != '.');

            palya[x, y] = 'T';
        }

        for (int i = 0; i < elolenyekSzama; i++)
        {
            int x, y;
            do
            {
                x = rand.Next(1, palyaMeret - 1);
                y = rand.Next(1, palyaMeret - 1);
            } while (palya[x, y] != '.');

            palya[x, y] = 'E';
        }
    }

    static void KirajzolPalya()
    {
        Console.Clear();
        for (int i = 0; i < palyaMeret; i++)
        {
            for (int j = 0; j < palyaMeret; j++)
            {
                Console.Write(palya[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void Kezeles(ConsoleKey key)
    {
        // Játékos mozgatása mindenhova
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (jatekosX > 1)
                {
                    Mozgas(-1, 0);
                }
                break;

            case ConsoleKey.DownArrow:
                if (jatekosX < palyaMeret - 2)
                {
                    Mozgas(1, 0);
                }
                break;

            case ConsoleKey.LeftArrow:
                if (jatekosY > 1)
                {
                    Mozgas(0, -1);
                }
                break;

            case ConsoleKey.RightArrow:
                if (jatekosY < palyaMeret - 2)
                {
                    Mozgas(0, 1);
                }
                break;
        }
    }

    static void Mozgas(int dx, int dy)
    {
        // Játékos mozgatása csak üres területekre vagy tárgyakra
        if (palya[jatekosX + dx, jatekosY + dy] != 'E' && palya[jatekosX + dx, jatekosY + dy] != 'X')
        {
            palya[jatekosX, jatekosY] = '.';
            jatekosX += dx;
            jatekosY += dy;
            palya[jatekosX, jatekosY] = 'P';
        }
    }
}
