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
    public class AktorsController : Controller
    {
        private readonly projektContext _context;

        public AktorsController(projektContext context)
        {
            _context = context;
        }

        // GET: Aktors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aktor.ToListAsync());
        }

        // GET: Aktors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktor = await _context.Aktor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktor == null)
            {
                return NotFound();
            }

            return View(aktor);
        }

        // GET: Aktors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aktors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko")] Aktor aktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aktor);
        }

        // GET: Aktors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktor = await _context.Aktor.FindAsync(id);
            if (aktor == null)
            {
                return NotFound();
            }
            return View(aktor);
        }

        // POST: Aktors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko")] Aktor aktor)
        {
            if (id != aktor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktorExists(aktor.Id))
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
            return View(aktor);
        }

        // GET: Aktors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktor = await _context.Aktor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktor == null)
            {
                return NotFound();
            }

            return View(aktor);
        }

        // POST: Aktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktor = await _context.Aktor.FindAsync(id);
            _context.Aktor.Remove(aktor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktorExists(int id)
        {
            return _context.Aktor.Any(e => e.Id == id);
        }
    }
}
