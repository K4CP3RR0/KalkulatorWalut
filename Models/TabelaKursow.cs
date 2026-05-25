using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KalkulatorWalut2.Models
{
    [XmlRoot("tabela_kursow")]

    public class TabelaKursow
    {
        [XmlElement("pozycja")]
        public List<PozycjaTabA> pozycje { get; set; }

    }
}
