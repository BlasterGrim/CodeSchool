using Models.Tabelle;

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
        public string Descrizione { get; set; }
    }
}