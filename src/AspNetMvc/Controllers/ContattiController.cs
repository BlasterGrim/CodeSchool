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
    public class ContattiController : BaseController
    {

        public ContattiController(AnagraficaContext context) : base(context) { }

        // GET: Contatti
        public async Task<IActionResult> IndexOld()
        {
            var anagraficaContext = db.Contatti.Include(c => c.Anagrafica).Include(c => c.TipoContatto);
            return View(await anagraficaContext.ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            List<ContattiView> views = new List<ContattiView>();
            var items = await db.Contatti.Include(x => x.TipoContatto).ToListAsync();
            foreach (var item in items)
            {
                ContattiView con = new ContattiView()
                {
                    ContattiId = item.ContattiId,
                    Valore = item.Valore,
                    Note = item.Note,
                    AnagraficaId = item.AnagraficaId,
                };
                if (item.TipoContatto != null)
                {
                    con.TipoContatto = new TipoContattoView(item.TipoContatto);
                }
                views.Add(con);
            }
            return View(views);
        }

        // GET: Contatti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatti = await db.Contatti
                .Include(c => c.Anagrafica)
                .Include(c => c.TipoContatto)
                .FirstOrDefaultAsync(m => m.ContattiId == id);
            if (contatti == null)
            {
                return NotFound();
            }

            return View(contatti);
        }

        // GET: Contatti/Create
        public IActionResult Create()
        {
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId");
            ViewData["TipoContattoId"] = new SelectList(db.TipoContatto, "TipoContattoId", "TipoContattoId");
            return View();
        }

        // POST: Contatti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContattiId,Valore,Note,AnagraficaId,TipoContattoId")] Contatti contatti)
        {
            if (ModelState.IsValid)
            {
                db.Add(contatti);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId", contatti.AnagraficaId);
            ViewData["TipoContattoId"] = new SelectList(db.TipoContatto, "TipoContattoId", "TipoContattoId", contatti.TipoContattoId);
            return View(contatti);
        }

        // GET: Contatti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatti = await db.Contatti.FindAsync(id);
            if (contatti == null)
            {
                return NotFound();
            }
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId", contatti.AnagraficaId);
            ViewData["TipoContattoId"] = new SelectList(db.TipoContatto, "TipoContattoId", "TipoContattoId", contatti.TipoContattoId);
            return View(contatti);
        }

        // POST: Contatti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContattiId,Valore,Note,AnagraficaId,TipoContattoId")] Contatti contatti)
        {
            if (id != contatti.ContattiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(contatti);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContattiExists(contatti.ContattiId))
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
            ViewData["AnagraficaId"] = new SelectList(db.Anagrafica, "AnagraficaId", "AnagraficaId", contatti.AnagraficaId);
            ViewData["TipoContattoId"] = new SelectList(db.TipoContatto, "TipoContattoId", "TipoContattoId", contatti.TipoContattoId);
            return View(contatti);
        }

        // GET: Contatti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatti = await db.Contatti
                .Include(c => c.Anagrafica)
                .Include(c => c.TipoContatto)
                .FirstOrDefaultAsync(m => m.ContattiId == id);
            if (contatti == null)
            {
                return NotFound();
            }

            return View(contatti);
        }

        // POST: Contatti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contatti = await db.Contatti.FindAsync(id);
            db.Contatti.Remove(contatti);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContattiExists(int id)
        {
            return db.Contatti.Any(e => e.ContattiId == id);
        }
    }
}
