using System.ComponentModel.DataAnnotations;
using Models.Tabelle;

namespace Models.Views
{
    public class IndirizziView
    {
        public IndirizziView()
        {
            IndirizziId = -1;
            Nazione = null;
            Regione = null;
            Provincia = null;
            Citta = null;
            Denominazione = null;
            Cap = null;
            Numero = null;
            AnagraficaId = -1;
            TipoIndirizzoId = -1;
        }
        public IndirizziView(Indirizzi tbl)
        {
            IndirizziId = tbl.IndirizziId;
            Nazione = tbl.Nazione;
            Regione = tbl.Regione;
            Provincia = tbl.Provincia;
            Citta = tbl.Citta;
            Denominazione = tbl.Denominazione;
            Cap = tbl.Cap;
            Numero = tbl.Numero;
            AnagraficaId = tbl.AnagraficaId;
            TipoIndirizzoId =tbl.TipoIndirizzoId;
        }
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