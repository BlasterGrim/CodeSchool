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
        private readonly AnagraficaContext _context;
        public TipoAnagraficaController(AnagraficaContext context) : base(context)
        {
            _context = context;
        }
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
            var item = await db.TipoAnagrafica.FirstOrDefaultAsync(m => m.TipoAnagraficaId == id);
            if (item == null)
            {
                return NotFound();
            }
            var tpAnagrafica = new TipoAnagraficaView(item);
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
        public async Task<IActionResult> Create([Bind("TipoAnagraficaId,Descrizione")] TipoAnagraficaView view)
        {
            var item = ConvertFromView(view);
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var newView = new TipoAnagraficaView(item);
            return View(newView);
        }
        // GET: TipoAnagrafica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await db.TipoAnagrafica.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var view = new TipoAnagraficaView(item);
            return View(view);
        }
        // POST: TipoAnagrafica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoAnagraficaId,Descrizione")] TipoAnagraficaView view)
        {
            if (id != view.TipoAnagraficaId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var item = ConvertFromView(view);
                    db.Update(item);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAnagraficaExists(view.TipoAnagraficaId))
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
            return View(view);
        }
        // GET: TipoAnagrafica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await db.TipoAnagrafica.FirstOrDefaultAsync(m => m.TipoAnagraficaId == id);
            if (item == null)
            {
                return NotFound();
            }
            var view = new TipoAnagraficaView(item);
            return View(view);
        }
        // POST: TipoAnagrafica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await db.TipoAnagrafica.FindAsync(id);
            db.TipoAnagrafica.Remove(item);
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
        private bool TipoAnagraficaExists(int id)
        {
            return db.TipoAnagrafica.Any(e => e.TipoAnagraficaId == id);
        }
        private TipoAnagrafica ConvertFromView(TipoAnagraficaView v)
        {
            TipoAnagrafica t = new TipoAnagrafica();
            t.TipoAnagraficaId = v.TipoAnagraficaId;
            t.Descrizione = v.Descrizione;
            return t;
        }
    }
}