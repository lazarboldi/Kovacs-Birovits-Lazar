public class Fiok
{
    public string Nev { get; private set; }
    public string Jelszo { get; private set; }
    public int Pisztoly { get; private set; }
    public int Ak { get; private set; }
    public int Kotszer { get; private set; }
    public int Koktel { get; private set; }
    public int Joint { get; private set; }
    public int Hp { get; set; }
    public int Uveg { get; private set; }
    public int Femcso { get; private set; }
    public int Kes { get; private set; }
    public int Melleny { get; set; }
    public int Sisak { get; set; }
    public DateTime LetrehozasIdeje { get; private set; }

    public Fiok(string adatsor)
    {
        string[] adatok = adatsor.Split(';');

        if (adatok.Length == 14) // Ellenőrzés az adatsor hosszára
        {
            Nev = adatok[0];
            Jelszo = adatok[1];
            Pisztoly = int.Parse(adatok[2]);
            Ak = int.Parse(adatok[3]);
            Kotszer = int.Parse(adatok[4]);
            Koktel = int.Parse(adatok[5]);
            Joint = int.Parse(adatok[6]);
            Hp = int.Parse(adatok[7]);
            Uveg = int.Parse(adatok[8]);
            Femcso = int.Parse(adatok[9]);
            Kes = int.Parse(adatok[10]);
            Melleny = int.Parse(adatok[11]);
            Sisak = int.Parse(adatok[12]);
            LetrehozasIdeje = DateTime.Parse(adatok[13]);
        }
    }
}
