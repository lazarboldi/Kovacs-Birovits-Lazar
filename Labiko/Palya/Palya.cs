



namespace Palya
{
    public class Palya
    {



        public int[] Szoba1 { get; init; }
        public int[] Szoba2 { get; init; }
        public int[] Szoba3 { get; init; }
        public int[] Szoba4 { get; init; }

        public Palya()
        {
            Szoba1 = new int[2];
            Szoba1[0] = 0;
            Szoba1[1] = 0;


            Szoba2 = new int[2];
            Szoba2[0] = 0;
            Szoba2[1] = 0;

            Szoba3 = new int[2];
            Szoba3[0] = 0;
            Szoba3[1] = 0;

            Szoba4 = new int[2];
            Szoba4[0] = 0;
            Szoba4[1] = 0;

        }


        public void AlapInic(out char[,] palya, out int jatekosX, out int jatekosY)
        {





            palya = new char[Szoba1[0], Szoba1[1]];







            jatekosX = Szoba1[0] / 2;
            jatekosY = Szoba1[1] / 2;

            // Pálya feltöltése falakkal és üres területekkel // Lázár feladat
            for (int i = 0; i < Szoba1[0]; i++)
            {
                for (int j = 0; j < Szoba1[1]; j++)
                {
                    // Falak a pálya szélén
                    if (i == 0 || j == 0 || i == Szoba1[0] - 1 || j == Szoba1[1] - 1)
                    {
                        palya[i, j] = 'X';
                    }
                    else
                    {
                        palya[i, j] = '.';
                    }
                }
            }

            // Játékos elhelyezése a pálya közepén
            palya[jatekosX, jatekosY] = 'P';

            // Tárgyak és élőlények elhelyezése
            Random rand = new();
            int targyakSzama = 8;
            int elolenyekSzama = 5;

            for (int i = 0; i < targyakSzama; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(1, Szoba1[0] - 1);
                    y = rand.Next(1, Szoba1[1] - 1);
                } while (palya[x, y] != '.');

                palya[x, y] = 'T';
            }

            for (int i = 0; i < elolenyekSzama; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(1, Szoba1[0] - 1);
                    y = rand.Next(1, Szoba1[1] - 1);
                } while (palya[x, y] != '.');

                palya[x, y] = 'E';
            }
        }





        public void Inicializalas(int id, int i, int j, char[,] palya)
        {

            switch (id)
            {
                case int x when x == 1 && palya[i, j + 1] == 'A':


                    id = 2;

                    break;

                case int x when x == 2 && palya[i, j - 1] == 'A':

                    AlapInic(out palya, jatekosX, jatekosY);
                    id = 1;

                    break;


                case int x when x == 2 && palya[i, j + 1] == 'A':


                    id = 3;

                    break;


                case int x when x == 3 && palya[i - 1, j] == 'A':


                    id = 4;

                    break;


                case int x when x == 3 && palya[i, j - 1] == 'A':


                    id = 2;

                    break;


                case int x when x == 4 && palya[i, j + 1] == 'A':


                    id = 4;

                    break;


            }













            // Pálya feltöltése falakkal és üres területekkel // Lázár feladat
            for (int i = 0; i < palyaMeret; i++)
            {
                for (int j = 0; j < palyaMeret; j++)
                {
                    // Falak a pálya szélén
                    if (i == 0 || j == 0 || i == palyaMeret - 1 || j == palyaMeret - 1)
                    {
                        palya[i, j] = 'X';
                    }
                    else
                    {
                        palya[i, j] = '.';
                    }
                }
            }

            // Játékos elhelyezése a pálya közepén
            palya[jatekosX, jatekosY] = 'P';

            // Tárgyak és élőlények elhelyezése
            Random rand = new Random();
            int targyakSzama = 8;
            int elolenyekSzama = 5;

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






    }
}