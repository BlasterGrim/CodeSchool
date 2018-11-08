using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace AspNetMvc.Controllers{
    public class BaseController : Controller{
        const string active = "active";
        const string open = "open";
        protected readonly AnagraficaContext db;
        public BaseController(AnagraficaContext ctx){
            db = ctx;
        }
        public override void OnActionExecuting(ActionExecutingContext context){
            var route = context.RouteData.Values;
            string childName = route["Controller"].ToString();
            string rootName;
            switch (childName.ToLower()){
                case "anagrafica":
                case "contatti":
                case "indirizzi":
                    rootName = "Anagrafiche";
                    break;
                case "tipoanagrafica":
                case "tipocontatto":
                case "tipoindirizzo":
                    rootName = "Domini";
                    break;
                default:
                    rootName = "Dashboard";
                    break;
            }
            ViewData["Home"] = rootName.Equals("Dashboard") ? active : "";
            ViewData["AnagRoot"] = rootName.Equals("Anagrafiche") ? active : "";
            ViewData["AnagOpen"] = rootName.Equals("Anagrafiche") ? open : "";
            ViewData["AnagItem"] = childName.ToLower().Equals("anagrafica") ? active : "";
            ViewData["IndiItem"] = childName.ToLower().Equals("indirizzi") ? active : "";
            ViewData["ContItem"] = childName.ToLower().Equals("contatti") ? active : "";
            ViewData["DomRoot"] = rootName.Equals("Domini") ? active : "";
            ViewData["DomOpen"] = rootName.Equals("Domini") ? open : "";
            ViewData["TpAnagItem"] = childName.ToLower().Equals("tipoanagrafica") ? active : "";
            ViewData["TpContItem"] = childName.ToLower().Equals("tipocontatto") ? active : "";
            ViewData["TpIndiItem"] = childName.ToLower().Equals("tipoindirizzo") ? active : "";
            ViewData["Root"] = rootName;
            ViewData["Child"] = AddSpacesToSentence(childName, true);
            base.OnActionExecuting(context);
        }
        string AddSpacesToSentence(string text, bool preserveAcronyms){
            if (string.IsNullOrWhiteSpace(text)){
                return string.Empty;
            }
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++){
                if (char.IsUpper(text[i])){
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) || (preserveAcronyms && char.IsUpper(text[i - 1]) && i < text.Length - 1 && !char.IsUpper(text[i + 1]))){
                        newText.Append(' ');
                    }
                }
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}