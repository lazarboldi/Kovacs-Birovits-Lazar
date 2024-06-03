using ClassLib;
using System;
using System.IO;

public class FiokKezeles
{
    private static readonly string FilePath;

    static FiokKezeles()
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
        var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
        FilePath = Path.Combine(classLibDirectory, "beallitasok.txt");
    }

    private string[] GetFiokSorok()
    {
        return File.ReadAllLines(FilePath);
    }

    private void UpdateFiokSorok(string[] fiokSorok)
    {
        File.WriteAllLines(FilePath, fiokSorok);
    }

    private int GetFiokIndex(string nev)
    {
        string[] fiokok = GetFiokSorok();
        for (int i = 1; i < fiokok.Length; i++)
        {
            if (fiokok[i].Split(';')[0] == nev)
            {
                return i;
            }
        }
        return -1;
    }

    private void ModifyFiokData(string nev, int index, int value)
    {
        int fiokIndex = GetFiokIndex(nev);
        if (fiokIndex == -1) return;

        string[] fiokok = GetFiokSorok();
        string[] adatok = fiokok[fiokIndex].Split(';');
        int currentValue = int.Parse(adatok[index]);

        adatok[index] = (Math.Max(0, currentValue + value)).ToString();

        fiokok[fiokIndex] = string.Join(";", adatok);
        UpdateFiokSorok(fiokok);
    }

    public Fiok LoadFiok(string nev)
    {
        int fiokIndex = GetFiokIndex(nev);
        if (fiokIndex == -1) return null;

        string[] fiokok = GetFiokSorok();
        string[] adatok = fiokok[fiokIndex].Split(';');

        return new Fiok
        {
            Nev = adatok[0],
            Jelszo = adatok[1],
            Eletero = int.Parse(adatok[2]),
            PisztolyLoszer = int.Parse(adatok[3]),
            AkLoszer = int.Parse(adatok[4]),
            Kotszer = int.Parse(adatok[5]),
            Koktel = int.Parse(adatok[6]),
            Joint = int.Parse(adatok[7]),
            Melleny = bool.Parse(adatok[8]),
            Sisak = bool.Parse(adatok[9]),
            LetrehozasIdeje = DateTime.Parse(adatok[10])
        };
    }

    public void SaveFiok(Fiok fiok)
    {
        int fiokIndex = GetFiokIndex(fiok.Nev);
        if (fiokIndex == -1) return;

        string[] fiokok = GetFiokSorok();
        string[] adatok = new string[]
        {
            fiok.Nev,
            fiok.Jelszo,
            fiok.Eletero.ToString(),
            fiok.PisztolyLoszer.ToString(),
            fiok.AkLoszer.ToString(),
            fiok.Kotszer.ToString(),
            fiok.Koktel.ToString(),
            fiok.Joint.ToString(),
            fiok.Melleny.ToString(),
            fiok.Sisak.ToString(),
            fiok.LetrehozasIdeje.ToString()
        };

        fiokok[fiokIndex] = string.Join(";", adatok);
        UpdateFiokSorok(fiokok);
    }

    public void NovelemPisztoly(string nev) => ModifyFiokData(nev, 2, 15);
    public void CsokkentemPisztoly(string nev) => ModifyFiokData(nev, 2, -1);
    public void NovelemAk(string nev) => ModifyFiokData(nev, 3, 30);
    public void CsokkentemAk(string nev) => ModifyFiokData(nev, 3, -1);
    public void NovelemKotszer(string nev) => ModifyFiokData(nev, 4, 1);
    public void CsokkentemKotszer(string nev) => ModifyFiokData(nev, 4, -1);
    public void NovelemKoktel(string nev) => ModifyFiokData(nev, 5, 1);
    public void CsokkentemKoktel(string nev) => ModifyFiokData(nev, 5, -1);
    public void NovelemJoint(string nev) => ModifyFiokData(nev, 6, 1);
    public void CsokkentemJoint(string nev) => ModifyFiokData(nev, 6, -1);
    public void NovelemHp(string nev) => ModifyFiokData(nev, 7, 1);
    public void CsokkentemHp(string nev) => ModifyFiokData(nev, 7, -1);
    public void NovelemUveg(string nev) => ModifyFiokData(nev, 8, 1);
    public void CsokkentemUveg(string nev) => ModifyFiokData(nev, 8, -1);
    public void NovelemFemcso(string nev) => ModifyFiokData(nev, 9, 1);
    public void CsokkentemFemcso(string nev) => ModifyFiokData(nev, 9, -1);
    public void NovelemKes(string nev) => ModifyFiokData(nev, 10, 1);
    public void CsokkentemKes(string nev) => ModifyFiokData(nev, 10, -1);
    public void NovelemMelleny(string nev) => ModifyFiokData(nev, 11, 1);
    public void CsokkentemMelleny(string nev) => ModifyFiokData(nev, 11, -1);
    public void NovelemSisak(string nev) => ModifyFiokData(nev, 12, 1);
    public void CsokkentemSisak(string nev) => ModifyFiokData(nev, 12, -1);
}