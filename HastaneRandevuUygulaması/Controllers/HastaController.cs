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
    public class HastaController : Controller
    {
        private readonly HastaneRandevuUygulamasıContext _context;

        public HastaController(HastaneRandevuUygulamasıContext context)
        {
            _context = context;
        }

        // GET: Hasta
        public async Task<IActionResult> Index()
        {
              return _context.Hasta != null ? 
                          View(await _context.Hasta.ToListAsync()) :
                          Problem("Entity set 'HastaneRandevuUygulamasıContext.Hasta'  is null.");
        }

        // GET: Hasta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .FirstOrDefaultAsync(m => m.hastaId == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // GET: Hasta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hasta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("hastaId,hastaName,hastaLastName,Password,ConfirmPassword,tc,Email,dogumTarihi")] Hasta hasta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hasta);
        }

        // GET: Hasta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }
            return View(hasta);
        }

        // POST: Hasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("hastaId,hastaName,hastaLastName,Password,ConfirmPassword,tc,Email,dogumTarihi")] Hasta hasta)
        {
            if (id != hasta.hastaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaExists(hasta.hastaId))
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
            return View(hasta);
        }

        // GET: Hasta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .FirstOrDefaultAsync(m => m.hastaId == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // POST: Hasta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hasta == null)
            {
                return Problem("Entity set 'HastaneRandevuUygulamasıContext.Hasta'  is null.");
            }
            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta != null)
            {
                _context.Hasta.Remove(hasta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaExists(int id)
        {
          return (_context.Hasta?.Any(e => e.hastaId == id)).GetValueOrDefault();
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Hesap()
        {
            return _context.Hasta != null ?
                        View(await _context.Hasta.ToListAsync()) :
                        Problem("Entity set 'HastaneRandevuUygulamasıContext.Hasta'  is null.");
        }
        public IActionResult Exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> LoginControl(Hasta hasta)
        {
            var Hasta = await _context.Hasta
                .FirstOrDefaultAsync(h => h.tc == hasta.tc && h.Password == hasta.Password);

            if (Hasta != null)
            {
                HttpContext.Session.SetString("SessionUser", Hasta.tc);
                HttpContext.Session.SetString("Yetki", "Hasta");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Giriş başarısız, kullanıcıyı bilgilendir
                ViewBag.ErrorMessage = "TC Kimlik No veya Şifre yanlış.";
                return RedirectToAction("Login", "Hasta");
            }
        }
    }
}
