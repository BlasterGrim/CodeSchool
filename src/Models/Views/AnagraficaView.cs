using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Models.Tabelle;

namespace Models.Views
{
    public class AnagraficaView
    {
        public AnagraficaView()
        {
            AnagraficaId = -1;
            CodiceAnagrafica = null;
            IsAzienda = false;
            Nome = null;
            Cognome = null;
            RagioneSociale = null;
            PartitaIva = null;
            CodiceFiscale = null;
            TipoAnagraficaId = -1;
        }
        public AnagraficaView(Anagrafica tbl)
        {
            AnagraficaId = tbl.AnagraficaId;
            CodiceAnagrafica = tbl.CodiceAnagrafica;
            IsAzienda = tbl.IsAzienda;
            Nome = tbl.Nome;
            Cognome = tbl.Cognome;
            RagioneSociale = tbl.RagioneSociale;
            PartitaIva = tbl.PartitaIva;
            CodiceFiscale = tbl.CodiceFiscale;
            TipoAnagraficaId = tbl.TipoAnagraficaId;
        }
        [Key]
        public int AnagraficaId { get; set; }
        
        [Required]
        public string CodiceAnagrafica { get; set; }
        public bool IsAzienda { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string RagioneSociale { get; set; }
        public string PartitaIva { get; set; }
        public string CodiceFiscale { get; set; }
        public int TipoAnagraficaId { get; set; }
        public TipoAnagraficaView TipoAnagrafica {get;set;}
        public ICollection<ContattiView> Contatti { get; set; }
        public ICollection<IndirizziView> Indirizzi { get; set; }
        //public ContattiView Contatti { get; set; }
        //public TipoAnagraficaView TipoAnagrafica { get; set; }
    }
}