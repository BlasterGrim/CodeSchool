using System.ComponentModel.DataAnnotations;
using Models.Tabelle;

namespace Models.Views
{
    public class TipoAnagraficaView
    {
        public TipoAnagraficaView()
        {
            //TipoAnagraficaId = -1;
            Descrizione = null;
        }
        public TipoAnagraficaView(TipoAnagrafica tbl)
        {
            TipoAnagraficaId = tbl.TipoAnagraficaId;
            Descrizione = tbl.Descrizione;
        }
        public int TipoAnagraficaId { get; set; }
        
        [Required]
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
    }
}