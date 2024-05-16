using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgeSystem
{
    public class Kihivasok
    {
        List<Kihivas> kihivasok = new();
        public Kihivasok(IEnumerable<string> adatok)
        {
            foreach (var item in adatok)
            {
                kihivasok.Add(new Kihivas(item));
            }
        }
        public int Megszamlalas() => kihivasok.Count(x => x.Teljesites == "teljesitett");
    }
}
