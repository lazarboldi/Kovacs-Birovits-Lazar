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
            string[] opciok = { "Játék", "Játékról", "Rankod", "Kihívások", "Fiókkezelés", "Tesztelés", "kilépés" };
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
                    Teszteleskiiratasa();
                    break;
                case 6:
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
            WriteLine("A játékról...");
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
                    ascii.Rankkiiras();
                    ResetColor();
                    break;
                case int n when (n > 9 && n <= 18):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine("arany!\n");
                    ascii.Rankkiiras();
                    ResetColor();
                    break;
                case int n when (n > 18 && n <= 27):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkGreen;
                    WriteLine("emeráld!\n");
                    ascii.Rankkiiras();
                    ResetColor();
                    break;
                case int n when (n > 27 && n <= 36):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkMagenta;
                    WriteLine("mester!\n");
                    ascii.Rankkiiras();
                    ResetColor();
                    break;
                case int n when (n > 36 && n <= 45):
                    Write("A rankod: ");
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine("hős!\n");
                    ascii.Rankkiiras();
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
            WriteLine(@"#1 Fegyverkeresés: Az első szobában keresd meg a biztonsági őr sokkolóját az asztalon!
#2 Kombinációs kihívás: A második szobában készíts lockpick-et a zárt ajtó kinyitásához vagy találj alternatív utat a fegyveredhez!
#3 Időzített menekülés: A harmadik szobában kerüld el vagy állítsd meg a terroristákat, mielőtt továbbmész!
#4 Boss Fight előkészítés: Készíts golyóálló mellényt a főellenség előtt!
#5 Taktikai döntés: Használd a kispisztolyt vagy a molotov koktélt a főellenséggel szemben!
#6 Elsősegély kihívás: Találj kötszert és használd fel a sebeid kezelésére!
#7 Rejtett ajtó keresése: Fedezd fel a raktárat a pályán!
#8 Lopakodás: Kerüld el az ellenfeleket és rejtőzz el előlük!
#9 Vadászat az ellenségre: Keresd meg és semmisítsd meg az összes ellenséget!
#10 Veszélyes csapdák: Óvakodj a rejtett csapdáktól a pályán!
#11 Gyorsított idő: Sietve juss át a következő szintre, mielőtt lejár az idő!
#12 Több ellenség, kevesebb fegyver: Harcolj meg több ellenféllel, mint amennyi lőszered van!
#13 Vegyes taktikák: Kombináld a fegyvereket és támadási stílusokat a hatékonyabb ellenfelek legyőzéséhez!
#14 Éjszakai küldetés: Teljesítsd a szintet sötétben, korlátozott láthatósággal!
#15 Gyorsított fegyvercsere: Cserélj fegyvert gyorsan a körülötted lévő tárgyakkal!
#16 Tárgyak gyorsan: Gyűjts minél több tárgyat a pályán a következő szintre való felkészüléshez!
#17 Szoros menekülés: Menekülj az ellenfelek elől egy szűk folyosón!
#18 Bosszú: Számolj le azokkal az őrökkel, akik elraboltak téged!
#19 Titkos üzenetek: Keresd meg és olvasd el a rejtett üzeneteket a pályán!
#20 Éleslátás: Használd az éleslátásodat a távoli tárgyak és ellenségek megtalálásához!
#21 Tárgygyűjtési küldetés: Gyűjts össze bizonyos számú tárgyat a pályán!
#22 Időzített robbanások: Használd a Molotov koktélt időzített robbanásként!
#23 Folyamatos mozgás: Mozogj állandóan a pályán a támadások elkerüléséhez!
#24 Szűkös források: Hatékonyan használd a korlátozott lőszerkészleteket és fegyvereket!
#25 Láthatatlanság: Használd a környezetet az elrejtőzéshez!");
            ReadKey(true);
            Clear();
            WriteLine(@"#26 Hangjelzők: Figyeld a hangokat az ellenfelek lokalizálásához!
#27 Megtévesztés: Tereld el az ellenfelek figyelmét és irányítsd őket!
#28 Szenvedélyes szökés: Használj minden rendelkezésre álló eszközt a meneküléshez!
#29 Játék a türelemmel: Légy türelmes és figyelmes a pályán!
#30 Sötét út: Vezess át a sötétben és használd az éjszakai látást!
#31 Támadás és visszavonulás: Támadj az ellenfelekre, majd húzz vissza!
#32 Váratlan ellenségek: Számolj be váratlan ellenfelekkel a pályán!
#33 Gyors és hatékony: Légy gyors és hatékony a harcban!
#34 Eszközkihasználás: Használd ki a körülötted lévő tárgyakat!
#35 Álcázás és elrejtőzés: Használj álcázást és elrejtőzést!
#36 Stratégiai felkészülés: Készülj fel stratégiai módon a következő szintre!
#37 Tűzkereszt: Használd ki a tűztereket az ellenfelek támadására!
#38 Jutalmak keresése: Keresd meg a rejtett jutalmakat a pályán!
#39 Búvóhelyek: Fedezz fel rejtett búvóhelyeket a pályán!
#40 Fegyverforgatókönyv: Készíts elő egy taktikát a következő szintre!
#41 Lézerszintű pontosság: Légy pontos és célzott a lövéseiddel!
#42 Gyors és veszélyes: Támadj gyorsan és hatékonyan!
#43 Kreatív felhasználás: Használj kreatív módszereket és tárgyakat!
#44 Társ segítségére: Dolgozz együtt a társaiddal!
#45 Ellenségek szétzúzása: Használj tárgyakat az ellenségek legyőzésére!");
            ReadKey(true);
            MainFuttatasa();
        }


        private void JatekFuttatas()
        {
            if (Mentes.valasztottFiokNev == null)
            {
                Clear();
                WriteLine("Fiókod: nincs kiválasztva");
                WriteLine("A játék futtatásához válassz vagy hozz létre egy fiókot.");
                ReadKey();

                Mentes Mentes = new Mentes();
                Mentes.Fomenu();
            }
            else
            {
                Jatek jatek = new Jatek();
                jatek.Foresz(); // Játék indítása
            }

            //A változtatások után

            //WriteLine("Remélem hogy tetszett a játékunk és köszönjük, hogy kipróbáltad!");

            //KilepesJatekbol();
        }

        private void Teszteleskiiratasa()
        {
            string prompt = "Mit szeretnél lefuttatni:\n";
            string[] opciok = { "Pisztoly", "AK-47", "Katona", "Terrorista", "Rohamosztagos", "Kötszer", "Koktél", "Joint" };
            Menu menu = new Menu(prompt, opciok);
            int Megjelolt = menu.Futas();

            switch (Megjelolt)
            {
                case 0:
                    Pisztolykiiratasa();
                    break;
                case 1:
                    Akkiiratasa();
                    break;
                case 2:
                    Katonakiiratasa();
                    break;
                case 3:
                    Terroristakiiratasa();
                    break;
                case 4:
                    Rohamosztagoskiiratasa();
                    break;
                case 5:
                    Kotszerkiiratasa();
                    break;
                case 6:
                    Koktelkiiratasa();
                    break;
                case 7:
                    Jointkiiratasa();
                    break;
            }
        }
        static bool stopRequested = false;

        Pisztoly pisztoly = new Pisztoly();
        
        private void Pisztolykiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                pisztoly.PisztolyKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        Ak ak = new Ak();

        private void Akkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ak.AkKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        Katona katona = new Katona();

        private void Katonakiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGreen;

            while (!stopRequested)
            {
                katona.KatonaKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        Terrorista terrorista = new Terrorista();

        private void Terroristakiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            while (!stopRequested)
            {
                terrorista.TerroristaKiir();
            }

            keyListenerThread.Join();

            MainFuttatasa();
        }

        Rohamosztagos rohamosztagos = new Rohamosztagos();

        private void Rohamosztagoskiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                rohamosztagos.RohamosztagosKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        Ascii ascii = new Ascii();
        private void Kotszerkiiratasa()
        {
            Clear();
            string kotszer = ascii.kotszer;
            WriteLine(kotszer);
            ReadKey(true);
            MainFuttatasa();
        }

        private void Koktelkiiratasa()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkMagenta;
            string koktel = ascii.koktel;
            WriteLine(koktel);
            ResetColor();
            ReadKey(true);
            MainFuttatasa();
        }

        private void Jointkiiratasa()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkGreen;
            string joint = ascii.joint;
            WriteLine(joint);
            ResetColor();
            ReadKey(true);
            MainFuttatasa();
        }

        static void KeyListener()
        {
            while (!stopRequested)
            {
                if (KeyAvailable)
                {
                    ReadKey(true);
                    stopRequested = true;
                }
            }
        }
    }
}