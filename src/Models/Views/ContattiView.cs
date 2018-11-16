using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Tabelle;

namespace Models.Views
{
    public class ContattiView
    {
        public ContattiView()
        {
            //ContattiId = -1;
            Valore = null;
            Note = null;
            AnagraficaId = -1;
            TipoContattoId = -1;
        }
        public ContattiView(Contatti tbl)
        {
            ContattiId = tbl.ContattiId;
            Valore = tbl.Valore;
            Note = tbl.Note;
            AnagraficaId = tbl.AnagraficaId;
            TipoContattoId = tbl.TipoContattoId;
        }
        public int ContattiId { get; set; }

        [Required]
        public string Valore { get; set; }
        public string Note { get; set; }
        public int AnagraficaId { get; set; }
        public AnagraficaView Anagrafica { get; set; }
        public int TipoContattoId { get; set; }
        public TipoContattoView TipoContatto { get; set; }

    }
}