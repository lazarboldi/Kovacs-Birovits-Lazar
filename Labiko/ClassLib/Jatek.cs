using LandingPageMenuDemo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Console;

namespace ClassLib
{
    public class Jatek
    {
        public char[,] palya;
        static int palyaMeret = 20; // Növeltük a pálya méretét
        public int jatekosX, jatekosY;
        public int Eletero { get; private set; } = 100;
        public int PisztolyLoszer { get; private set; }
        public int AkLoszer { get; private set; }
        public int Kotszer { get; private set; }
        public int Koktel { get; private set; }
        public int Joint { get; private set; }
        public int Uveg { get; private set; }
        public int Femcso { get; private set; }
        public int Kes { get; private set; }
        private ConsoleColor[,] szinek;
        public List<Targy> FelszedhetoTargyak { get; private set; }

        public Jatek()
        {
            FelszedhetoTargyak = new List<Targy>();
        }

        private bool vanMelleny;
        private bool vanSisak;
        static bool stopRequested = false;
        Ascii ascii = new Ascii();

        public void Foresz()
        {
            Inicializalas();
            KirajzolPalya();

            while (true)
            {
                Kezeles(ReadKey(true).Key);
                KirajzolPalya();
            }
        }

        public void Inicializalas()
        {
            // Pálya mérete és játékos kezdőpozíció beállítása
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
            int targyakSzama = 8;
            int elolenyekSzama = 5;

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

        public void KirajzolPalya()
        {
            Console.Clear();
            WriteLine($"Escape a kilépéshez...    Fiókod: {Mentes.valasztottFiokNev}\n");
            WriteLine("Nyomd meg a TAB-ot a statisztikák megjelenítéséhez!\n");

            // Inicializáljuk a színek tömböt
            szinek = new ConsoleColor[palyaMeret, palyaMeret];

            for (int i = 0; i < palyaMeret; i++)
            {
                for (int j = 0; j < palyaMeret; j++)
                {
                    // Beállítjuk az alapértelmezett színt (kék)
                    szinek[i, j] = ConsoleColor.Blue;

                    // Az ellenségek pirosak lesznek
                    if (palya[i, j] == 'E')
                    {
                        szinek[i, j] = ConsoleColor.Red;
                    }
                    // A játékos zöld lesz
                    else if (palya[i, j] == 'P')
                    {
                        szinek[i, j] = ConsoleColor.Green;
                    }
                    else if (palya[i, j] == 'X')
                    {
                        szinek[i, j] = ConsoleColor.White;
                    }
                    else if (palya[i, j] == '.')
                    {
                        szinek[i, j] = ConsoleColor.White;
                    }

                    // Kiírjuk a karaktert a megfelelő színnel
                    Console.ForegroundColor = szinek[i, j];
                    Console.Write(palya[i, j] + " ");
                }
                Console.WriteLine();
            }
            ResetColor();
            Console.WriteLine($"\nNyomd meg a H-t az életerő visszatöltéséhez.");
        }

        public void Kezeles(ConsoleKey key)
        {
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

                case ConsoleKey.Escape:
                    MenteseBeallitasok();
                    Jatekmenet jatekmenet = new Jatekmenet();
                    jatekmenet.MainFuttatasa();
                    break;

                case ConsoleKey.Tab:
                    Console.Clear();
                    MegjelenitStatisztika();
                    Console.WriteLine("\nNyomj meg egy billentyűt a folytatáshoz...");
                    ReadKey(true);
                    break;

                case ConsoleKey.H:
                    if (Kotszer > 0 || Koktel > 0 || Joint > 0)
                    {
                        Heal();
                        Clear();
                        Console.WriteLine("Élet feltöltve!");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Clear();
                        Console.WriteLine("Nincs semmi élettöltőd.");
                        Thread.Sleep(2000);
                    }
                    break;

                default:
                    break;
            }
        }

        public void Mozgas(int dx, int dy)
        {
            int ujX = jatekosX + dx;
            int ujY = jatekosY + dy;

            if (palya[ujX, ujY] == '.')
            {
                palya[jatekosX, jatekosY] = '.';
                jatekosX = ujX;
                jatekosY = ujY;
                palya[jatekosX, jatekosY] = 'P';
            }
            else if (palya[ujX, ujY] == 'T')
            {
                TargyFelvetel(ujX, ujY);
                palya[jatekosX, jatekosY] = '.';
                jatekosX = ujX;
                jatekosY = ujY;
                palya[jatekosX, jatekosY] = 'P';
            }
            else if (palya[ujX, ujY] == 'E')
            {
                Eletero -= 10; // Az ellenség megtámadja a játékost, és 10 életerőt veszít
                if (Eletero <= 0)
                {
                    Console.Clear();
                    WriteLine("Vége a játéknak! Elvesztetted az összes életerődet.");
                    Environment.Exit(0);
                }
            }
        }

