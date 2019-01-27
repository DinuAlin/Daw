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
    public class ProducatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProducatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Producators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Producator.ToListAsync());
        }

        // GET: Producators/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producator = await _context.Producator
                .SingleOrDefaultAsync(m => m.IdProducator == id);
            if (producator == null)
            {
                return NotFound();
            }

            return View(producator);
        }

        // GET: Producators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducator,Nume,Prenume,DataNastere")] Producator producator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producator);
        }

        // GET: Producators/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producator = await _context.Producator.SingleOrDefaultAsync(m => m.IdProducator == id);
            if (producator == null)
            {
                return NotFound();
            }
            return View(producator);
        }

        // POST: Producators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdProducator,Nume,Prenume,DataNastere")] Producator producator)
        {
            if (id != producator.IdProducator)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducatorExists(producator.IdProducator))
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
            return View(producator);
        }

        // GET: Producators/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producator = await _context.Producator
                .SingleOrDefaultAsync(m => m.IdProducator == id);
            if (producator == null)
            {
                return NotFound();
            }

            return View(producator);
        }

        // POST: Producators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var producator = await _context.Producator.SingleOrDefaultAsync(m => m.IdProducator == id);
            _context.Producator.Remove(producator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducatorExists(long id)
        {
            return _context.Producator.Any(e => e.IdProducator == id);
        }
    }
}
