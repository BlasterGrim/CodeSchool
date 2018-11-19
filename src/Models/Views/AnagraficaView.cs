using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Models.Tabelle;

namespace Models.Views
{
    public class AnagraficaView
    {
        public AnagraficaView()
        {
            //AnagraficaId = -1;
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
        [Display(Name="Codice")]
        public string CodiceAnagrafica { get; set; }

        [Required]
        [Display(Name="È azienda?")]
        public bool IsAzienda { get; set; }

        [Required]
        [Display(Name="Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name="Cognome")]
        public string Cognome { get; set; }

        [Required]
        [Display(Name="Ragione sociale")]
        public string RagioneSociale { get; set; }

        [Required]
        [Display(Name="Partita IVA")]
        public string PartitaIva { get; set; }

        [Required]
        [Display(Name="Codice fiscale")]
        public string CodiceFiscale { get; set; }

        [Display(Name="Tipo anagrafica")]
        public int TipoAnagraficaId { get; set; }
        public TipoAnagraficaView TipoAnagrafica {get;set;}
        public ICollection<ContattiView> Contatti { get; set; }
        public ICollection<IndirizziView> Indirizzi { get; set; }

        public ContattiView Contatti2 { get; set; }
        public IndirizziView Indirizzi2 { get; set; }
    }
}