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
    public class IndirizziController : BaseController
    {

        public IndirizziController(AnagraficaContext context) : base(context) { }

        // GET: Indirizzi
        public async Task<IActionResult> IndexOld()
        {
            var anagraficaContext = db.Indirizzi.Include(i => i.Anagrafica).Include(i => i.TipoIndirizzo);
            return View(await anagraficaContext.ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            List<IndirizziView> views = new List<IndirizziView>();
            var items = await db.Indirizzi.Include(x => x.TipoIndirizzo).ToListAsync();
            foreach (var item in items)
            {
                IndirizziView ind = new IndirizziView()
                {
                    IndirizziId = item.IndirizziId,
                    Nazione = item.Nazione,
                    Regione = item.Regione,
                    Provincia = item.Provincia,
                    Citta = item.Citta,
                    Denominazione = item.Denominazione,
                    Cap = item.Cap,
                    Numero = item.Numero,
                    AnagraficaId = item.AnagraficaId,
                    TipoIndirizzoId = item.TipoIndirizzoId,
                };
                if (item.TipoIndirizzo != null)
                {
                    ind.TipoIndirizzo = new TipoIndirizzoView(item.TipoIndirizzo);
                }
                views.Add(ind);
            }
            return View(views);
        }

        // GET: Indirizzi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirizzi = await db.Indirizzi
                .Include(i => i.Anagrafica)
                .Include(i => i.TipoIndirizzo)
                .FirstOrDefaultAsync(m => m.IndirizziId == id);
            if (indirizzi == null)
            {
                return NotFound();
            }

            return View(indirizzi);
        }

        // GET: Indirizzi/Create
        public IActionResult Create()
        {
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId");
            ViewData["TipoIndirizzoId"] = new SelectList(db.TipoIndirizzo, "TipoIndirizzoId", "TipoIndirizzoId");
            return View();
        }

        // POST: Indirizzi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IndirizziId,Nazione,Regione,Provincia,Citta,Denominazione,Cap,Numero,AnagraficaId,TipoIndirizzoId")] Indirizzi indirizzi)
        {
            if (ModelState.IsValid)
            {
                db.Add(indirizzi);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId", indirizzi.AnagraficaId);
            ViewData["TipoIndirizzoId"] = new SelectList(db.TipoIndirizzo, "TipoIndirizzoId", "TipoIndirizzoId", indirizzi.TipoIndirizzoId);
            return View(indirizzi);
        }

        // GET: Indirizzi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirizzi = await db.Indirizzi.FindAsync(id);
            if (indirizzi == null)
            {
                return NotFound();
            }
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId", indirizzi.AnagraficaId);
            ViewData["TipoIndirizzoId"] = new SelectList(db.TipoIndirizzo, "TipoIndirizzoId", "TipoIndirizzoId", indirizzi.TipoIndirizzoId);
            return View(indirizzi);
        }

        // POST: Indirizzi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IndirizziId,Nazione,Regione,Provincia,Citta,Denominazione,Cap,Numero,AnagraficaId,TipoIndirizzoId")] Indirizzi indirizzi)
        {
            if (id != indirizzi.IndirizziId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(indirizzi);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndirizziExists(indirizzi.IndirizziId))
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
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId", indirizzi.AnagraficaId);
            ViewData["TipoIndirizzoId"] = new SelectList(db.TipoIndirizzo, "TipoIndirizzoId", "TipoIndirizzoId", indirizzi.TipoIndirizzoId);
            return View(indirizzi);
        }

        // GET: Indirizzi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirizzi = await db.Indirizzi
                .Include(i => i.Anagrafica)
                .Include(i => i.TipoIndirizzo)
                .FirstOrDefaultAsync(m => m.IndirizziId == id);
            if (indirizzi == null)
            {
                return NotFound();
            }

            return View(indirizzi);
        }

        // POST: Indirizzi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var indirizzi = await db.Indirizzi.FindAsync(id);
            db.Indirizzi.Remove(indirizzi);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndirizziExists(int id)
        {
            return db.Indirizzi.Any(e => e.IndirizziId == id);
        }
    }
}
