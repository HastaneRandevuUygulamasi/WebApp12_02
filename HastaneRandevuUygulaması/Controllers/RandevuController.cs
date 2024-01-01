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
    public class RandevuController : Controller
    {
        private readonly HastaneRandevuUygulamasıContext _context;

        public RandevuController(HastaneRandevuUygulamasıContext context)
        {
            _context = context;
        }

        // GET: Randevu
        public async Task<IActionResult> Index()
        {
            var hastaneRandevuUygulamasıContext = _context.Randevu.Include(r => r.doktor).Include(r => r.hasta);
            return View(await hastaneRandevuUygulamasıContext.ToListAsync());
        }

        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .FirstOrDefaultAsync(m => m.randevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevu/Create
        public IActionResult Create()
        {
            ViewData["doktorId"] = new SelectList(_context.Doktor, "doktorId", "doktorId");
            ViewData["hastaId"] = new SelectList(_context.Hasta, "hastaId", "Email");
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("randevuId,randevuTarihi,doktorId,hastaId")] Randevu randevu)
        {
            
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["doktorId"] = new SelectList(_context.Doktor, "doktorId", "doktorId", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "hastaId", "Email", randevu.hastaId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("randevuId,randevuTarihi,doktorId,hastaId")] Randevu randevu)
        {
            if (id != randevu.randevuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.randevuId))
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
            ViewData["doktorId"] = new SelectList(_context.Doktor, "doktorId", "doktorId", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "hastaId", "Email", randevu.hastaId);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .FirstOrDefaultAsync(m => m.randevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevu == null)
            {
                return Problem("Entity set 'HastaneRandevuUygulamasıContext.Randevu'  is null.");
            }
            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevu.Remove(randevu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
          return (_context.Randevu?.Any(e => e.randevuId == id)).GetValueOrDefault();
        }
    }
}