        public void TargyFelvetel(int x, int y)
        {
            Random rand = new Random();
            Animacio animacio = new Animacio();
            TargyTipus tipus = (TargyTipus)rand.Next(9); // Random tárgytípus kiválasztása (0-8)

            switch (tipus)
            {
                case TargyTipus.PisztolyLoves:
                    PisztolyLoszer += 15;
                    FrissitAdatok(); // Call to update statistics
                    animacio.Pisztolykiiratasa();
                    break;
                case TargyTipus.AkLovesz:
                    AkLoszer += 30; // AkLovesz esetén 30-at ad hozzá
                    FrissitAdatok(); // Call to update statistics
                    animacio.Akkiiratasa();
                    break;
                case TargyTipus.Kotszer:
                    Kotszer += 1;
                    FelszedhetoTargyak.Add(new Targy(TargyTipus.Kotszer, 1));
                    FrissitAdatok();
                    animacio.Kotszerkiiratasa();
                    break;
                case TargyTipus.Koktel:
                    Koktel += 1;
                    FelszedhetoTargyak.Add(new Targy(TargyTipus.Koktel, 1));
                    FrissitAdatok();
                    animacio.Koktelkiiratasa();
                    break;
                case TargyTipus.Joint:
                    Joint += 1;
                    FelszedhetoTargyak.Add(new Targy(TargyTipus.Joint, 1));
                    FrissitAdatok();
                    animacio.Jointkiiratasa();
                    break;
                case TargyTipus.Uveg:
                    if (Uveg == 0) // Ellenőrizzük, hogy van-e már Uveg tárgy
                    {
                        Uveg += 1;
                        FelszedhetoTargyak.Add(new Targy(TargyTipus.Uveg, 1));
                        FrissitAdatok(); // Call to update statistics
                        animacio.Uvegkiiratasa();
                    }
                    break;
                case TargyTipus.Femcso:
                    if (Femcso == 0) // Ellenőrizzük, hogy van-e már Femcso tárgy
                    {
                        Femcso += 1;
                        FelszedhetoTargyak.Add(new Targy(TargyTipus.Femcso, 1));
                        FrissitAdatok(); // Call to update statistics
                        animacio.Femcsokiiratasa();
                    }
                    break;
                case TargyTipus.Kes:
                    if (Kes == 0) // Ellenőrizzük, hogy van-e már Kes tárgy
                    {
                        Kes += 1;
                        FelszedhetoTargyak.Add(new Targy(TargyTipus.Kes, 1));
                        FrissitAdatok(); // Call to update statistics
                        animacio.Keskiiratasa();
                    }
                    break;

                case TargyTipus.Melleny:
                    if (!vanMelleny)
                    {
                        vanMelleny = true;
                        Eletero = Math.Min(Eletero + 50, 200);
                        FrissitAdatok(); // Call to update statistics
                        animacio.Mellenykiiratasa();
                    }
                    break;
                case TargyTipus.Sisak:
                    if (!vanSisak)
                    {
                        vanSisak = true;
                        Eletero = Math.Min(Eletero + 50, 200);
                        FrissitAdatok(); // Call to update statistics
                        animacio.Sisakkiiratasa();
                    }
                    break;
                default:
                    break;
            }

            // Check for armor conditions
            if (Eletero >= 150 && vanSisak)
            {
                vanSisak = false;
            }

            if (Eletero >= 100 && vanMelleny)
            {
                vanMelleny = false;
            }
        }

        public void HasznalKotszer()
        {
            if (Kotszer > 0 && Eletero < 100)
            {
                Eletero = Math.Min(Eletero + 10, 100);
                Kotszer--;
            }
        }

        public void HasznalKoktel()
        {
            if (Koktel > 0 && Eletero < 100)
            {
                Eletero = Math.Min(Eletero + 15, 100);
                Koktel--;
            }
        }

        public void HasznalJoint()
        {
            if (Joint > 0 && Eletero < 100)
            {
                Eletero = Math.Min(Eletero + 20, 100);
                Joint--;
            }
        }

        public void Heal()
        {
            if (Kotszer > 0)
            {
                Eletero = Math.Min(Eletero + 10, 200);
                Kotszer--;
                FrissitAdatok();
            }
            else if (Koktel > 0)
            {
                Eletero = Math.Min(Eletero + 15, 200);
                Koktel--;
                FrissitAdatok();
            }
            else if (Joint > 0)
            {
                Eletero = Math.Min(Eletero + 20, 200);
                Joint--;
                FrissitAdatok();
            }
        }

        public void FrissitAdatok()
        {
            MentesManager mentes = new MentesManager();
            mentes.StatisztikakFrissitese(Eletero, PisztolyLoszer, AkLoszer, Kotszer, Koktel, Joint, Uveg, Femcso, Kes, vanMelleny, vanSisak);
        }

        public void MenteseBeallitasok()
        {
            MentesManager mentes = new MentesManager();
            mentes.MentBeallitasok(Eletero, PisztolyLoszer, AkLoszer, Kotszer, Koktel, Joint, Uveg, Femcso, Kes, vanMelleny, vanSisak);
            Clear();
            Console.WriteLine("Beállítások elmentve.");
            Thread.Sleep(2000);
        }

        public void MegjelenitStatisztika()
        {
            Console.Clear();
            Console.WriteLine("Statisztikák:");
            Console.WriteLine($"Életerő: {Eletero}");
            Console.WriteLine($"Pisztoly Lőszer: {PisztolyLoszer}");
            Console.WriteLine($"AK Lőszer: {AkLoszer}");
            Console.WriteLine($"Kötözőszer: {Kotszer}");
            Console.WriteLine($"Koktél: {Koktel}");
            Console.WriteLine($"Joint: {Joint}");
            Console.WriteLine($"Üveg: {Uveg}");
            Console.WriteLine($"Fémcső: {Femcso}");
            Console.WriteLine($"Kés: {Kes}");
            Console.WriteLine($"Mellény: {(vanMelleny ? "Igen" : "Nem")}");
            Console.WriteLine($"Sisak: {(vanSisak ? "Igen" : "Nem")}");
        }
    }
}
