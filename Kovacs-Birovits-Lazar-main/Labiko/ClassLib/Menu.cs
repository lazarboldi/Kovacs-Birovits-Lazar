using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LandingPageMenuDemo
{
    public class Menu
    {
        private int Megjelolt;
        private string[] Opciok;
        private string Prompt;

        public Menu(string prompt, string[] opciok)
        {
            Prompt = prompt;
            Opciok = opciok;
            Megjelolt = 0;
        }

        private void OpciokMegjelenitese()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Opciok.Length; i++)
            {
                string jelenlegiOpcio = Opciok[i];
                string elotag;

                if (i == Megjelolt)
                {
                    elotag = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    elotag = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                WriteLine($"{elotag} << {jelenlegiOpcio} >>");
            }
            ResetColor();
        }

        public int Futas()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                OpciokMegjelenitese();


                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                //Megjelölt index frissítése a nyilakkal.

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    Megjelolt--;
                    if (Megjelolt == -1)
                    {
                        Megjelolt = Opciok.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    Megjelolt++;
                    if (Megjelolt == Opciok.Length)
                    {
                        Megjelolt = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return Megjelolt;
        }
    }
}
