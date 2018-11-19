using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Tabelle;
using Models.Views;

namespace AspNetMvc.Controllers
{
    public class AnagraficaController : BaseController
    {
        private readonly AnagraficaContext _context;
        public AnagraficaController(AnagraficaContext context) : base(context)
        {
            _context = context;
        }
        // GET: Anagrafica
        public async Task<IActionResult> IndexOld()
        {
            var anagraficaContext = db.Anagrafica.Include(a => a.TipoAnagrafica);
            return View(await anagraficaContext.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            List<AnagraficaView> views = new List<AnagraficaView>();
            var items = await db.Anagrafica.Include(x => x.TipoAnagrafica).ToListAsync();
            foreach (var item in items)
            {
                AnagraficaView anag = new AnagraficaView
                {
                    AnagraficaId = item.AnagraficaId,
                    CodiceAnagrafica = item.CodiceAnagrafica,
                    IsAzienda = item.IsAzienda,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    RagioneSociale = item.RagioneSociale,
                    PartitaIva = item.PartitaIva,
                    CodiceFiscale = item.CodiceFiscale,
                    TipoAnagraficaId = item.TipoAnagraficaId,
                };
                if (item.TipoAnagrafica != null)
                {
                    anag.TipoAnagrafica = new TipoAnagraficaView(item.TipoAnagrafica);
                }
                var contatti = await db.Contatti.Include(x => x.TipoContatto).Where(x => x.AnagraficaId == item.AnagraficaId).ToListAsync();
                if (contatti != null && contatti.Count > 0)
                {
                    anag.Contatti = new List<ContattiView>();
                    foreach (var contatto in contatti)
                    {
                        ContattiView con = new ContattiView()
                        {
                            ContattiId = contatto.ContattiId,
                            Valore = contatto.Valore,
                            Note = contatto.Note,
                            AnagraficaId = anag.AnagraficaId,
                        };
                        if (contatto.TipoContatto != null)
                        {
                            TipoContattoView tpCont = new TipoContattoView
                            {
                                TipoContattoId = contatto.TipoContatto.TipoContattoId,
                                Descrizione = contatto.TipoContatto.Descrizione
                            };
                            con.TipoContatto = tpCont;
                            con.TipoContattoId = contatto.TipoContattoId;
                        }
                        anag.Contatti.Add(con);
                    }
                }
                var indirizzi = await db.Indirizzi.Include(x => x.TipoIndirizzo).Where(x => x.AnagraficaId == item.AnagraficaId).ToListAsync();
                if (indirizzi != null && indirizzi.Count > 0)
                {
                    anag.Indirizzi = new List<IndirizziView>();
                    foreach (var indirizzo in indirizzi)
                    {
                        IndirizziView ind = new IndirizziView()
                        {
                            IndirizziId = indirizzo.IndirizziId,
                            Nazione = indirizzo.Nazione,
                            Regione = indirizzo.Regione,
                            Provincia = indirizzo.Provincia,
                            Citta = indirizzo.Citta,
                            Denominazione = indirizzo.Denominazione,
                            Cap = indirizzo.Cap,
                            Numero = indirizzo.Numero,
                            AnagraficaId = anag.AnagraficaId,
                        };
                        if (indirizzo.TipoIndirizzo != null)
                        {
                            TipoIndirizzoView tpInd = new TipoIndirizzoView
                            {
                                TipoIndirizzoId = indirizzo.TipoIndirizzo.TipoIndirizzoId,
                                Descrizione = indirizzo.TipoIndirizzo.Descrizione
                            };
                            ind.TipoIndirizzo = tpInd;
                            ind.TipoIndirizzoId = indirizzo.TipoIndirizzoId;
                        }
                        anag.Indirizzi.Add(ind);
                    }
                }
                views.Add(anag);
            }
            return View(views);
        }

        // GET: Anagrafica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anagrafica = await db.Anagrafica
                .Include(a => a.TipoAnagrafica)
                .FirstOrDefaultAsync(m => m.AnagraficaId == id);
            AnagraficaView anag = new AnagraficaView()
            {
                AnagraficaId = anagrafica.AnagraficaId,
                CodiceAnagrafica = anagrafica.CodiceAnagrafica,
                IsAzienda = anagrafica.IsAzienda,
                Nome = anagrafica.Nome,
                Cognome = anagrafica.Cognome,
                RagioneSociale = anagrafica.RagioneSociale,
                PartitaIva = anagrafica.PartitaIva,
                CodiceFiscale = anagrafica.CodiceFiscale,
                TipoAnagraficaId = anagrafica.TipoAnagraficaId,
            };
            if (anagrafica.TipoAnagrafica != null)
            {
                anag.TipoAnagrafica = new TipoAnagraficaView(anagrafica.TipoAnagrafica);
            }

            if (anag == null)
            {
                return NotFound();
            }
            return View(anag);
        }

        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //List<AnagraficaView> views = new List<AnagraficaView>();
            var items = await db.Anagrafica.Include(x => x.TipoAnagrafica).FirstOrDefaultAsync();
            if (items == null)
            {
                return NotFound();
            }
            //views.Add(items);
            return View(items);
        }*/

