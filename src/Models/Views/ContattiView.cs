namespace Models.Views {
    public class ContattiView {
        public int ContattiId {get;set;}
        public string Valore{get;set;}
        public string Note {get;set;}
        public int AnagraficaId {get;set;}
         public int TipoContattoId {get;set;}
        public TipoContattoView TipoContatto {get;set;}
    }
}