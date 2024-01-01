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
    public class DoktorController : Controller
    {
        private readonly HastaneRandevuUygulamasıContext _context;

        public DoktorController(HastaneRandevuUygulamasıContext context)
        {
            _context = context;
        }

        // GET: Doktor
        public async Task<IActionResult> Index()
        {
            var hastaneRandevuUygulamasıContext = _context.Doktor.Include(d => d.poliklinik);
            return View(await hastaneRandevuUygulamasıContext.ToListAsync());
        }

        // GET: Doktor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .Include(d => d.poliklinik)
                .FirstOrDefaultAsync(m => m.doktorId == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktor/Create
        public IActionResult Create()
        {
            ViewData["poliklinikId"] = new SelectList(_context.Set<Poliklinik>(), "poliklinikId", "poliklinikId");
            return View();
        }

        // POST: Doktor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("doktorId,doktorName,poliklinikId")] Doktor doktor)
        {
            
                _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Doktor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }
            ViewData["poliklinikId"] = new SelectList(_context.Set<Poliklinik>(), "poliklinikId", "poliklinikId", doktor.poliklinikId);
            return View(doktor);
        }

        // POST: Doktor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("doktorId,doktorName,poliklinikId")] Doktor doktor)
        {
            if (id != doktor.doktorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorExists(doktor.doktorId))
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
            ViewData["poliklinikId"] = new SelectList(_context.Set<Poliklinik>(), "poliklinikId", "poliklinikId", doktor.poliklinikId);
            return View(doktor);
        }

        // GET: Doktor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .Include(d => d.poliklinik)
                .FirstOrDefaultAsync(m => m.doktorId == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doktor == null)
            {
                return Problem("Entity set 'HastaneRandevuUygulamasıContext.Doktor'  is null.");
            }
            var doktor = await _context.Doktor.FindAsync(id);
            if (doktor != null)
            {
                _context.Doktor.Remove(doktor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(int id)
        {
          return (_context.Doktor?.Any(e => e.doktorId == id)).GetValueOrDefault();
        }
    }
}
