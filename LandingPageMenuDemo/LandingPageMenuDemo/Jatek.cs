using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LandingPageMenuDemo
{
    public class Jatek
    {
        public void Kezd()
        {
            Title = "Escape from the club - A játék!";
            MainFuttatasa();
            WriteLine("Nyomj bármilyen gombot a kilépéshez...");
            ReadKey(true);
        }
        private void MainFuttatasa()
        {
            string prompt = @"
___________                                    ___________                       ___________.__             _________ .__       ___.    
\_   _____/ ______ ____ _____  ______   ____   \_   _____/______  ____   _____   \__    ___/|  |__   ____   \_   ___ \|  |  __ _\_ |__  
 |    __)_ /  ___// ___\\__  \ \____ \_/ __ \   |    __) \_  __ \/  _ \ /     \    |    |   |  |  \_/ __ \  /    \  \/|  | |  |  \ __ \ 
 |        \\___ \\  \___ / __ \|  |_> >  ___/   |     \   |  | \(  <_> )  Y Y  \   |    |   |   Y  \  ___/  \     \___|  |_|  |  / \_\ \
/_______  /____  >\___  >____  /   __/ \___  >  \___  /   |__|   \____/|__|_|  /   |____|   |___|  /\___  >  \______  /____/____/|___  /
        \/     \/     \/     \/|__|        \/       \/                       \/                  \/     \/          \/               \/ 

( Az ajánlott képernyő szélességet akkor érheti el, ha kirajzolódott a cím. )
( Ilyen méretű magassággal érheti el az ajánlott méretet! )

Köszöntelek az Escape from the club szimulátorunkban! Hogyan tovább?
(Használd a nyilakat a választáshoz és nyomd meg az entert!)";
            string[] opciok = { "Játék", "Játékról", "Badgek", "kilépés" };
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
                    Badgekkiiratása();
                    break;
                case 3:
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

        private void Badgekkiiratása()
        {
            Clear();
            WriteLine("Ide lesznek kiírva a badgek!");
            ReadKey(true);
            MainFuttatasa();
        }

        private void JatekFuttatas()
        {
            string prompt = "Milyen színű legyen a karaktered?";
            string[] opciok = { "Piros", "Zöld", "Kék", "Sárga" };
            Menu szinekMenu = new Menu(prompt, opciok);
            int Megjelolt = szinekMenu.Futas();

            BackgroundColor = ConsoleColor.Black;

            switch (Megjelolt)
            {
                case 0:
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Karaktered pirossá változott!");
                    break;
                case 1:
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("Karaktered zölddé változott!");
                    break;
                case 2:
                    ForegroundColor = ConsoleColor.Blue;
                    WriteLine("Karaktered kékké változott!");
                    break;
                case 3:
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Karaktered sárgává változott!");
                    break;
            }
            ResetColor();

            ReadKey(true);
            MainFuttatasa();

            //WriteLine("Remélem hogy tetszett a játékunk és köszönjük, hogy kipróbáltad!");

            //KilepesJatekbol();
        }
    }
}
