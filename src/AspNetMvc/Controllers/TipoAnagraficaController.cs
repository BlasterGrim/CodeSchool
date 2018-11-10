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
    public class TipoAnagraficaController : BaseController
    {

        public TipoAnagraficaController(AnagraficaContext context) :base(context){}
        // GET: TipoAnagrafica
        public async Task<IActionResult> Index()
        {
            return View(await GetConvertedListAsync());
        }

        // GET: TipoAnagrafica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAnagrafica = await db.TipoAnagrafica
                .FirstOrDefaultAsync(m => m.TipoAnagraficaId == id);
            if (tipoAnagrafica == null)
            {
                return NotFound();
            }
            var tpAnagrafica = new TipoAnagraficaView(tipoAnagrafica);
            return View(tpAnagrafica);
        }

        // GET: TipoAnagrafica/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoAnagrafica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoAnagraficaId,Descrizione")] TipoAnagrafica tipoAnagrafica)
        {
            if (ModelState.IsValid)
            {
                db.Add(tipoAnagrafica);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var tpAnagrafica = new TipoAnagraficaView(tipoAnagrafica);
            return View(tpAnagrafica);
        }

        // GET: TipoAnagrafica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAnagrafica = await db.TipoAnagrafica.FindAsync(id);
            if (tipoAnagrafica == null)
            {
                return NotFound();
            }
            var tpAnagrafica = new TipoAnagraficaView(tipoAnagrafica);
            return View(tpAnagrafica);
        }

        // POST: TipoAnagrafica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoAnagraficaId,Descrizione")] TipoAnagrafica tipoAnagrafica)
        {
            if (id != tipoAnagrafica.TipoAnagraficaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tipoAnagrafica);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAnagraficaExists(tipoAnagrafica.TipoAnagraficaId))
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
            var tpAnagrafica = new TipoAnagraficaView(tipoAnagrafica);
            return View(tpAnagrafica);
        }

        // GET: TipoAnagrafica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAnagrafica = await db.TipoAnagrafica
                .FirstOrDefaultAsync(m => m.TipoAnagraficaId == id);
            if (tipoAnagrafica == null)
            {
                return NotFound();
            }
            var tpAnagrafica = new TipoAnagraficaView(tipoAnagrafica);
            return View(tpAnagrafica);
        }

        // POST: TipoAnagrafica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoAnagrafica = await db.TipoAnagrafica.FindAsync(id);
            db.TipoAnagrafica.Remove(tipoAnagrafica);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task<List<TipoAnagraficaView>> GetConvertedListAsync(){
            List<TipoAnagraficaView> lst = new List<TipoAnagraficaView>();
            var items = await db.TipoAnagrafica.ToListAsync();
            foreach(var item in items ){
                lst.Add(new TipoAnagraficaView(item));
            }
            return lst;
        }
        private bool TipoAnagraficaExists(int id)
        {
            return db.TipoAnagrafica.Any(e => e.TipoAnagraficaId == id);
        }
    }
}
