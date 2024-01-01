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
    public class HastaneController : Controller
    {
        private readonly HastaneRandevuUygulamasıContext _context;

        public HastaneController(HastaneRandevuUygulamasıContext context)
        {
            _context = context;
        }

        // GET: Hastane
        public async Task<IActionResult> Index()
        {
            var hastaneRandevuUygulamasıContext = _context.Hastane.Include(h => h.ilce);
            return View(await hastaneRandevuUygulamasıContext.ToListAsync());
        }

        // GET: Hastane/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hastane == null)
            {
                return NotFound();
            }

            var hastane = await _context.Hastane
                .Include(h => h.ilce)
                .FirstOrDefaultAsync(m => m.hastaneId == id);
            if (hastane == null)
            {
                return NotFound();
            }

            return View(hastane);
        }

        // GET: Hastane/Create
        public IActionResult Create()
        {
            ViewData["ilceId"] = new SelectList(_context.Set<ilce>(), "ilceID", "ilceID");
            return View();
        }

        // POST: Hastane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("hastaneId,hastaneAdi,ilceId")] Hastane hastane)
        {
              _context.Add(hastane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Hastane/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hastane == null)
            {
                return NotFound();
            }

            var hastane = await _context.Hastane.FindAsync(id);
            if (hastane == null)
            {
                return NotFound();
            }
            ViewData["ilceId"] = new SelectList(_context.Set<ilce>(), "ilceID", "ilceID", hastane.ilceId);
            return View(hastane);
        }

        // POST: Hastane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("hastaneId,hastaneAdi,ilceId")] Hastane hastane)
        {
            if (id != hastane.hastaneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaneExists(hastane.hastaneId))
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
            ViewData["ilceId"] = new SelectList(_context.Set<ilce>(), "ilceID", "ilceID", hastane.ilceId);
            return View(hastane);
        }

        // GET: Hastane/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hastane == null)
            {
                return NotFound();
            }

            var hastane = await _context.Hastane
                .Include(h => h.ilce)
                .FirstOrDefaultAsync(m => m.hastaneId == id);
            if (hastane == null)
            {
                return NotFound();
            }

            return View(hastane);
        }

        // POST: Hastane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hastane == null)
            {
                return Problem("Entity set 'HastaneRandevuUygulamasıContext.Hastane'  is null.");
            }
            var hastane = await _context.Hastane.FindAsync(id);
            if (hastane != null)
            {
                _context.Hastane.Remove(hastane);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaneExists(int id)
        {
          return (_context.Hastane?.Any(e => e.hastaneId == id)).GetValueOrDefault();
        }
    }
}
