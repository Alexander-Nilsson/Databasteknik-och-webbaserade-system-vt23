using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;





namespace Laboration_3___Databasdriven_webbapplikation.Models {
    public class PersonDetalj {

        //Konstruktor
        public PersonDetalj() {
        }

        //Publika egenskaper
        public string Fornamn { get; set; }
        public string Efternamn { get; set; }
        public string Epost { get; set; }
        public int Fodelsear { get; set; }
        public int Bor { get; set; }
        public int Id { get; set; }

    }
}
