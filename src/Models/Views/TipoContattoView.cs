using System.ComponentModel.DataAnnotations;
using Models.Tabelle;

namespace Models.Views
{
    public class TipoContattoView
    {
        public TipoContattoView()
        {
            //TipoAnagraficaId = -1;
            Descrizione = null;
        }
        public TipoContattoView(TipoContatto tbl)
        {
            TipoContattoId = tbl.TipoContattoId;
            Descrizione = tbl.Descrizione;
        }
        public int TipoContattoId { get; set; }

        [Required]
        [Display(Name = "Tipo Contatto")]
        public string Descrizione { get; set; }

    }
}