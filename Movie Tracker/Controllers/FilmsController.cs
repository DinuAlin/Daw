using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Tracker.Data;
using Movie_Tracker.Models;

namespace Movie_Tracker.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Film.Include(f => f.IdCompozitorNavigation).Include(f => f.IdScenaristNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.IdCompozitorNavigation)
                .Include(f => f.IdScenaristNavigation)
                .SingleOrDefaultAsync(m => m.IdFilm == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor");
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFilm,Gen,Durata,DataLansare,IdRegizor,IdScenarist,IdCompozitor")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor", film.IdCompozitor);
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist", film.IdScenarist);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.SingleOrDefaultAsync(m => m.IdFilm == id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor", film.IdCompozitor);
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist", film.IdScenarist);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdFilm,Gen,Durata,DataLansare,IdRegizor,IdScenarist,IdCompozitor")] Film film)
        {
            if (id != film.IdFilm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.IdFilm))
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
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor", film.IdCompozitor);
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist", film.IdScenarist);
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.IdCompozitorNavigation)
                .Include(f => f.IdScenaristNavigation)
                .SingleOrDefaultAsync(m => m.IdFilm == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var film = await _context.Film.SingleOrDefaultAsync(m => m.IdFilm == id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(long id)
        {
            return _context.Film.Any(e => e.IdFilm == id);
        }
    }
}
