using LandingPageMenuDemo;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

public class Mentes
{
    public string valasztottFiok { get; set; } // A választott fiók public változója

    private static readonly string FilePath;

    static Mentes()
    {
        // Dinamikusan meghatározzuk a projekt gyökérkönyvtárát
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
        var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
        FilePath = Path.Combine(classLibDirectory, "beallitasok.txt");


        // Ellenőrizzük, hogy a fájl létezik-e, ha nem, akkor hozzuk létre és írjunk bele egy fejlécet
        if (!File.Exists(FilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)); // Győződjünk meg arról, hogy a mappa létezik
            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                sw.WriteLine("Nev;Jelszo;Pisztoly;Ak;Kotszer;Koktel;Joint;LetrehozasIdeje");
            }
        }
    }

    public void Fomenu()
    {
        while (true)
        {
            Clear();
            string prompt = "Fiókkezelés\n";
            string[] opciok = { "Új fiók", "Fiókjaim", "Fiók törlése", "Összes fiók törlése", "Vissza a főmenübe" };
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
                    // Ha van már legalább egy fiók és nem választott ki másik fiókot,
                    // akkor a kilépéskor automatikusan a fiókok közül az első lesz kiválasztva
                    if (FiokokSzama() > 0 && string.IsNullOrEmpty(valasztottFiok))
                    {
                        string[] fiokok = File.ReadAllLines(FilePath);
                        valasztottFiok = fiokok[1]; // Az első fiók lesz a választott
                    }

                    if (!string.IsNullOrEmpty(valasztottFiok))
                    {
                        jatekmenet.MainFuttatasa();
                    }
                    else
                    {
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

    private int FiokokSzama()
    {
        if (File.Exists(FilePath))
        {
            string[] fiokok = File.ReadAllLines(FilePath);
            return fiokok.Length - 1; // Kivonjuk az 1-et, hogy ne számoljuk bele a fejlécet
        }
        return 0;
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

        // Addig kérjük be az új fiók nevét, amíg legalább egy karaktert nem ad meg a felhasználó
        while (string.IsNullOrEmpty(nev))
        {
            Clear();
            Write("Add meg az új fiók nevét (legalább 1 karakter): ");
            nev = ReadLine()?.Trim(); // A beolvasott szöveget trimmeljük, hogy eltávolítsuk a felesleges szóközöket
        }

        // Ellenőrizzük, hogy már létezik-e ilyen nevű fiók
        if (FiokNevLetezik(nev))
        {
            WriteLine("Ezzel a névvel már létezik fiók.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        // Addig kérjük be az új fiók jelszavát, amíg legalább egy karaktert nem ad meg a felhasználó
        while (string.IsNullOrEmpty(jelszo))
        {
            Clear();
            jelszo = GetPassword(); // Jelszó beolvasása
        }

        try
        {
            using (StreamWriter sw = new StreamWriter(FilePath, true))
            {
                string currentTime = DateTime.Now.ToString("yyyy. MM. dd. HH:mm:ss"); // Aktuális idő formázása
                sw.WriteLine($"{nev};{jelszo};0;0;0;0;0;{currentTime}"); // Fiók hozzáadása a létrehozási idővel
            }
            WriteLine("Az új fiók sikeresen létrehozva:");
            valasztottFiok = $"{nev};0;0;0;0;0"; // Az újonnan létrehozott fiók lesz a választott
            WriteLine(valasztottFiok.Split(';')[0]);
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

        if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
        {
            WriteLine("Nincsenek fiókok.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        string[] fiokok = File.ReadAllLines(FilePath);
        if (fiokok.Length <= 1)
        {
            WriteLine("Nincsenek fiókok.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        WriteLine("Fiókok:");
        for (int i = 1; i < fiokok.Length; i++) // Kezdjük az 1-es indexnél, hogy kihagyjuk a fejlécet
        {
            string[] fiok = fiokok[i].Split(';');
            string nev = fiok[0]; // Név megjelenítése
            string letrehozasIdeje = fiok[7]; // Létrehozás idejének megjelenítése
            WriteLine($"{i}. {nev} - Létrehozás ideje: {letrehozasIdeje}");
        }

        Write("Válassz egy fiókot a fenti listából: ");
        if (int.TryParse(ReadLine(), out int index) && index >= 1 && index < fiokok.Length)
        {
            string beirtJelszo = GetPassword(); // Jelszó beolvasása

            // Ellenőrizzük, hogy a beírt jelszó helyes-e
            if (beirtJelszo == fiokok[index].Split(';')[1])
            {
                valasztottFiok = fiokok[index]; // Beállítjuk a választott fiókot
                // Itt lehet további műveleteket végezni a kiválasztott fiókkal
                WriteLine($"A választott fiók: {valasztottFiok.Split(';')[0]}");
            }
            else
            {
                WriteLine("Hibás jelszó.");
            }
        }
        else
        {
            WriteLine("Érvénytelen szám.");
        }

        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        ReadKey();
    }

    private void FiokTorles()
    {
        Clear();

        if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
        {
            WriteLine("Nincsenek fiókok.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        string[] fiokok = File.ReadAllLines(FilePath);
        if (fiokok.Length <= 1)
        {
            WriteLine("Nincsenek fiókok.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        WriteLine("Fiókok:");
        for (int i = 1; i < fiokok.Length; i++) // Kezdjük az 1-es indexnél, hogy kihagyjuk a fejlécet
        {
            string[] fiok = fiokok[i].Split(';');
            string nev = fiok[0]; // Név megjelenítése
            string letrehozasIdeje = fiok[7]; // Létrehozás idejének megjelenítése
            WriteLine($"{i}. {nev} - Létrehozás ideje: {letrehozasIdeje}");
        }

        Write("Add meg a törlendő fiók számát: ");
        if (int.TryParse(ReadLine(), out int index) && index > 0 && index < fiokok.Length)
        {
            string beirtJelszo = GetPassword(); // Jelszó beolvasása

            // Ellenőrizzük, hogy a beírt jelszó helyes-e
            if (beirtJelszo == fiokok[index].Split(';')[1])
            {
                Write($"Biztosan törölni szeretnéd a(z) '{fiokok[index].Split(';')[0]}' fiókot? (i/n): ");
                if (ReadLine().ToLower() == "i")
                {
                    List<string> updatedFiokok = new List<string>(fiokok);
                    updatedFiokok.RemoveAt(index);
                    File.WriteAllLines(FilePath, updatedFiokok.ToArray());

                    WriteLine("Fiók törölve.");
                }
                else
                {
                    WriteLine("Fiók törlése megszakítva.");
                }
            }
            else
            {
                WriteLine("Hibás jelszó.");
            }
        }
        else
        {
            WriteLine("Érvénytelen szám.");
        }

        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        ReadKey();
    }


    private void OsszesFiokTorles()
    {
        Clear();

        if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
        {
            WriteLine("Nincsenek fiókok.");
            WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            ReadKey();
            return;
        }

        Write("Biztosan törölni szeretnéd az összes fiókot? (i/n): ");
        if (ReadLine().ToLower() == "i")
        {
            try
            {
                File.Delete(FilePath);
                using (StreamWriter sw = new StreamWriter(FilePath, false))
                {
                    sw.WriteLine("Nev;Jelszo;Pisztoly;Ak;Kotszer;Koktel;Joint;LetrehozasIdeje");
                }
                WriteLine("Az összes fiók sikeresen törölve.");
            }
            catch (Exception e)
            {
                WriteLine($"Hiba történt az összes fiók törlése közben: {e.Message}");
            }
        }
        else
        {
            WriteLine("Az összes fiók törlése megszakítva.");
        }

        WriteLine("Nyomj meg egy gombot a folytatáshoz.");
        ReadKey();
    }
}