using ClassLib;
using LandingPageMenuDemo;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

public class Mentes
{
    public static string valasztottFiok { get; set; } = null;
    public static string valasztottFiokNev { get; set; }

    private static readonly string FilePath;

    static Mentes()
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
        var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
        FilePath = Path.Combine(classLibDirectory, "beallitasok.txt");

        if (!File.Exists(FilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                sw.WriteLine("Nev;Jelszo;Pisztoly;Ak;Kotszer;Koktel;Joint;Hp;Uveg;Femcso;Kes;Melleny;Sisak;LetrehozasIdeje");
            }
        }

        // Eltávolítjuk az automatikus fiók kiválasztását
        valasztottFiok = null;
    }

    public void Fomenu()
    {
        bool kilep = false;

        while (!kilep)
        {
            Clear();
            string prompt = "Fiókkezelés\n";
            string[] opciok = { "Új fiók", "Fiókjaim", "Fiók törlése", "Összes fiók törlése", "Vissza a főmenübe", "Irány a játékba" };
            Menu menu = new Menu(prompt, opciok);
            int Megjelolt = menu.Futas();
            Jatekmenet jatekmenet = new Jatekmenet();

            switch (Megjelolt)
            {
                case 0:
                    UjFiok();
                    break;
                case 1:
                    Fiokjaim();
                    break;
                case 2:
                    FiokTorles();
                    break;
                case 3:
                    AdminJelszoBelekerese(); // Admin jelszó kérése az összes fiók törléséhez
                    break;
                case 4:
                    jatekmenet.MainFuttatasa();
                    break;
                case 5:
                    if (!string.IsNullOrEmpty(Mentes.valasztottFiok))
                    {
                        kilep = true; // Csak akkor lépünk ki a fiókkezelésből, ha van kiválasztott fiók
                    }
                    else
                    {
                        Clear();
                        WriteLine("Legalább egy fiók létrehozása szükséges.");
                        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
                        ReadKey();
                    }
                    break;
                default:
                    WriteLine("Érvénytelen opció, próbáld újra.");
                    break;
            }
        }
    }

    private void AdminJelszoBelekerese()
    {
        Clear();
        string adminJelszo = GetPassword();

        if (adminJelszo == "asdasdasd")
        {
            OsszesFiokTorles(); // Csak az admin jelszó megadása esetén történik az összes fiók törlése
        }
        else
        {
            WriteLine("Hibás admin jelszó.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
        }
    }

    private string GetPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        Write("Add meg a jelszót (legalább 1 karakter): ");
        do
        {
            key = ReadKey(true);

            // Ha a felhasználó lenyomta a Backspace-t, akkor töröljük az utolsó karaktert a jelszóból
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[..^1]; // Töröljük az utolsó karaktert
                CursorLeft--;
                Write(" ");
                CursorLeft--;
            }
        }
        while (key.Key != ConsoleKey.Enter);

        WriteLine(); // Új sorba lépünk a jelszó bevitele után

        return password;
    }

    private void UjFiok()
    {
        Clear();
        string nev = "";
        string jelszo = "";
        string currentTime = DateTime.Now.ToString("yyyy. MM. dd. HH:mm:ss");

        while (string.IsNullOrEmpty(nev))
        {
            Clear();
            Write("Add meg az új fiók nevét (legalább 1 karakter), vagy nyomj Enter-t a visszalépéshez: ");
            nev = ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nev))
            {
                return;
            }
        }

        if (FiokNevLetezik(nev))
        {
            WriteLine("Ezzel a névvel már létezik fiók.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        while (string.IsNullOrEmpty(jelszo))
        {
            Clear();
            jelszo = GetPassword();

            if (string.IsNullOrWhiteSpace(jelszo))
            {
                return;
            }
        }

        try
        {
            using (StreamWriter sw = new StreamWriter(FilePath, true))
            {
                sw.WriteLine($"{nev};{jelszo};0;0;0;0;0;200;0;0;0;1;1;{currentTime}");
            }
            WriteLine("Az új fiók sikeresen létrehozva:");
            Mentes.valasztottFiok = $"{nev};{jelszo};0;0;0;0;0;200;0;0;0;1;1;{currentTime}";
            WriteLine(Mentes.valasztottFiok.Split(';')[0]);
        }
        catch (Exception e)
        {
            WriteLine($"Hiba történt a fiók mentése közben: {e.Message}");
        }

        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        ReadKey();
    }


    private bool FiokNevLetezik(string nev)
    {
        if (File.Exists(FilePath))
        {
            string[] fiokok = File.ReadAllLines(FilePath);
            for (int i = 1; i < fiokok.Length; i++) // Kezdjük az 1-es indexnél, hogy kihagyjuk a fejlécet
            {
                string fiokNev = fiokok[i].Split(';')[0];
                if (fiokNev.Equals(nev, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void Fiokjaim()
    {
        Clear();
        string[] fiokok = File.ReadAllLines(FilePath);
        if (fiokok.Length <= 1)
        {
            WriteLine("Nincsenek fiókok.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        int valasztottIndex;
        while (true)
        {
            WriteLine("Fiókok:");
            for (int i = 1; i < fiokok.Length; i++)
            {
                // Megjelenítjük csak a nevet és a létrehozási időt
                string[] adatok = fiokok[i].Split(';');
                WriteLine($"{i}. Név: {adatok[0]}, Létrehozás ideje: {adatok[adatok.Length - 1]}");
            }
            Write("Add meg a választott fiók sorszámát (1-től kezdve), vagy nyomj Enter-t a visszalépéshez: ");
            string input = ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                // Ha csak Enter-t ütött, akkor reseteljük
                return;
            }
            else if (int.TryParse(input, out valasztottIndex))
            {
                if (valasztottIndex >= 1 && valasztottIndex < fiokok.Length)
                {
                    break; // Kilépünk a ciklusból ha a szám érvényes
                }
                else
                {
                    WriteLine("Nincs ilyen sorszámú fiók.");
                    Clear();
                }
            }
            else
            {
                WriteLine("Érvénytelen bemenet.");
                Clear();
            }
        }

        // Jelszó kérése a fiók kiválasztásához
        string jelszo = GetPassword();
        string[] valasztottFiokAdatok = fiokok[valasztottIndex].Split(';');

        // Ellenőrizzük, hogy a megadott jelszó egyezik-e a kiválasztott fiókhoz tartozó jelszóval
        if (jelszo == valasztottFiokAdatok[1])
        {
            Mentes.valasztottFiok = string.Join(";", valasztottFiokAdatok); // A választott fiók neve és adatai lesznek a valasztottFiokNev
            WriteLine($"A kiválasztott fiók: {Mentes.valasztottFiok.Split(";")[0]}");
        }
        else
        {
            WriteLine("Hibás jelszó.");
            ReadKey();
        }

        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        ReadKey();
    }


    private void FiokTorles()
    {
        Clear();
        string[] fiokok = File.ReadAllLines(FilePath);
        if (fiokok.Length <= 1)
        {
            WriteLine("Nincsenek fiókok a törléshez.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        int fiokSzam;
        while (true)
        {
            WriteLine("Fiókok:");
            for (int i = 1; i < fiokok.Length; i++)
            {
                // Megjelenítjük csak a nevet és a létrehozási időt
                string[] adatok = fiokok[i].Split(';');
                WriteLine($"{i}. Név: {adatok[0]}, Létrehozás ideje: {adatok[adatok.Length - 1]}");
            }
            Write("Add meg a törlendő fiók sorszámát (1-től kezdve), vagy nyomj Enter-t a visszalépéshez: ");
            string input = ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                // Ha csak Enter-t ütött, akkor reseteljük
                return;
            }
            else if (int.TryParse(input, out fiokSzam))
            {
                if (fiokSzam >= 1 && fiokSzam < fiokok.Length)
                {
                    break; // Kilépünk a ciklusból ha a szám érvényes
                }
                else
                {
                    WriteLine("Nincs ilyen sorszámú fiók.");
                    Clear();
                }
            }
            else
            {
                WriteLine("Érvénytelen bemenet.");
                Clear();
            }
        }

        // Jelszó kérése a fiók törléséhez
        string jelszo = GetPassword();
        string[] valasztottFiokAdatok = fiokok[fiokSzam].Split(';');

        // Ellenőrizzük, hogy a megadott jelszó egyezik-e a kiválasztott fiókhoz tartozó jelszóval
        if (jelszo == valasztottFiokAdatok[1])
        {
            WriteLine($"Biztosan törölni szeretnéd a(z) {valasztottFiokAdatok[0]} nevű fiókot? (i/n)");
            string valasz = ReadLine()?.ToLower();

            if (valasz == "i")
            {
                List<string> fiokLista = new List<string>(fiokok);
                fiokLista.RemoveAt(fiokSzam);

                try
                {
                    File.WriteAllLines(FilePath, fiokLista);
                    valasztottFiok = null;
                    WriteLine("A fiók sikeresen törölve.");
                }
                catch (Exception e)
                {
                    WriteLine($"Hiba történt a fiók törlése közben: {e.Message}");
                }
            }
            else if (valasz == "n")
            {
                WriteLine("A törlés megszakítva.");
            }
            else
            {
                WriteLine("Érvénytelen válasz.");
            }
        }
        else
        {
            WriteLine("Hibás jelszó.");
            ReadKey();
        }

        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        ReadKey();
    }

    private void OsszesFiokTorles()
    {
        Clear();
        try
        {
            WriteLine("Biztosan törölni szeretnéd az összes fiókot? (i/n)");
            string valasz = ReadLine()?.ToLower();

            if (valasz == "i")
            {
                File.WriteAllLines(FilePath, new[] { "Nev;Jelszo;Pisztoly;Ak;Kotszer;Koktel;Joint;Hp;Uveg;Femcso;Kes;Melleny;Sisak;LetrehozasIdeje" });
                valasztottFiok = null;
                WriteLine("Minden fiók sikeresen törölve.");
            }
            else if (valasz == "n")
            {
                WriteLine("Az összes fiók törlése megszakítva.");
            }
            else
            {
                WriteLine("Érvénytelen válasz.");
            }
        }
        catch (Exception e)
        {
            WriteLine($"Hiba történt az összes fiók törlése közben: {e.Message}");
        }

        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        ReadKey();
    }
}