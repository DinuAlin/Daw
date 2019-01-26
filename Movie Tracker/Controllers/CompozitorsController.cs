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
    public class CompozitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompozitorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compozitors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Compozitor.ToListAsync());
        }

        // GET: Compozitors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compozitor = await _context.Compozitor
                .SingleOrDefaultAsync(m => m.IdCompozitor == id);
            if (compozitor == null)
            {
                return NotFound();
            }

            return View(compozitor);
        }

        // GET: Compozitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compozitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompozitor,Nume,Prenume,DataNastere")] Compozitor compozitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compozitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compozitor);
        }

        // GET: Compozitors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compozitor = await _context.Compozitor.SingleOrDefaultAsync(m => m.IdCompozitor == id);
            if (compozitor == null)
            {
                return NotFound();
            }
            return View(compozitor);
        }

        // POST: Compozitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCompozitor,Nume,Prenume,DataNastere")] Compozitor compozitor)
        {
            if (id != compozitor.IdCompozitor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compozitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompozitorExists(compozitor.IdCompozitor))
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
            return View(compozitor);
        }

        // GET: Compozitors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compozitor = await _context.Compozitor
                .SingleOrDefaultAsync(m => m.IdCompozitor == id);
            if (compozitor == null)
            {
                return NotFound();
            }

            return View(compozitor);
        }

        // POST: Compozitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var compozitor = await _context.Compozitor.SingleOrDefaultAsync(m => m.IdCompozitor == id);
            _context.Compozitor.Remove(compozitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompozitorExists(long id)
        {
            return _context.Compozitor.Any(e => e.IdCompozitor == id);
        }
    }
}
