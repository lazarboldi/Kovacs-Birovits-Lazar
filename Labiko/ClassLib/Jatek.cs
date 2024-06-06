﻿using LandingPageMenuDemo;
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

        public int Pisztoly;
        public int Ak;
        public int Kotszer;
        public int Koktel;
        public int Joint;
        public int Eletero;
        public int Uveg;
        public int Femcso;
        public int Kes;
        public int Melleny;
        public int Sisak;

        private ConsoleColor[,] szinek;
        static bool stopRequested = false;
        Ascii ascii = new Ascii();
        FiokKezeles fiokKezeles = new FiokKezeles();

        public void Foresz()
        {
            Mentes.valasztottFiokNev = Mentes.valasztottFiok.Split(";")[0];

            Pisztoly = int.Parse(Mentes.valasztottFiok.Split(";")[2]);
            Ak = int.Parse(Mentes.valasztottFiok.Split(";")[3]);
            Kotszer = int.Parse(Mentes.valasztottFiok.Split(";")[4]);
            Koktel = int.Parse(Mentes.valasztottFiok.Split(";")[5]);
            Joint = int.Parse(Mentes.valasztottFiok.Split(";")[6]);
            Eletero = int.Parse(Mentes.valasztottFiok.Split(";")[7]);
            Uveg = int.Parse(Mentes.valasztottFiok.Split(";")[8]);
            Femcso = int.Parse(Mentes.valasztottFiok.Split(";")[9]);
            Kes = int.Parse(Mentes.valasztottFiok.Split(";")[10]);
            Melleny = int.Parse(Mentes.valasztottFiok.Split(";")[11]);
            Sisak = int.Parse(Mentes.valasztottFiok.Split(";")[12]);

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
            palyaMeret = 15; // Módosítottuk a pálya méretét
            palya = new char[palyaMeret, palyaMeret];
            jatekosX = palyaMeret / 2;
            jatekosY = palyaMeret / 2;

            // Pálya feltöltése falakkal és üres területekkel
            for (int i = 0; i < palyaMeret; i++)
            {
                for (int j = 0; j < palyaMeret; j++)
                {
                    // A széleken falak
                    if (i == 0 || j == 0 || i == palyaMeret - 1 || j == palyaMeret - 1)
                    {
                        palya[i, j] = 'X';
                    }
                    // Belül üres terület
                    else
                    {
                        palya[i, j] = '.';
                    }
                }
            }

            // Játékos kezdőpozíciójának beállítása
            palya[jatekosX, jatekosY] = 'P';

            // Tárgyak és élőlények elhelyezése
            Random rand = new Random();
            int targyakSzama = 8;
            int elolenyekSzama = 5;

            // Elhelyezzük a tárgyakat
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

            // Elhelyezzük az élőlényeket
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
            WriteLine($"Escape a kilépéshez...    Fiókod: {Mentes.valasztottFiokNev}");
            WriteLine("Nyomd meg a TAB-ot a statisztikák megjelenítéséhez!");
            Console.WriteLine($"\nNyomd meg a H-t az életerő visszatöltéséhez.");
            WriteLine($"Játékos élete: {Eletero}\n");

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
        }

        private void Kezeles(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (jatekosX > 1)
                    {
                        Mozgas(-1, 0);
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (jatekosX < palyaMeret - 2)
                    {
                        Mozgas(1, 0);
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (jatekosY > 1)
                    {
                        Mozgas(0, -1);
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (jatekosY < palyaMeret - 2)
                    {
                        Mozgas(0, 1);
                    }
                    break;
                case ConsoleKey.Tab:
                    Clear();
                    MegjelenitStatisztika();
                    ReadKey();
                    break;
                case ConsoleKey.Escape:
                    Clear();
                    Mentes.valasztottFiok = null;
                    Console.WriteLine("Adatok sikeresen elmentve");
                    Thread.Sleep(2000);
                    Fomenu();
                    break;
                case ConsoleKey.H:
                    GyogyitasMenu();
                    break;
                default:
                    break;
            }

            // Ha a játékos új helye nincs ütközésben, állítsuk be 'P'-re
            if (palya[jatekosX, jatekosY] != 'X')
            {
                palya[jatekosX, jatekosY] = 'P';
            }
        }

        private void Fomenu()
        {
            stopRequested = true;
            Jatekmenet jatekmenet = new Jatekmenet();
            jatekmenet.MainFuttatasa();
        }


        private void TargyFelvetel(int x, int y)
        {
            Animacio animacio = new Animacio();
            // Ellenőrizzük, hogy van-e már mellény és sisak a játékosnál
            bool vanMelleny = Melleny > 0;
            bool vanSisak = Sisak > 0;

            // Ha még nincs mellény, adjunk hozzá egyet
            if (!vanMelleny)
            {
                Melleny = 1;
                fiokKezeles.NovelemMelleny(Mentes.valasztottFiokNev);
                animacio.Mellenykiiratasa();
            }
            else
            {
                // Ha még nincs sisak, adjunk hozzá egyet
                if (!vanSisak)
                {
                    Sisak = 1;
                    fiokKezeles.NovelemSisak(Mentes.valasztottFiokNev);
                    animacio.Sisakkiiratasa();
                }
                else
                {
                    // Ha már van mindkettő, válassz véletlenszerűen egy tárgyat a többi közül
                    if (vanMelleny && vanSisak)
                    {
                        Random rand = new Random();
                        TargyTipus tipus = (TargyTipus)rand.Next(2, 7);
                        switch (tipus)
                        {
                            case TargyTipus.Pisztoly:
                                Pisztoly += 15;
                                fiokKezeles.NovelemPisztoly(Mentes.valasztottFiokNev);
                                animacio.Pisztolykiiratasa();
                                break;
                            case TargyTipus.Ak:
                                Ak += 30;
                                fiokKezeles.NovelemAk(Mentes.valasztottFiokNev);
                                animacio.Akkiiratasa();
                                break;
                            case TargyTipus.Kotszer:
                                Kotszer += 1;
                                fiokKezeles.NovelemKotszer(Mentes.valasztottFiokNev);
                                animacio.Kotszerkiiratasa();
                                break;
                            case TargyTipus.Koktel:
                                Koktel += 1;
                                fiokKezeles.NovelemKoktel(Mentes.valasztottFiokNev);
                                animacio.Koktelkiiratasa();
                                break;
                            case TargyTipus.Joint:
                                Joint += 1;
                                fiokKezeles.NovelemJoint(Mentes.valasztottFiokNev);
                                animacio.Jointkiiratasa();
                                break;
                            case TargyTipus.Uveg:
                                Uveg += 1;
                                fiokKezeles.NovelemUveg(Mentes.valasztottFiokNev);
                                animacio.Uvegkiiratasa();
                                break;
                            case TargyTipus.Femcso:
                                Femcso += 1;
                                fiokKezeles.NovelemFemcso(Mentes.valasztottFiokNev);
                                animacio.Femcsokiiratasa();
                                break;
                            case TargyTipus.Kes:
                                Kes += 1;
                                fiokKezeles.NovelemKes(Mentes.valasztottFiokNev);
                                animacio.Keskiiratasa();
                                break;
                            default:
                                break;

                        }
                    }
                }
            }

            // Korlátozzuk a tárgyak számát a maximális értékre
            if (Pisztoly > 150) Pisztoly = 150;
            if (Ak > 300) Ak = 300;
            if (Kotszer > 10) Kotszer = 10;
            if (Koktel > 10) Koktel = 10;
            if (Joint > 10) Joint = 10;
            if (Uveg > 1) Uveg = 1;
            if (Femcso > 1) Femcso = 1;
            if (Kes > 1) Kes = 1;
            if (Melleny > 1) Melleny = 1;
            if (Sisak > 1) Sisak = 1;
        }

    public void Mozgas(int deltaX, int deltaY)
{
    // Új hely kiszámítása
    int ujX = jatekosX + deltaX;
    int ujY = jatekosY + deltaY;

    // Ellenőrzés, hogy az új helyen van-e tárgy
    if (palya[ujX, ujY] == 'T')
    {
        TargyFelvetel(ujX, ujY);
    }

    // Ellenőrzés, hogy az új helyen van-e ellenség
    if (palya[ujX, ujY] == 'E') // Ha ellenségbe ütközünk
    {
        bool jatekosGyoz = Harc(ujX, ujY);
        
        // Harc után, ha a játékos életben van, frissítjük a pályát
        if (Eletero > 0)
        {
            KirajzolPalya();
            WriteLine($"Játékos élete: {Eletero}\n");
        }
        if (jatekosGyoz)
        {
            // Ha a játékos győzött, mozgatjuk a játékost az ellenség helyére
            palya[jatekosX, jatekosY] = '.'; // Az aktuális hely törlése
            jatekosX = ujX;
            jatekosY = ujY;
            palya[jatekosX, jatekosY] = 'P';
        }
        return;
    }

    // Ha az új hely nincs ütközésben, mozgatjuk a játékost
    if (palya[ujX, ujY] != 'X')
    {
        // Az aktuális hely törlése
        palya[jatekosX, jatekosY] = '.';

        // Az új hely beállítása
        jatekosX = ujX;
        jatekosY = ujY;

        // Az új hely beállítása
        palya[jatekosX, jatekosY] = 'P';
    }
}

public bool Harc(int ujX, int ujY)
{
    EllensegTipus ellenseg = GetRandomEllensegTipus();
    bool jatekosGyoz = Csata(this, ellenseg);

    if (jatekosGyoz)
    {
        WriteLine("\nLegyőzted az ellenséget!");
        Thread.Sleep(1000);
        palya[ujX, ujY] = '.'; // Töröljük az ellenséget a pályáról
    }
    else
    {
        WriteLine("\nAz ellenség legyőzött téged!");
        Thread.Sleep(1000);
        if (Eletero <= 0)
        {
            Clear();
            Mentes.valasztottFiok = null;
            WriteLine("Meghaltál!");
            Thread.Sleep(2000);
            Fomenu();
        }
    }
    Thread.Sleep(1000);
    return jatekosGyoz;
}

private static Random rng = new Random();
public static bool Csata(Jatek jatekos, EllensegTipus ellenseg)
{
    FiokKezeles fiokKezeles = new FiokKezeles();
    Animacio animacio = new Animacio();
    int dobas = rng.Next(1, 11);
    int jatekosGyozesiKuszob = GetGyozesiKuszob(ellenseg);

    if (dobas <= jatekosGyozesiKuszob)
    {
         switch (ellenseg)
        {
            case EllensegTipus.GyengeTerrorista:
                animacio.Gyengekiiratasa();
                break;
            case EllensegTipus.KozepesTerrorista:
                animacio.Kotszerkiiratasa();
                break;
            case EllensegTipus.ErosTerrorista:
                animacio.Eroskiiratasa();
                break;
        }
        return true; // Játékos győz
    }
    else
    {
        int sebzes = GetEllensegSebzes(ellenseg);
        jatekos.Eletero -= sebzes;

        // Csökkentés a megfelelő nehézségi szint alapján
        switch (ellenseg)
        {
            case EllensegTipus.GyengeTerrorista:
                fiokKezeles.CsokkentemKonnyu(Mentes.valasztottFiokNev);
                animacio.Gyengekiiratasa();
                break;
            case EllensegTipus.KozepesTerrorista:
                fiokKezeles.CsokkentemKozepes(Mentes.valasztottFiokNev);
                animacio.Kotszerkiiratasa();
                break;
            case EllensegTipus.ErosTerrorista:
                fiokKezeles.CsokkentemNehez(Mentes.valasztottFiokNev);
                animacio.Eroskiiratasa();
                break;
        }
        return false; // Játékos veszít
    }
}

private static int GetGyozesiKuszob(EllensegTipus ellenseg)
{
    switch (ellenseg)
    {
        case EllensegTipus.GyengeTerrorista:
            return 7;
        case EllensegTipus.KozepesTerrorista:
            return 5;
        case EllensegTipus.ErosTerrorista:
            return 3;
        default:
            throw new ArgumentException("Érvénytelen ellenseg típus");
    }
}

private static int GetEllensegSebzes(EllensegTipus ellenseg)
{
    switch (ellenseg)
    {
        case EllensegTipus.GyengeTerrorista:
            return 10;
        case EllensegTipus.KozepesTerrorista:
            return 20;
        case EllensegTipus.ErosTerrorista:
            return 30;
        default:
            throw new ArgumentException("Érvénytelen ellenseg típus");
    }
}

private EllensegTipus GetRandomEllensegTipus()
{
    int dobas = rng.Next(1, 4); // 1-3 közötti random szám
    switch (dobas)
    {
        case 1:
            return EllensegTipus.GyengeTerrorista;
        case 2:
            return EllensegTipus.KozepesTerrorista;
        case 3:
            return EllensegTipus.ErosTerrorista;
        default:
            throw new ArgumentException("Érvénytelen random szám");
    }
}

public enum EllensegTipus
{
    GyengeTerrorista,
    KozepesTerrorista,
    ErosTerrorista
}

        private void MegjelenitStatisztika()
        {
            Clear();
            WriteLine($@"Név: {Mentes.valasztottFiokNev}
Életerő: {Eletero}
Pisztoly lőszer: {Pisztoly}
Ak lőszer: {Ak}
Kötszer: {Kotszer}
Koktél: {Koktel}
Joint: {Joint}
Üveg: {EroforrasAllapot(Uveg)}
Fémcső: {EroforrasAllapot(Femcso)}
Kés: {EroforrasAllapot(Kes)}
Mellény: {EroforrasAllapot(Melleny)}
Sisak: {EroforrasAllapot(Sisak)}");

            Console.WriteLine("\nNyomj meg egy gombot a folytatáshoz...");
            ReadKey(true);
        }


        public static string EroforrasAllapot(int ertek)
        {
            if (ertek == 0)
            {
                return "nincs";
            }
            else if (ertek == 1)
            {
                return "van";
            }
            else
            {
                return "érvénytelen";
            }
        }

        private void GyogyitasMenu()
        {
            Fiok fiok = new Fiok(Mentes.valasztottFiok);
            while (true)
            {
                Clear();
                WriteLine($"Játékos élete: {Eletero}\n");
                Ascii ascii = new Ascii();

                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
                var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
                var filePath = Path.Combine(classLibDirectory, "beallitasok.txt");
                Fiokok fiokok = new Fiokok(File.ReadAllLines(filePath, Encoding.UTF8).Skip(1));

                string prompt = "Válassz egy gyógyító tárgyat az alábbiak közül:";
                List<string> opciok = new List<string>();

                if (fiokok.SzamolomKotszer() > 0) opciok.Add("Kötés");
                if (fiokok.SzamolomKoktel() > 0) opciok.Add("Koktél");
                if (fiokok.SzamolomJoint() > 0) opciok.Add("Joint");

                if (opciok.Count == 0)
                {
                    WriteLine("Nincs elérhető élettöltő eszköz.");
                    WriteLine("Nyomd meg az ESCAPE-et a játékba való visszatéréshez.");
                    if (ReadKey(true).Key == ConsoleKey.Escape) return;
                }
                else
                {
                    opciok.Add("Vissza a játékba");

                    Menu menu = new Menu(prompt, opciok.ToArray());
                    int Megjelolt = menu.Futas();

                    if (Megjelolt == opciok.Count - 1) break; // Vissza a játékba

                    switch (opciok[Megjelolt])
                    {
                        case "Kötés":
                            Gyogyitas(TargyTipus.Kotszer);
                            break;
                        case "Koktél":
                            Gyogyitas(TargyTipus.Koktel);
                            break;
                        case "Joint":
                            Gyogyitas(TargyTipus.Joint);
                            break;
                    }
                }
            }
        }

        private void Gyogyitas(TargyTipus tipus)
        {
            FiokKezeles fiokKezeles = new FiokKezeles();
            // Gyógyító hatás alkalmazása
            switch (tipus)
            {
                case TargyTipus.Kotszer:
                    // Kötés használata
                    fiokKezeles.NovelemKotszerHp(Mentes.valasztottFiokNev);
                    fiokKezeles.CsokkentemKotszer(Mentes.valasztottFiokNev);
                    Eletero += 10;
                    Kotszer -= 1;
                    WriteLine("\nKötés használt.");
                    break;
                case TargyTipus.Koktel:
                    // Koktél használata
                    fiokKezeles.NovelemKoktelHp(Mentes.valasztottFiokNev);
                    fiokKezeles.CsokkentemKoktel(Mentes.valasztottFiokNev);
                    Eletero += 15;
                    Koktel -= 1;
                    WriteLine("\nKoktél használt.");
                    break;
                case TargyTipus.Joint:
                    // Joint használata
                    fiokKezeles.NovelemJointHp(Mentes.valasztottFiokNev);
                    fiokKezeles.CsokkentemJoint(Mentes.valasztottFiokNev);
                    Eletero += 20;
                    Joint -= 1;
                    WriteLine("\nJoint használt.");
                    break;
                default:
                    break;
            }
            WriteLine("Nyomd meg az ESCAPE-et a játékba való visszatéréshez, vagy válassz másik élettöltő eszközt.");

            // Frissítsük a képernyőn megjelenített életerő értékét
            KirajzolPalya();
            WriteLine($"Játékos élete: {Eletero}");
        }
    }
}
