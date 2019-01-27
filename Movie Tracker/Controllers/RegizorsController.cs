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
    public class RegizorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegizorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Regizors
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var regizori = from a in _context.Regizor
                         select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                regizori = regizori.Where(s => s.Nume.Contains(searchString)
                                       || s.Prenume.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    regizori = regizori.OrderByDescending(s => s.Nume);
                    break;
                case "Date":
                    regizori = regizori.OrderBy(s => s.DataNastere);
                    break;
                case "date_desc":
                    regizori = regizori.OrderByDescending(s => s.DataNastere);
                    break;
                default:
                    regizori = regizori.OrderBy(s => s.Nume);
                    break;
            }
            return View(regizori.ToList());
        }

        // GET: Regizors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regizor = await _context.Regizor
                .SingleOrDefaultAsync(m => m.Id == id);
            if (regizor == null)
            {
                return NotFound();
            }

            return View(regizor);
        }

        // GET: Regizors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regizors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Prenume,DataNastere")] Regizor regizor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regizor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regizor);
        }

        // GET: Regizors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regizor = await _context.Regizor.SingleOrDefaultAsync(m => m.Id == id);
            if (regizor == null)
            {
                return NotFound();
            }
            return View(regizor);
        }

        // POST: Regizors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nume,Prenume,DataNastere")] Regizor regizor)
        {
            if (id != regizor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regizor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegizorExists(regizor.Id))
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
            return View(regizor);
        }

        // GET: Regizors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regizor = await _context.Regizor
                .SingleOrDefaultAsync(m => m.Id == id);
            if (regizor == null)
            {
                return NotFound();
            }

            return View(regizor);
        }

        // POST: Regizors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var regizor = await _context.Regizor.SingleOrDefaultAsync(m => m.Id == id);
            _context.Regizor.Remove(regizor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegizorExists(long id)
        {
            return _context.Regizor.Any(e => e.Id == id);
        }
    }
}
