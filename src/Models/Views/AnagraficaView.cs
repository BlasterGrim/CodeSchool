using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Models.Views
{
    public class AnagraficaView {
        public int AnagraficaId {get;set;} 
        public string CodiceAnagrafica {get;set;}
        public bool IsAzienda {get;set;}
        public string Nome{get;set;}
        public string Cognome {get;set;}
        public string RagioneSociale {get;set;}
        public string PartitaIva {get;set;}
        public string CodiceFiscale {get;set;}
        public int TipoAnagraficaId{ get;set;}
        public TipoAnagraficaView TipoAnagrafica {get;set;}
        public ICollection<ContattiView> Contatti { get; set; }
        public ICollection<IndirizziView> Indirizzi { get; set; }
    }
}