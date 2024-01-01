using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuUygulaması.Data;
using HastaneRandevuUygulaması.Models;

namespace HastaneRandevuUygulaması.Controllers
{
    public class PoliklinikController : Controller
    {
        private readonly HastaneRandevuUygulamasıContext _context;

        public PoliklinikController(HastaneRandevuUygulamasıContext context)
        {
            _context = context;
        }

        // GET: Polikliniks
        public async Task<IActionResult> Index()
        {
            var hastaneRandevuUygulamasıContext = _context.Poliklinik.Include(p => p.hastane);
            return View(await hastaneRandevuUygulamasıContext.ToListAsync());
        }

        // GET: Polikliniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinik
                .Include(p => p.hastane)
                .FirstOrDefaultAsync(m => m.poliklinikId == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // GET: Polikliniks/Create
        public IActionResult Create()
        {
            ViewData["hastaneId"] = new SelectList(_context.Hastane, "hastaneId", "hastaneId");
            return View();
        }

        // POST: Polikliniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("poliklinikId,poliklinikName,hastaneId")] Poliklinik poliklinik)
        {
           
                _context.Add(poliklinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: Polikliniks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinik.FindAsync(id);
            if (poliklinik == null)
            {
                return NotFound();
            }
            ViewData["hastaneId"] = new SelectList(_context.Hastane, "hastaneId", "hastaneId", poliklinik.hastaneId);
            return View(poliklinik);
        }

        // POST: Polikliniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("poliklinikId,poliklinikName,hastaneId")] Poliklinik poliklinik)
        {
            if (id != poliklinik.poliklinikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poliklinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliklinikExists(poliklinik.poliklinikId))
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
            ViewData["hastaneId"] = new SelectList(_context.Hastane, "hastaneId", "hastaneId", poliklinik.hastaneId);
            return View(poliklinik);
        }

        // GET: Polikliniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinik
                .Include(p => p.hastane)
                .FirstOrDefaultAsync(m => m.poliklinikId == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // POST: Polikliniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Poliklinik == null)
            {
                return Problem("Entity set 'HastaneRandevuUygulamasıContext.Poliklinik'  is null.");
            }
            var poliklinik = await _context.Poliklinik.FindAsync(id);
            if (poliklinik != null)
            {
                _context.Poliklinik.Remove(poliklinik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliklinikExists(int id)
        {
          return (_context.Poliklinik?.Any(e => e.poliklinikId == id)).GetValueOrDefault();
        }
    }
}
