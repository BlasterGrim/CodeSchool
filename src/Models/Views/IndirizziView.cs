using System.ComponentModel.DataAnnotations;

namespace Models.Views
{
    public class IndirizziView
    {
        public int IndirizziId {get;set;}
        public string Nazione {get;set;}
        public string Regione {get;set;}
        public string Provincia {get;set;}
        public string Citta {get;set;}
        public string Denominazione {get;set;}
        public string Cap{get;set;}
        public string Numero {get;set;}
        public int AnagraficaId {get;set;}
        public AnagraficaView Anagrafica {get;set;}
        public int TipoIndirizzoId {get;set;}
        public TipoIndirizzoView TipoIndirizzo {get;set;}
    }
}