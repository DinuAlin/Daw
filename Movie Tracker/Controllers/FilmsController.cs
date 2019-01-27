using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var applicationDbContext = _context.Film.Include(f => f.IdRegizorNavigation).Include(f => f.IdCompozitorNavigation).Include(f => f.IdScenaristNavigation);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.GenSortParm = sortOrder == "Gen" ? "gen_desc" : "Gen";
            var films = from a in _context.Film
                        select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                films = films.Where(s => s.Nume.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    films = films.OrderByDescending(s => s.Nume);
                    break;
                case "Date":
                    films = films.OrderBy(s => s.DataLansare);
                    break;
                case "date_desc":
                    films = films.OrderByDescending(s => s.DataLansare);
                    break;
                case "Gen":
                    films = films.OrderBy(s => s.Gen);
                    break;
                case "gen_desc":
                    films = films.OrderByDescending(s => s.Gen);
                    break;
                default:
                    films = films.OrderBy(s => s.Nume);
                    break;
            }
            return View(films.ToList());
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.IdRegizorNavigation)
                .Include(f => f.IdCompozitorNavigation)
                .Include(f => f.IdScenaristNavigation)
                .SingleOrDefaultAsync(m => m.IdFilm == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        [Authorize(Roles = "Admin")]
        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["IdRegizor"] = new SelectList(_context.Regizor, "IdRegizor", "IdRegizor");
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor");
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFilm,Nume,Gen,Durata,DataLansare,IdRegizor,IdScenarist,IdCompozitor")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRegizor"] = new SelectList(_context.Regizor, "IdRegizor", "IdRegizor", film.IdRegizor);
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor", film.IdCompozitor);
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist", film.IdScenarist);
            return View(film);
        }

        [Authorize(Roles = "Admin")]
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
            ViewData["IdRegizor"] = new SelectList(_context.Regizor, "IdRegizor", "IdRegizor", film.IdRegizor);
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor", film.IdCompozitor);
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist", film.IdScenarist);
            return View(film);
        }

        [Authorize(Roles = "Admin")]
        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdFilm,Nume,Gen,Durata,DataLansare,IdRegizor,IdScenarist,IdCompozitor")] Film film)
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
            ViewData["IdRegizor"] = new SelectList(_context.Regizor, "IdRegizor", "IdRegizor", film.IdRegizor);
            ViewData["IdCompozitor"] = new SelectList(_context.Compozitor, "IdCompozitor", "IdCompozitor", film.IdCompozitor);
            ViewData["IdScenarist"] = new SelectList(_context.Scenarist, "IdScenarist", "IdScenarist", film.IdScenarist);
            return View(film);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
