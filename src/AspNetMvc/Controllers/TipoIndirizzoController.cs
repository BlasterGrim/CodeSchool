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
    public class TipoIndirizzoController : BaseController
    {

        public TipoIndirizzoController(AnagraficaContext context) :base(context){}

        // GET: TipoIndirizzo
        public async Task<IActionResult> Index()
        {
            return View(await db.TipoIndirizzo.ToListAsync());
        }

        // GET: TipoIndirizzo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIndirizzo = await db.TipoIndirizzo
                .FirstOrDefaultAsync(m => m.TipoIndirizzoId == id);
            if (tipoIndirizzo == null)
            {
                return NotFound();
            }

            return View(tipoIndirizzo);
        }

        // GET: TipoIndirizzo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoIndirizzo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoIndirizzoId,Descrizione")] TipoIndirizzo tipoIndirizzo)
        {
            if (ModelState.IsValid)
            {
                db.Add(tipoIndirizzo);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoIndirizzo);
        }

        // GET: TipoIndirizzo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIndirizzo = await db.TipoIndirizzo.FindAsync(id);
            if (tipoIndirizzo == null)
            {
                return NotFound();
            }
            return View(tipoIndirizzo);
        }

        // POST: TipoIndirizzo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoIndirizzoId,Descrizione")] TipoIndirizzo tipoIndirizzo)
        {
            if (id != tipoIndirizzo.TipoIndirizzoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tipoIndirizzo);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoIndirizzoExists(tipoIndirizzo.TipoIndirizzoId))
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
            return View(tipoIndirizzo);
        }

        // GET: TipoIndirizzo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIndirizzo = await db.TipoIndirizzo
                .FirstOrDefaultAsync(m => m.TipoIndirizzoId == id);
            if (tipoIndirizzo == null)
            {
                return NotFound();
            }

            return View(tipoIndirizzo);
        }

        // POST: TipoIndirizzo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoIndirizzo = await db.TipoIndirizzo.FindAsync(id);
            db.TipoIndirizzo.Remove(tipoIndirizzo);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoIndirizzoExists(int id)
        {
            return db.TipoIndirizzo.Any(e => e.TipoIndirizzoId == id);
        }
    }
}
