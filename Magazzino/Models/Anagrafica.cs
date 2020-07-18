using System;
using System.Collections.Generic;

namespace Magazzino.Models
{
    public partial class Anagrafica
    {
        public int Id { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
    }
}
