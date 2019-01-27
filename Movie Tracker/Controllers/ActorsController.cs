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
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var actors = from a in _context.Actor
                         select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                actors = actors.Where(s => s.Nume.Contains(searchString)
                                       || s.Prenume.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    actors = actors.OrderByDescending(s => s.Nume);
                    break;
                case "Date":
                    actors = actors.OrderBy(s => s.DataNastere);
                    break;
                case "date_desc":
                    actors = actors.OrderByDescending(s => s.DataNastere);
                    break;
                default:
                    actors = actors.OrderBy(s => s.Nume);
                    break;
            }
            return View(actors.ToList());
            //return View(await _context.Actor.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .SingleOrDefaultAsync(m => m.IdActor == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdActor,Nume,Prenume,DataNastere")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.SingleOrDefaultAsync(m => m.IdActor == id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdActor,Nume,Prenume,DataNastere")] Actor actor)
        {
            if (id != actor.IdActor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.IdActor))
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
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .SingleOrDefaultAsync(m => m.IdActor == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var actor = await _context.Actor.SingleOrDefaultAsync(m => m.IdActor == id);
            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(long id)
        {
            return _context.Actor.Any(e => e.IdActor == id);
        }
    }
}
