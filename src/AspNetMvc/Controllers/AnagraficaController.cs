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

namespace AspNetMvc.Controllers {
    public class AnagraficaController : BaseController {
        public AnagraficaController(AnagraficaContext context):base(context){}
        // GET: Anagrafica
        public async Task<IActionResult> IndexOld() {
            var anagraficaContext = db.Anagrafica.Include(a => a.TipoAnagrafica);
            return View(await anagraficaContext.ToListAsync());
        }
        public async Task<IActionResult> Index (){
            List<AnagraficaView> views  = new List<AnagraficaView>(); 
            var items = await db.Anagrafica.Include(x => x.TipoAnagrafica).ToListAsync();
            foreach (var item in items) {
                AnagraficaView anag = new AnagraficaView {
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
                if (item.TipoAnagrafica != null){
                    anag.TipoAnagrafica = new TipoAnagraficaView(item.TipoAnagrafica);
                }
                var contatti = await db.Contatti.Include(x => x.TipoContatto).Where(x=> x.AnagraficaId == item.AnagraficaId).ToListAsync();
                if(contatti!=null && contatti.Count>0){
                    anag.Contatti = new List<ContattiView>();
                    foreach (var contatto in contatti) {
                        ContattiView con = new ContattiView(){
                            ContattiId = contatto.ContattiId,
                            Valore = contatto.Valore,
                            Note = contatto.Note,
                            AnagraficaId = anag.AnagraficaId
                        };
                        if(contatto.TipoContatto!=null){
                            TipoContattoView tpCont = new TipoContattoView{
                                TipoContattoId = contatto.TipoContatto.TipoContattoId,
                                Descrizione = contatto.TipoContatto.Descrizione
                            };
                            con.TipoContatto = tpCont;
                            con.TipoContattoId = contatto.TipoContattoId;
                        }
                        anag.Contatti.Add(con);                     
                    }
                }
                var indirizzi = await db.Indirizzi.Include(x => x.TipoIndirizzo).Where(x=> x.AnagraficaId == item.AnagraficaId).ToListAsync();

                views.Add(anag);
            }    
            return View(views);
        }

        // GET: Anagrafica/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            var anagrafica = await db.Anagrafica
                .Include(a => a.TipoAnagrafica)
                .FirstOrDefaultAsync(m => m.AnagraficaId == id);
            if (anagrafica == null){
                return NotFound();
            }
            return View(anagrafica);
        }
        // GET: Anagrafica/Create
        public IActionResult Create() {
            ViewData["TipoAnagraficaId"] = new SelectList(db.TipoAnagrafica, "TipoAnagraficaId", "Descrizione");
            return View();
        }
        // POST: Anagrafica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnagraficaId,CodiceAnagrafica,IsAzienda,Nome,Cognome,RagioneSociale,PartitaIva,CodiceFiscale,TipoAnagraficaId")] Anagrafica anagrafica) {
            if (ModelState.IsValid) {
                db.Add(anagrafica);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoAnagraficaId"] = new SelectList(db.TipoAnagrafica, "TipoAnagraficaId", "Descrizione", anagrafica.TipoAnagraficaId);
            return View(anagrafica);
        }

        // GET: Anagrafica/Edit/5
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }
            var anagrafica = await db.Anagrafica.FindAsync(id);
            if (anagrafica == null){
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
        public async Task<IActionResult> Edit(int id, [Bind("AnagraficaId,CodiceAnagrafica,IsAzienda,Nome,Cognome,RagioneSociale,PartitaIva,CodiceFiscale,TipoAnagraficaId")] Anagrafica anagrafica){
            if (id != anagrafica.AnagraficaId){
                return NotFound();
            }
            if (ModelState.IsValid){
                try{
                    db.Update(anagrafica);
                    await db.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!AnagraficaExists(anagrafica.AnagraficaId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoAnagraficaId"] = new SelectList(db.TipoAnagrafica, "TipoAnagraficaId", "Descrizione", anagrafica.TipoAnagraficaId);
            return View(anagrafica);
        }

        // GET: Anagrafica/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }
            var anagrafica = await db.Anagrafica
                .Include(a => a.TipoAnagrafica)
                .FirstOrDefaultAsync(m => m.AnagraficaId == id);
            if (anagrafica == null){
                return NotFound();
            }
            return View(anagrafica);
        }
        // POST: Anagrafica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id){
            var anagrafica = await db.Anagrafica.FindAsync(id);
            db.Anagrafica.Remove(anagrafica);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AnagraficaExists(int id){
            return db.Anagrafica.Any(e => e.AnagraficaId == id);
        }
    }
}