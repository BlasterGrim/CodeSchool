using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Tabelle;

namespace AspNetMvc.Controllers
{
    public class TipoContattoController : BaseController
    {

        public TipoContattoController(AnagraficaContext context) :base(context){}
        // GET: TipoContatto
        public async Task<IActionResult> Index()
        {
            return View(await db.TipoContatto.ToListAsync());
        }

        // GET: TipoContatto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContatto = await db.TipoContatto
                .FirstOrDefaultAsync(m => m.TipoContattoId == id);
            if (tipoContatto == null)
            {
                return NotFound();
            }

            return View(tipoContatto);
        }

        // GET: TipoContatto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoContatto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoContattoId,Descrizione")] TipoContatto tipoContatto)
        {
            if (ModelState.IsValid)
            {
                db.Add(tipoContatto);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoContatto);
        }

        // GET: TipoContatto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContatto = await db.TipoContatto.FindAsync(id);
            if (tipoContatto == null)
            {
                return NotFound();
            }
            return View(tipoContatto);
        }

        // POST: TipoContatto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoContattoId,Descrizione")] TipoContatto tipoContatto)
        {
            if (id != tipoContatto.TipoContattoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tipoContatto);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoContattoExists(tipoContatto.TipoContattoId))
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
            return View(tipoContatto);
        }

        // GET: TipoContatto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContatto = await db.TipoContatto
                .FirstOrDefaultAsync(m => m.TipoContattoId == id);
            if (tipoContatto == null)
            {
                return NotFound();
            }

            return View(tipoContatto);
        }

        // POST: TipoContatto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoContatto = await db.TipoContatto.FindAsync(id);
            db.TipoContatto.Remove(tipoContatto);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoContattoExists(int id)
        {
            return db.TipoContatto.Any(e => e.TipoContattoId == id);
        }
    }
}
