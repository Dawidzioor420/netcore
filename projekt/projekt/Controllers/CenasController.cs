using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

namespace projekt.Controllers
{
    public class CenasController : Controller
    {
        private readonly projektContext _context;

        public CenasController(projektContext context)
        {
            _context = context;
        }

        // GET: Rezysers
        public async Task<IActionResult> Index()
        {
            var projektContext = _context.Cenas.Include(s => s.Films);
            return View(await projektContext.ToListAsync());
        }

        // GET: Rezysers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var Cenaa = await _context.Cenas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Cenaa == null)
            {
                return NotFound();
            }

            return View(Cenaa);
        }

        // GET: Rezysers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rezysers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Cenaa")] Cena Cenaa)

        {
            if (Cenaa is null)
            {
                throw new ArgumentNullException(nameof(Cenaa));
            }

            if (ModelState.IsValid)
            {
                _context.Add(Cenaa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Cenaa);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cenaa = await _context.Cenas.FindAsync(id);
            if (Cenaa == null)
            {
                return NotFound();
            }
            return View(Cenaa);
        }

        // POST: Rezysers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cenaa")] Cena Cenaa)
        {
            if (id != Cenaa.Id)
            {
                return NotFound();
            }

            if (Cenaa is null)
            {
                throw new ArgumentNullException(nameof(Cenaa));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Cenaa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CenaExists(Cenaa.Id))
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
            return View(Cenaa);
        }

        // GET: Rezysers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cenaa = await _context.Cenas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Cenaa == null)
            {
                return NotFound();
            }

            return View(Cenaa);
        }

        // POST: Rezysers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Cenaa = await _context.Cenas.FindAsync(id);
            _context.Cenas.Remove(Cenaa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CenaExists(int id)
        {
            return _context.Cenas.Any(e => e.Id == id);
        }
    }
}
