using System.ComponentModel.DataAnnotations;
using Models.Tabelle;
using Models.Views;

namespace Models.Views
{
    public class TipoIndirizzoView
    {
        public TipoIndirizzoView()
        {
            //TipoAnagraficaId = -1;
            Descrizione = null;
        }
        public TipoIndirizzoView(TipoIndirizzo tbl)
        {
            TipoIndirizzoId = tbl.TipoIndirizzoId;
            Descrizione = tbl.Descrizione;
        }
        public int TipoIndirizzoId { get; set; }

        [Required]
        [Display(Name = "Tipo Indirizzo")]
        public string Descrizione { get; set; }
    }
}