        // GET: Anagrafica/Create
        public IActionResult Create()
        {
            ViewData["TipoAnagraficaId"] = new SelectList(db.TipoAnagrafica, "TipoAnagraficaId", "Descrizione");
            ViewData["TipoIndirizzoId"] = new SelectList(db.TipoIndirizzo, "TipoIndirizzoId", "Descrizione");
            ViewData["TipoContattoId"] = new SelectList(db.TipoContatto, "TipoContattoId", "Descrizione");
            return View();
        }

        // POST: Anagrafica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnagraficaId,CodiceAnagrafica,IsAzienda,Nome,Cognome,RagioneSociale,PartitaIva,CodiceFiscale,TipoAnagraficaId")] AnagraficaView view, [Bind("ContattiId,Valore,Note,AnagraficaId,TipoContattoId")] ContattiView viewC, [Bind("IndirizziId,Nazione,Regione,Provincia,Citta,Denominazione,Cap,Numero,AnagraficaId,TipoIndirizzoId")] IndirizziView viewI)
        {
            viewC.AnagraficaId = view.AnagraficaId;
            viewI.AnagraficaId = view.AnagraficaId;
            //if (ModelState.IsValid)
            {
                //AnagraficaView BCVM = new AnagraficaView();
                //view.Contatti = GetContatti(viewC);
                //view.Indirizzi = GetIndirizzi(viewI);

                var item = ConvertFromView(view);
                var item1 = ConvertFromView2(viewC);
                var item2 = ConvertFromView3(viewI);
                //var item1 = ConvertFromView2(viewC);
                //var item2 = ConvertFromView3(viewI);
                //db.Add(cnt);
                /*db.Add(item1);
                db.Add(item2);*/
                _context.Add(item);
                await _context.SaveChangesAsync();
                item1.AnagraficaId = item.AnagraficaId;
                _context.Add(item1);
                await _context.SaveChangesAsync();
                item2.AnagraficaId = item.AnagraficaId;
                _context.Add(item2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["TipoAnagraficaId"] = new SelectList(db.TipoAnagrafica, "TipoAnagraficaId", "Descrizione", view.TipoAnagraficaId);
            //ViewData["TipoContattoId"] = new SelectList(db.TipoContatto, "TipoContattoId", "Descrizione", viewC.TipoContattoId);
            //ViewData["TipoIndirizzoId"] = new SelectList(db.TipoIndirizzo, "TipoIndirizzoId", "Descrizione", viewI.TipoIndirizzoId);
            //var newView = new AnagraficaView(item1);
            //return View(view);
        }

        /*public async Task<IActionResult> CreateOld([Bind("")]AnagraficaView anagraficaView)
        {
            var items = await db.Anagrafica.Include(x => x.TipoAnagrafica).ToListAsync();
            List<AnagraficaView> lst = new List<AnagraficaView>();
            AnagraficaView anag = new AnagraficaView
            {
                AnagraficaId = item.AnagraficaId,
                CodiceAnagrafica = item.CodiceAnagrafica,
                IsAzienda = item.IsAzienda,
                Nome = item.Nome,
                Cognome = item.Cognome,
                RagioneSociale = item.RagioneSociale,
                PartitaIva = item.PartitaIva,
                CodiceFiscale = item.CodiceFiscale,
                TipoAnagraficaId = item.TipoAnagraficaId
            };
            foreach (var item in items)
            {
                AnagraficaId = item.AnagraficaId,
                CodiceAnagrafica = item.CodiceAnagrafica,
                IsAzienda = item.IsAzienda,
                Nome = item.Nome,
                Cognome = item.Cognome,
                RagioneSociale = item.RagioneSociale,
                PartitaIva = item.PartitaIva,
                CodiceFiscale = item.CodiceFiscale,
                TipoAnagraficaId = item.TipoAnagraficaId

            }
            if (ModelState.IsValid)
            {

            }
            ViewData["Contatti"] = new SelectList(lst);
        }*/

        // GET: Anagrafica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anagrafica = await db.Anagrafica.FindAsync(id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            ViewData["TipoAnagraficaId"] = new SelectList(db.TipoAnagrafica, "TipoAnagraficaId", "Descrizione", anagrafica.TipoAnagraficaId);
            return View(anagrafica);
        }

        // POST: Anagrafica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnagraficaId,CodiceAnagrafica,IsAzienda,Nome,Cognome,RagioneSociale,PartitaIva,CodiceFiscale,TipoAnagraficaId")] Anagrafica anagrafica)
        {
            if (id != anagrafica.AnagraficaId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(anagrafica);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnagraficaExists(anagrafica.AnagraficaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoAnagraficaId"] = new SelectList(db.TipoAnagrafica, "TipoAnagraficaId", "Descrizione", anagrafica.TipoAnagraficaId);
            return View(anagrafica);
        }

        // GET: Anagrafica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anagrafica = await db.Anagrafica
                .Include(a => a.TipoAnagrafica)
                .FirstOrDefaultAsync(m => m.AnagraficaId == id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            return View(anagrafica);
        }
        // POST: Anagrafica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anagrafica = await db.Anagrafica.FindAsync(id);
            db.Anagrafica.Remove(anagrafica);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task<List<TipoAnagraficaView>> GetConvertedListAsync()
        {
            List<TipoAnagraficaView> lst = new List<TipoAnagraficaView>();
            var items = await db.TipoAnagrafica.ToListAsync();
            foreach (var item in items)
            {
                lst.Add(new TipoAnagraficaView(item));
            }
            return lst;
        }
        private bool AnagraficaExists(int id)
        {
            return db.Anagrafica.Any(e => e.AnagraficaId == id);
        }
        private Anagrafica ConvertFromView(AnagraficaView tbl)
        {
            Anagrafica t = new Anagrafica();
            t.AnagraficaId = tbl.AnagraficaId;
            t.CodiceAnagrafica = tbl.CodiceAnagrafica;
            t.IsAzienda = tbl.IsAzienda;
            t.Nome = tbl.Nome;
            t.Cognome = tbl.Cognome;
            t.RagioneSociale = tbl.RagioneSociale;
            t.PartitaIva = tbl.PartitaIva;
            t.CodiceFiscale = tbl.CodiceFiscale;
            t.TipoAnagraficaId = tbl.TipoAnagraficaId;
            return t;
        }
        private Contatti ConvertFromView2(ContattiView tbl)
        {
            Contatti t = new Contatti();
            t.Valore = tbl.Valore;
            t.Note = tbl.Note;
            t.AnagraficaId = tbl.AnagraficaId;
            t.TipoContattoId = tbl.TipoContattoId;
            return t;
        }
        private Indirizzi ConvertFromView3(IndirizziView tbl)
        {
            Indirizzi t = new Indirizzi();
            t.IndirizziId = tbl.IndirizziId;
            t.Nazione = tbl.Nazione;
            t.Regione = tbl.Regione;
            t.Provincia = tbl.Provincia;
            t.Citta = tbl.Citta;
            t.Provincia = tbl.Provincia;
            t.Citta = tbl.Citta;
            t.Denominazione = tbl.Denominazione;
            t.Cap = tbl.Cap;
            t.Numero = tbl.Numero;
            t.AnagraficaId = tbl.AnagraficaId;
            t.TipoIndirizzoId = tbl.TipoIndirizzoId;
            return t;
        }

        public ICollection<ContattiView> GetContatti(ContattiView contatti)
        {
            List<ContattiView> bModel = new List<ContattiView>();
            if (ModelState.IsValid)
            {

            }
            return bModel;
        }

        public ICollection<IndirizziView> GetIndirizzi(IndirizziView indirizzi)
        {
            List<IndirizziView> cModel = new List<IndirizziView>();

            cModel.Add(new IndirizziView()
            {
                IndirizziId = 1,
                Nazione = "Ialia",
                Regione = "Good One",
                Provincia = "Vijay",
            });
            return cModel;
        }

    }
}