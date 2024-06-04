using BadgeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class Fiokok
    {
        List<Fiok> fiokok = new();
        public Fiokok(IEnumerable<string> adatok)
        {
            foreach (var item in adatok)
            {
                fiokok.Add(new Fiok(item));
            }
        }

        public int SzamolomUveg() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Uveg);
        public int SzamolomFemcso() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Femcso);
        public int SzamolomKes() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Kes);
        public int SzamolomSisak() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Sisak);
        public int SzamolomMelleny() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Melleny);
        public int SzamolomKotszer() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Kotszer);
        public int SzamolomKoktel() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Koktel);
        public int SzamolomJoint() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Joint);
        public int SzamolomAk() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Ak);
        public int SzamolomPisztoly() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Pisztoly);
        public int SzamolomEletero() => fiokok.Where(x => x.Nev == Mentes.valasztottFiokNev).Sum(x => x.Hp);
    }
}
