using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KalkulatorWalut2.Models
{
    public class PozycjaTabA
    {
        [XmlElement("nazwa_waluty")]
        public string NazwaWaluty { get; set; }


        [XmlElement("przelicznik")]
        public string Przelicznik { get; set; }


        [XmlElement("kod_waluty")]
        public string KodWaluty { get; set; }


        [XmlElement("kurs_sredni")]
        public string KursSredni { get; set; }


        [XmlIgnore]
        public decimal Kurs { 
            get{
                decimal kursLocal;
                string separator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                KursSredni = KursSredni.Replace(",", separator);
                decimal.TryParse(KursSredni, out kursLocal);
                return kursLocal;
            }  
        }
    }
}
