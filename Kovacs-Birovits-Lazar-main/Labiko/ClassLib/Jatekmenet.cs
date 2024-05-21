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
            string[] opciok = { "Játék", "Játékról", "Rankod", "Kihívások", "Tesztelés", "kilépés" };
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
                    Rankkiiratása();
                    break;
                case 3:
                    Kihivasokkiiratasa();
                    break;
                case 4:
                    Teszteleskiiratasa();
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

        private void JatekrolMegjelenites()
        {
            Clear();
            WriteLine("A játékról...");
            WriteLine();
            ReadKey(true);
            MainFuttatasa();
        }

        private void Rankkiiratása()
        {
            Clear();
            Kihivasok kihivaslista = new(File.ReadAllLines("feladatok.txt", Encoding.UTF8).Skip(1));

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
            WriteLine(@"#1 Fegyverkeresés: Az első szobában csak egy törött vodkásüveggel rendelkezel. A szoba sötét és zűrzavaros, de találhatsz egy biztonsági őr sokkolóját egy asztalon. Keresd meg mielőtt az őr visszatérne!
#2 Kombinációs kihívás: A második szobában találsz egy zárt ajtót, amely mögött a fő fegyvered, a kispisztoly van. A kulcs az asztalon van, de nincs nálad lockpick. Keresd meg a kötészetet, hogy készíts egyet, vagy találj egy alternatív utat a fegyverhez!
#3 Időzített menekülés: A harmadik szobában a terroristák már közelednek, és nincs sok időd a kijutásra. Találj egy módját, hogy megakadályozd őket, vagy találj egy gyorsabb kijutási utat!
#4 Boss Fight előkészítés: Mielőtt a főellenséggel, a túszejtők vezetőjével találkoznál, készítsd fel magadat. Találj egy fémcsövet, és craftolj belőle egy golyóálló mellényt a védelem érdekében!
#5 Taktikai döntés: Végül, amikor szembeszállsz a főellenséggel, döntened kell, hogy melyik fegyvert használod. Használd a kispisztolyt a távolsági harcra, vagy a molotov koktélt a közelharchoz? A jó döntés a túlélés kulcsa lehet!
#6 Elsősegély kihívás: Találj egy kötszert és használd fel a sebeid kezelésére, mielőtt továbbmész a következő szintre!
#7 Rejtett ajtó keresése: Fedezd fel a pályát, hogy megtaláld a rejtett ajtót, ami egy raktárhoz vezet, ahol fegyverek és extra gyógyító eszközök találhatók!
#8 Lopakodás: Kerüld el az ellenfeleket, és próbálj meg elrejtőzni előlük, hogy ne kelljen harcba bocsátkoznod velük!
#9 Vadászat az ellenségre: Keresd meg és semmisítsd meg az összes ellenfelet egy adott szobában, mielőtt továbbmész!
#10 Veszélyes csapdák: Óvakodj a rejtett csapdáktól a pályán, és kerüld el őket, mielőtt kijutsz a következő szobába!
#11 Gyorsított idő: A játékosoknak csak rövid idő áll rendelkezésre a következő szintre való átjutásra, mielőtt a terroristák rájuk találnak!
#12 Több ellenség, kevesebb fegyver: Harcolj meg több ellenféllel, mint amennyi lőszerved van, és találj kreatív megoldásokat a túlélésre!
#13 Vegyes taktikák: Vegyítsd a különböző fegyvereket és támadási stílusokat a hatékonyabb ellenfelek legyőzéséhez!
#14 Éjszakai küldetés: Teljesítsd a szintet sötétben, ahol csak korlátozott láthatóságod van, és figyelj az ellenségek árnyékaira!
#15 Gyorsított fegyvercsere: Használd a körülötted lévő tárgyakat, hogy gyorsan fegyvert cserélj és alkalmazkodj az ellenségek támadásaira!
#16 Tárgyak gyorsan: Gyűjts össze minél több tárgyat a pályán, mielőtt továbbmész, hogy felkészülj a következő szintre!
#17 Szoros menekülés: Menekülj az ellenfelek elől egy szűk folyosón, miközben figyelsz a támadásokra és a csapdákra!
#18 Bosszú: Keresd meg és számolj le azokkal az őrökkel, akik elraboltak téged, mielőtt továbbhaladnál a következő szintre!
#19 Titkos üzenetek: Keresd meg és olvasd el a rejtett üzeneteket a pályán, ami segíthet a túlélésben és a fejtörők megoldásában!
#20 Éleslátás: Használd ki az éleslátásodat, hogy megtaláld a távoli tárgyakat és ellenségeket a pályán!
#21 Tárgygyűjtési küldetés: Gyűjts össze bizonyos számú gyógyító eszközt és fegyvert a pályán található elrejtett helyekről, mielőtt továbbmész!
#22 Időzített robbanások: Készítsd elő a Molotov koktélt és robbantsd fel az ellenségeket egy időzített robbanás segítségével!
#23 Folyamatos mozgás: Mozogj állandóan a pályán, hogy elkerüld az ellenségek támadásait és könnyebben találj elhelyezkedéseket a győzelemhez!
#24 Szűkös források: Keresd meg a korlátozott lőszerkészleteket és fegyvereket, és használd őket hatékonyan az ellenfelek legyőzésére!
#25 Láthatatlanság: Használd a körülötted lévő környezetet, hogy elrejtőzz az ellenfelek elől és ne essenek rád a támadások!");
            ReadKey(true);
            Clear();
            WriteLine(@"#26 Hangjelzők: Figyeld a hangokat és használd őket az ellenfelek lokalizálására és megelőzésére!
#27 Megtévesztés: Használd a környezetet és a tárgyakat a téveszteni az ellenfeleket, hogy könnyebben átjuss a pályán!
#28 Gyors reakció: Készíts elő gyorsan a támadásokra és válaszolj az ellenfelek támadásaira, hogy ne veszítsd el az irányítást a harcban!
#29 Védelmi stratégiák: Keresd meg a védő pozíciókat a pályán és használd őket a túléléshez és a támadások elkerüléséhez!
#30 Váratlan fordulatok: Számolj be váratlan eseményekkel és helyzetekkel a pályán, amelyek fokozzák a játék izgalmát és kihívásait!
#31 Eltérítés: Tereld el az ellenfelek figyelmét és irányítsd őket azáltal, hogy hangokat vagy tárgyakat használsz, hogy könnyebben kijuthass a pályáról!
#32 Szenvedélyes szökés: Használj minden rendelkezésre álló eszközt és képességet a szenvedélyes meneküléshez és a túléléshez a pályán!
#33 Játék a türelemmel: Legyél türelmes és figyelmes a pályán, hogy könnyebben felismerd az ellenségek mozgását és taktikáit!
#34 Sötét út: Vezess át a sötétben a pályán és használd ki az éjszakai látást és a tárgyakat, hogy ne veszítsd el az irányt!
#35 Támadás és visszavonulás: Támadj az ellenfelekre, majd húzz vissza a biztonságos helyre, hogy elkerüld a sérüléseket és gyorsabban kijuss a pályáról!
#36 Váratlan ellenségek: Számolj be váratlan ellenfelekkel a pályán, amelyek elősegítik a kihívásokat és a fejlesztéseket a játék során!
#37 Gyors és hatékony: Legyél gyors és hatékony a támadásokban és a védelemben, hogy ne veszítsd el az irányítást a harcban!
#38 Eszközkihasználás: Használd ki a körülötted lévő tárgyakat és környezetet a győzelemhez és a túléléshez a pályán!
#39 Álcázás és elrejtőzés: Használj álcázást és elrejtőzést a pályán, hogy könnyebben elkerüld az ellenfelek támadásait és ne essenek rád!
#40 Stratégiai felkészülés: Készülj fel stratégiai módon a következő szintre, gyűjts össze tárgyakat és fegyvereket a pályán, hogy könnyebben túlélj!
#41 Tűzkereszt: Használd ki a tűztereket és a robbanó tárgyakat az ellenfelek támadására és elijesztésére!
#42 Jutalmak keresése: Keresd meg a rejtett jutalmakat a pályán, amelyek extra lőszerrel, gyógyító eszközökkel és fegyverekkel jutalmaznak!
#43 Búvóhelyek: Fedezz fel rejtett búvóhelyeket a pályán, amelyek segítenek elrejtőzni az ellenségek elől és támadásokra készülni!
#44 Fegyverforgatókönyv: Készíts elő egy taktikát és fegyverforgatókönyvet a következő szintre, figyelembe véve az ellenfelek típusát és helyét!
#45 Lézerszintű pontosság: Légy pontos és célzott a lövéseiddel, hogy hatékonyan megölj minden ellenséget és kijuss a pályáról!
#46 Gyors és veszélyes: Gyorsan és hatékonyan támadj az ellenfelekre, hogy megzavarjad és elijesszed őket a következő szintre való átjutás előtt!
#47 Kreatív felhasználás: Használj kreatív módszereket és tárgyakat a pályán, hogy megtéveszd és legyőzd az ellenfeleket!
#48 Társ segítségére: Használj együttműködésre és taktikai kiegészítésekre a társaidkal a pályán, hogy könnyebben kijuthassatok az ellenségek közül!
#49 Ellenségek szétzúzása: Szétzúzd az ellenségeket körülötted található tárgyakkal és környezeti elemekkel, hogy könnyebben túlélj a harcban!
#50 Utolsó menedék: Keresd meg az utolsó menedéket a pályán, ahol biztonságban érezheted magadat és gyógyíthatsz a következő szintre való átjutás előtt!");
            ReadKey(true);
            MainFuttatasa();
        }

        private void JatekFuttatas()
        {
            string prompt = "Milyen színű legyen a játék?";
            string[] opciok = { "Piros", "Zöld", "Kék", "Sárga" };
            Menu szinekMenu = new Menu(prompt, opciok);
            int Megjelolt = szinekMenu.Futas();

            BackgroundColor = ConsoleColor.Black;

            switch (Megjelolt)
            {
                case 0:
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("\nA játék pirossá változott!");
                    break;
                case 1:
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("\nA játék zölddé változott!");
                    break;
                case 2:
                    ForegroundColor = ConsoleColor.Blue;
                    WriteLine("\nA játék kékké változott!");
                    break;
                case 3:
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("\nA játék sárgává változott!");
                    break;
            }

            Thread.Sleep(2000);

            Jatek jatek = new Jatek();
            jatek.Foresz();

            //A változtatások után

            //WriteLine("Remélem hogy tetszett a játékunk és köszönjük, hogy kipróbáltad!");

            //KilepesJatekbol();
        }

        private void Teszteleskiiratasa()
        {
            string prompt = "Melyik animációt szeretnéd látni?\n";
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

        Ascii ascii = new Ascii();

        private void Pisztolykiiratasa()
        {
            Clear();
            WriteLine("Pisztoly");
            ReadKey(true);
            MainFuttatasa();
        }

        private void Akkiiratasa()
        {
            Clear();
            WriteLine("Ak-47");
            ReadKey(true);
            MainFuttatasa();
        }

        private void Katonakiiratasa()
        {
            Clear();
            WriteLine("Katona");
            ReadKey(true);
            MainFuttatasa();
        }

        private void Terroristakiiratasa()
        {
            Clear();
            WriteLine("Terrorista");
            ReadKey(true);
            MainFuttatasa();
        }

        private void Rohamosztagoskiiratasa()
        {
            Clear();
            WriteLine("Rohamosztagos");
            ReadKey(true);
            MainFuttatasa();
        }

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
            string koktel = ascii.koktel;
            WriteLine(koktel);
            ReadKey(true);
            MainFuttatasa();
        }

        private void Jointkiiratasa()
        {
            Clear();
            string joint = ascii.joint;
            WriteLine(joint);
            ReadKey(true);
            MainFuttatasa();
        }
    }
}