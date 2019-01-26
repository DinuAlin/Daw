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
    public class ScenaristsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScenaristsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Scenarists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Scenarist.ToListAsync());
        }

        // GET: Scenarists/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scenarist = await _context.Scenarist
                .SingleOrDefaultAsync(m => m.IdScenarist == id);
            if (scenarist == null)
            {
                return NotFound();
            }

            return View(scenarist);
        }

        // GET: Scenarists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scenarists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdScenarist,Nume,Prenume,DataNastere")] Scenarist scenarist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scenarist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scenarist);
        }

        // GET: Scenarists/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scenarist = await _context.Scenarist.SingleOrDefaultAsync(m => m.IdScenarist == id);
            if (scenarist == null)
            {
                return NotFound();
            }
            return View(scenarist);
        }

        // POST: Scenarists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdScenarist,Nume,Prenume,DataNastere")] Scenarist scenarist)
        {
            if (id != scenarist.IdScenarist)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scenarist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScenaristExists(scenarist.IdScenarist))
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
            return View(scenarist);
        }

        // GET: Scenarists/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scenarist = await _context.Scenarist
                .SingleOrDefaultAsync(m => m.IdScenarist == id);
            if (scenarist == null)
            {
                return NotFound();
            }

            return View(scenarist);
        }

        // POST: Scenarists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var scenarist = await _context.Scenarist.SingleOrDefaultAsync(m => m.IdScenarist == id);
            _context.Scenarist.Remove(scenarist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScenaristExists(long id)
        {
            return _context.Scenarist.Any(e => e.IdScenarist == id);
        }
    }
}
