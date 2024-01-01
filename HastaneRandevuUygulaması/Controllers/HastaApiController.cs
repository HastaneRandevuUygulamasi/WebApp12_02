using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuUygulaması.Data;
using HastaneRandevuUygulaması.Models;

namespace HastaneRandevuUygulaması.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HastaApiController : ControllerBase
    {
        private readonly HastaneRandevuUygulamasıContext _context;

        public HastaApiController(HastaneRandevuUygulamasıContext context)
        {
            _context = context;
        }

        // GET: api/HastaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hasta>>> GetHasta()
        {
          if (_context.Hasta == null)
          {
              return NotFound();
          }
            return await _context.Hasta.ToListAsync();
        }

        // GET: api/HastaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hasta>> GetHasta(int id)
        {
          if (_context.Hasta == null)
          {
              return NotFound();
          }
            var hasta = await _context.Hasta.FindAsync(id);

            if (hasta == null)
            {
                return NotFound();
            }

            return hasta;
        }

        // PUT: api/HastaApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHasta(int id, Hasta hasta)
        {
            if (id != hasta.hastaId)
            {
                return BadRequest();
            }

            _context.Entry(hasta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HastaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HastaApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hasta>> PostHasta(Hasta hasta)
        {
          if (_context.Hasta == null)
          {
              return Problem("Entity set 'HastaneRandevuUygulamasıContext.Hasta'  is null.");
          }
            _context.Hasta.Add(hasta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHasta", new { id = hasta.hastaId }, hasta);
        }

        // DELETE: api/HastaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHasta(int id)
        {
            if (_context.Hasta == null)
            {
                return NotFound();
            }
            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }

            _context.Hasta.Remove(hasta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HastaExists(int id)
        {
            return (_context.Hasta?.Any(e => e.hastaId == id)).GetValueOrDefault();
        }
    }
}
