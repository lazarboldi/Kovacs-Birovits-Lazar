using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ClassLib
{
    public class Animacio
    {

        static bool stopRequested = false;

        Ascii ascii = new Ascii();
        public void Pisztolykiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ascii.PisztolyKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Akkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ascii.AkKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Uvegkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            while (!stopRequested)
            {
                ascii.UvegKiir();
            }

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Femcsokiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ascii.FemcsoKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Keskiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ascii.KesKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Katonakiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGreen;

            while (!stopRequested)
            {
                ascii.KatonaKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Terroristakiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            while (!stopRequested)
            {
                ascii.TerroristaKiir();
            }

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Rohamosztagoskiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ascii.RohamosztagosKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Foellensegkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            while (!stopRequested)
            {
                ascii.FoellensegKiir();
            }

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Kotszerkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            while (!stopRequested)
            {
                ascii.KotszerKiir();
            }

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Koktelkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkMagenta;

            while (!stopRequested)
            {
                ascii.KoktelKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Jointkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGreen;

            while (!stopRequested)
            {
                ascii.JoKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Mellenykiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ascii.MellenyKiir();
            }

            ResetColor();

            keyListenerThread.Join();

            MainFuttatasa();
        }

        public void Sisakkiiratasa()
        {
            Clear();
            stopRequested = false;

            Thread keyListenerThread = new Thread(KeyListener);
            keyListenerThread.Start();

            ForegroundColor = ConsoleColor.DarkGray;

            while (!stopRequested)
            {
                ascii.SisakKiir();
            }

            ResetColor();

            keyListenerThread.Join();

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

        private void MainFuttatasa()
        {
            Clear();
            Jatek jatek = new Jatek();
            jatek.Foresz();
        }
    }
}
