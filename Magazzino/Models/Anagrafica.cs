using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Magazzino.Models
{
    public partial class Anagrafica
    {
        public int Id { get; set; }
        [DisplayName("Codice")]
        [Remote(controller: "Anagrafica", action: "ValidateCodice", ErrorMessage = "Codice articolo già in uso.")]
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
    }
}
