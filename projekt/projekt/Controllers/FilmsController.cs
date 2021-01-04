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
    public class FilmsController : Controller
    {
        private readonly projektContext _context;

        public FilmsController(projektContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var projektContext = _context.Films.Include(f => f.Kategoria).Include(f => f.Cena).Include(cg=>cg.FilmAktors).ThenInclude(c=>c.Aktor);
            return View(await projektContext.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Kategoria)
                .Include(f => f.Cena)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Kategorias, "Id", "Nazwa");
            ViewData["CenaId"] = new SelectList(_context.Cenas, "Id", "Cenaa");
            GetCourseList();
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tytul,RokProdukcji,KategoriaId,CenaId,Nowy")] Film film)
        {
            if (ModelState.IsValid)
            {
                var lista = HttpContext.Request.Form["selectedCourses"];
                _context.Add(film);
                await _context.SaveChangesAsync();
                foreach(var l in lista)
                {
                    var cg = new FilmAktor();
                    cg.AktorId = int.Parse(l);
                    cg.FilmId = film.Id;
                    _context.Add(cg);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorias, "Id", "Nazwa", film.KategoriaId);
            ViewData["CenaId"] = new SelectList(_context.Cenas, "Id", "Cena", film.CenaId);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorias, "Id", "Nazwa", film.KategoriaId);
            ViewData["CenaId"] = new SelectList(_context.Cenas, "Id", "Cena", film.CenaId);
            GetSelectedCourseList(film);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,RokProdukcji,KategoriaId,CenaId,Nowy")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach(var cgold in _context.FilmAktors.Where(cg=>cg.FilmId==film.Id))
                    {
                        _context.Remove(cgold);
                    }
                    var lista = HttpContext.Request.Form["selectedCourses"];
                    foreach (var l in lista)
                    {
                        var cg = new FilmAktor();
                        cg.AktorId = int.Parse(l);
                        cg.FilmId = film.Id;
                        _context.Add(cg);
                        await _context.SaveChangesAsync();
                    }
                     _context.Update(film);
                  
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
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
            ViewData["KategoriaId"] = new SelectList(_context.Kategorias, "Id", "Nazwa", film.KategoriaId);
            ViewData["CenaId"] = new SelectList(_context.Cenas, "Id", "Cena", film.CenaId);
            return View(film);
        }
        public async Task<IActionResult>Grade(int? cid, int? gid)   
        {
            var Slist = _context.Aktor.Where(s => s.Id == cid);
            var xaktor = await _context.Aktor.FindAsync(cid);
            var xfilm = await _context.Films.FindAsync(gid);
            var GListt = _context.Grades.Where(g => g.FilmId == gid).Where(g => g.AktorId == cid).Count();
            var GsList = new List<GS>();
            foreach(Aktor s in Slist)
            {
                var gs = GListt;
                var xocena = 0;
                if (gs > 0)
                {
                    var GList = _context.Grades.Where(g => g.FilmId == gid).Where(g => g.AktorId == cid).ToList().First();
                    xocena =GList.Ocena;
                }
                GsList.Add(new GS
                {
                    AktorId = s.Id,
                    ImieNazwisko = s.ImieNazwisko,
                    Ocena = xocena
                }
                    );
            }
            ViewData["grades"] = GsList;
            ViewData["course"] = xaktor;
            ViewData["group"] = xfilm;
            return View(xfilm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Grade()
        {
            int cid = int.Parse(HttpContext.Request.Form["cid"]);
            int gid = int.Parse(HttpContext.Request.Form["gid"]);
            var Slist = _context.Aktor.Where(s => s.Id == cid);
            var GList = _context.Grades.Where(g => g.FilmId == gid).Where(g => g.AktorId == cid);
            foreach(Grade g in GList)
            {
                _context.Remove(g);
            }
            foreach (Aktor s in Slist)
            {
                int xgr = int.Parse(HttpContext.Request.Form[s.Id.ToString()]);
                var g = new Grade();
                g.AktorId = s.Id;
                g.FilmId = gid;
                g.Ocena = xgr;
                _context.Add(g);
                
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Kategoria)
                .Include(f => f.Cena)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }
        private void GetCourseList()
        {
            var allcourses = _context.Aktor;
            var Kursy = new List<C>();
            foreach (var c in allcourses)
            {
                Kursy.Add(new C
                {
                    CourseId = c.Id,
                    Nazwa=c.Imie+" "+c.Nazwisko,
                    Checked =""
                });
                
            }
            ViewData["courses"] = Kursy;
        }
        private void GetSelectedCourseList(Film film)
        {
            var allcourses = _context.Aktor;
            var selectcourses = _context.FilmAktors.Where(cg => cg.FilmId == film.Id).ToList();
            var Kursy = new List<C>();
            foreach (var c in allcourses)
            {
                Kursy.Add(new C
                {
                    CourseId = c.Id,
                    Nazwa = c.Imie + " " + c.Nazwisko,
                    Checked = ""
                });

            }
            foreach(var k in Kursy)
            {
                if(selectcourses.Exists(cg=>cg.AktorId==k.CourseId))
                {
                    k.Checked = "checked";
                }
            }
            ViewData["courses"] = Kursy;
        }
    }
}
