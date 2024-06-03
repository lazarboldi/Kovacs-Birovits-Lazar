using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLib
{
    public class MentesManager
    {
        private static readonly string FilePath;
        private static readonly string StatistikakFilePath;

        static MentesManager()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
            var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
            FilePath = Path.Combine(classLibDirectory, "beallitasok.txt");
            StatistikakFilePath = Path.Combine(classLibDirectory, "statistikak.txt");

            // Ellenőrizzük, hogy létezik-e a fájl, ha nem, akkor létrehozzuk
            if (!File.Exists(FilePath))
            {
                using (StreamWriter sw = new StreamWriter(FilePath))
                {
                    // Ha még nem létezik a fájl, írjuk bele az alap beállításokat
                    sw.WriteLine("Nev;Jelszo;Pisztoly;Ak;Kotszer;Koktel;Joint;Hp;Uveg;Femcso;Kes;Melleny;Sisak;LetrehozasIdeje");
                }
            }

            // Ellenőrizzük, hogy létezik-e a statistikak.txt fájl, ha nem, akkor létrehozzuk
            if (!File.Exists(StatistikakFilePath))
            {
                using (StreamWriter sw = new StreamWriter(StatistikakFilePath))
                {
                    // Ha még nem létezik a fájl, létrehozzuk és beírjuk az alapértelmezett szövegeket
                    sw.WriteLine("Pisztoly;Ak;Kotszer;Koktel;Joint;Hp;Uveg;Femcso;Kes;Melleny;Sisak");
                }
            }
        }

        public static void MentesStatistikak(int eletero, int pisztolyLoszer, int akLoszer, bool vanMelleny, bool vanSisak)
        {
            // Ékezet nélküli változók
            string vanMellenyString = vanMelleny ? "van" : "nincs";
            string vanSisakString = vanSisak ? "van" : "nincs";

            // Az első sor összeállítása
            string header = "Pisztoly;Ak;Kotszer;Koktel;Joint;Hp;Uveg;Femcso;Kes;Melleny;Sisak";

            // Az adatok összeállítása és formázása
            string dataLine = string.Format("{0};{1};0;0;0;{2};0;0;0;{3};{4}", pisztolyLoszer, akLoszer, eletero, vanMellenyString, vanSisakString);

            // Ha még nem létezik a fájl, írjuk bele az első sort
            if (!File.Exists(StatistikakFilePath))
            {
                using (StreamWriter sw = new StreamWriter(StatistikakFilePath))
                {
                    sw.WriteLine(header);
                }
            }

            // Az adatokat hozzáadjuk a fájlhoz
            using (StreamWriter sw = File.AppendText(StatistikakFilePath))
            {
                sw.WriteLine(dataLine);
            }
        }

        public void MentesBeallitasok(int eletero, int pisztolyLoszer, int akLoszer, bool vanMelleny, bool vanSisak)
        {
            // Beállítások stringként
            string[] beallitasok = File.ReadAllLines(FilePath);

            if (beallitasok.Length > 1)
            {
                string[] adatok = beallitasok[1].Split(';');

                // Frissítjük az értékeket csak azoknál a változóknál, amelyek megváltoztak
                adatok[2] = pisztolyLoszer.ToString(); // Pisztoly lőszerek
                adatok[3] = akLoszer.ToString(); // AK lőszerek
                adatok[7] = eletero.ToString(); // Életerő
                adatok[11] = vanMelleny ? "Van" : "Nincs"; // Van mellény
                adatok[12] = vanSisak ? "Van" : "Nincs"; // Van sisak

                // Összerakjuk az új beállításokat és visszaírjuk a fájlba
                string updatedSettings = string.Join(";", adatok);
                beallitasok[1] = updatedSettings;
                File.WriteAllLines(FilePath, beallitasok);
            }
        }

        public void StatisztikakFrissitese(int eletero, int pisztolyLoszer, int akLoszer, int kotszer, int koktel, int joint, int uveg, int femcso, int kes, bool vanMelleny, bool vanSisak)
        {
            // Játékos statisztikák mentése
            using (StreamWriter sw = new StreamWriter($"{Mentes.valasztottFiokNev}_statisztikak.txt"))
            {
                sw.WriteLine($"Életerő: {eletero}");
                sw.WriteLine($"Pisztoly Lőszer: {pisztolyLoszer}");
                sw.WriteLine($"AK Lőszer: {akLoszer}");
                sw.WriteLine($"Kötözőszer: {kotszer}");
                sw.WriteLine($"Koktél: {koktel}");
                sw.WriteLine($"Joint: {joint}");
                sw.WriteLine($"Üveg: {uveg}");
                sw.WriteLine($"Fémcső: {femcso}");
                sw.WriteLine($"Kés: {kes}");
                sw.WriteLine($"Mellény: {(vanMelleny ? "Igen" : "Nem")}");
                sw.WriteLine($"Sisak: {(vanSisak ? "Igen" : "Nem")}");
            }
        }

        public void MentBeallitasok(int eletero, int pisztolyLoszer, int akLoszer, int kotszer, int koktel, int joint, int uveg, int femcso, int kes, bool vanMelleny, bool vanSisak)
        {
            // Játékos beállítások mentése
            using (StreamWriter sw = new StreamWriter($"{Mentes.valasztottFiokNev}_beallitasok.txt"))
            {
                sw.WriteLine($"Életerő: {eletero}");
                sw.WriteLine($"Pisztoly Lőszer: {pisztolyLoszer}");
                sw.WriteLine($"AK Lőszer: {akLoszer}");
                sw.WriteLine($"Kötözőszer: {kotszer}");
                sw.WriteLine($"Koktél: {koktel}");
                sw.WriteLine($"Joint: {joint}");
                sw.WriteLine($"Üveg: {uveg}");
                sw.WriteLine($"Fémcső: {femcso}");
                sw.WriteLine($"Kés: {kes}");
                sw.WriteLine($"Mellény: {(vanMelleny ? "Igen" : "Nem")}");
                sw.WriteLine($"Sisak: {(vanSisak ? "Igen" : "Nem")}");
            }
        }
    }
}
