using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tahaluf.Models;

namespace Tahaluf.Controllers
{
    public class VisacardsController : Controller
    {
        private readonly ModelContext _context;

        public VisacardsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Visacards
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Visacards.Include(v => v.UseracountNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Visacards/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Visacards == null)
            {
                return NotFound();
            }

            var visacard = await _context.Visacards
                .Include(v => v.UseracountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visacard == null)
            {
                return NotFound();
            }

            return View(visacard);
        }

        // GET: Visacards/Create
        public IActionResult Create()
        {
            ViewData["Useracount"] = new SelectList(_context.Useracounts, "Id", "Id");
            return View();
        }

        // POST: Visacards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cvv,Cardnumber,Expiredate,Goodthrow,Balance,Email,Useracount")] Visacard visacard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visacard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Useracount"] = new SelectList(_context.Useracounts, "Id", "Id", visacard.Useracount);
            return View(visacard);
        }

        // GET: Visacards/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Visacards == null)
            {
                return NotFound();
            }

            var visacard = await _context.Visacards.FindAsync(id);
            if (visacard == null)
            {
                return NotFound();
            }
            ViewData["Useracount"] = new SelectList(_context.Useracounts, "Id", "Id", visacard.Useracount);
            return View(visacard);
        }

        // POST: Visacards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Cvv,Cardnumber,Expiredate,Goodthrow,Balance,Email,Useracount")] Visacard visacard)
        {
            if (id != visacard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visacard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisacardExists(visacard.Id))
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
            ViewData["Useracount"] = new SelectList(_context.Useracounts, "Id", "Id", visacard.Useracount);
            return View(visacard);
        }

        // GET: Visacards/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Visacards == null)
            {
                return NotFound();
            }

            var visacard = await _context.Visacards
                .Include(v => v.UseracountNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visacard == null)
            {
                return NotFound();
            }

            return View(visacard);
        }

        // POST: Visacards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Visacards == null)
            {
                return Problem("Entity set 'ModelContext.Visacards'  is null.");
            }
            var visacard = await _context.Visacards.FindAsync(id);
            if (visacard != null)
            {
                _context.Visacards.Remove(visacard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisacardExists(decimal id)
        {
          return (_context.Visacards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
