using BadgeSystem;
using ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;

namespace LandingPageMenuDemo
{
    public class Jatekmenet
    {
        public void Kezd()
        {
            Title = "Escape from the club - A játék!";
            MainFuttatasa();
            WriteLine("\nNyomj bármilyen gombot a kilépéshez...");
            ReadKey(true);
        }
        public void MainFuttatasa()
        {
            Ascii ascii = new Ascii();
            string prompt = ascii.jatekcim;
            string[] opciok = { "Játék", "Játékról", "Rankod", "Kihívások", "Fiókkezelés", "kilépés" };
            Menu menu = new Menu(prompt, opciok);
            int Megjelolt = menu.Futas();

            switch (Megjelolt)
            {
                case 0:
                    JatekFuttatas();
                    break;
                case 1:
                    JatekrolMegjelenites();
                    break;
                case 2:
                    Rankkiiratasa();
                    break;
                case 3:
                    Kihivasokkiiratasa();
                    break;
                case 4:
                    Fiokkezeles();
                    break;
                case 5:
                    KilepesJatekbol();
                    break;
            }
        }

        private void KilepesJatekbol()
        {
            WriteLine("\nNyomj bármilyen gombot a kilépéshez...");
            ReadKey(true);
            Environment.Exit(0);
        }

        private void Fiokkezeles()
        {
            Mentes mentes = new Mentes();
            mentes.Fomenu();
        }

        private void JatekrolMegjelenites()
        {
            Clear();
            WriteLine("Történet: \n\nElmentél bulizni a barátaiddal, jól szórakoztatok amikor megtámadják a szórakozóhelyet, túszul ejtenek mindenkit, majd téged elvittek egy szobába. Egy terrorista őriz téged, azonban ő elment segíteni a társainak, mivel lázadtak a túszok. Téged megkötözve hagytak a szobában. Nagyjából 2 perced van eldönteni, hogy hogyan szeretnél kijutni a szobából. Amint ebből a szobából kijutottál eljutsz egy másik szobába, ahonnan szintén időre kell kiszabadulni mert a terroristák már a nyomodban vannak. A játék végén ha végig jól választottál, akkor meg kell küzdened a túszejtők vezetőjével, akit ha sikeresen legyőzöl kiszabadulsz. \n\n\nA játék folyamata: \n\nA játékos végigmegy a pályán, megküzd az őrökkel, terroristákkal. Egy szintnek akkor lesz vége, hogy ha a játékos kimegy az ajtón, ezt követően a következő szinten nehezebb küldetése, erősebb ellenfele lesz. Miután elér a játékos az utolsó szintre, akkor ki kell szabadítania a túszokat az utolsó szobából. El kell vinnie a túszokat a kijárathoz mindeközben meg kell küzdenie még egy pár őrrel.");
            WriteLine();
            ReadKey(true);
            MainFuttatasa();
        }

        private void Rankkiiratasa()
        {
            Clear();
            // Dinamikusan meghatározzuk a projekt gyökérkönyvtárát
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
            var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
            var filePath = Path.Combine(classLibDirectory, "feladatok.txt");

            Kihivasok kihivaslista = new(File.ReadAllLines(filePath, Encoding.UTF8).Skip(1));

            Ascii ascii = new Ascii();

            int teljesitettek = kihivaslista.Megszamlalas();

            Console.WriteLine($"Ennyi kihívást teljesítettél: {teljesitettek}");

            //9 18 27 36 45

            switch (teljesitettek)
            {
                case int n when (n >= 0 && n <= 9):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine("ezüst!\n");
                    WriteLine(ascii.rankod);
                    ResetColor();
                    break;
                case int n when (n > 9 && n <= 18):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine("arany!\n");
                    WriteLine(ascii.rankod);
                    ResetColor();
                    break;
                case int n when (n > 18 && n <= 27):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkGreen;
                    WriteLine("emeráld!\n");
                    WriteLine(ascii.rankod);
                    ResetColor();
                    break;
                case int n when (n > 27 && n <= 36):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkMagenta;
                    WriteLine("mester!\n");
                    WriteLine(ascii.rankod);
                    ResetColor();
                    break;
                case int n when (n > 36 && n <= 45):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine("hős!\n");
                    WriteLine(ascii.rankod);
                    ResetColor();
                    break;
                default:
                    // Ha egyik eset sem teljesül, ez fut le.
                    break;
            }
            ReadKey(true);
            MainFuttatasa();
        }

        private void Kihivasokkiiratasa()
        {
            Clear();
            WriteLine(@"#1 Vegyél fel egy pisztolyt a földről.
#2 Vegyél fel egy AK-t a földről.
#3 Vegyél fel egy jointot a földről.
#4 Vegyél fel egy koktélt a földről.
#5 Vegyél fel egy bandaget a földről.
#6 Győzz le 1 ellenséget.
#7 Győzz le 5 ellenséget.
#8 Győzz le 25 ellenséget.
#9 Győzz le 50 ellenséget.
#10 Győzz le 100 ellenséget.
#11 Győzz le 250 ellenséget.
#12 Győzz le 500 ellenséget.
#13 Veszíts el egy csatát.
#14 Legyél minimum 50 életerőn.
#15 Legyél minimum 30 életerőn.
#16 Legyél minimum 20 életerőn.
#17 Legyél minimum 10 életerőn.
#18 Nyerj egy csatát életerő vesztése nélkül.
#19 Érd el az EZÜST rankot.
#20 Érd el az ARANY rankot.
#21 Érd el az EMERALD rankot.
#22 Érd el a MESTER rankot.
#23 Érd el a HŐS rankot.
#24 Mozogj 50 területet.
#25 Mozogj 100 területet.");
            ReadKey(true);
            Clear();
            WriteLine(@"#26 Mozogj 500 területet.
#27 Mozogj 1000 területet.
#28 Győzz le 10 ellenséget életerő vesztése nélkül.
#29 Győzz le 25 ellenséget életerő vesztése nélkül.
#30 Győzz le 50 ellenséget életerő vesztése nélkül.
#31 Játsz a játékkal 5 percet.
#32 Játsz a játékkal 10 percet.
#33 Játsz a játékkal 15 percet.
#34 Játsz a játékkal 30 percet.
#35 Játsz a játékkal 1 órát.
#36 Játsz a játékkal 3 órát.
#37 Gyógyíts magadon 10 életerőt.
#38 Gyógyíts magadon 30 életerőt.
#39 Gyógyíts magadon 50 életerőt.
#40 Gyógyíts magadon 100 életerőt.
#41 Gyógyítsd magadat vissza 100 életerőre.
#42 Nyerj egymás után 2 csatát vesztés nélkül.
#43 Nyerj egymás után 5 csatát vesztés nélkül.
#44 Nyerj egymás után 10 csatát vesztés nélkül.
#45 Nyerj egymás után 15 csatát vesztés nélkül.");
            ReadKey(true);
            MainFuttatasa();
        }

        private void JatekFuttatas()
        {
            while (Mentes.valasztottFiok == null)
            {
                Clear();
                WriteLine("Fiókod: nincs kiválasztva");
                WriteLine("A játék futtatásához válassz vagy hozz létre egy fiókot.");
                ReadKey();

                Mentes mentes = new Mentes();
                mentes.Fomenu();
            }

            Jatek jatek = new Jatek();
            jatek.Foresz(); // Játék indítása
        }
    }
}