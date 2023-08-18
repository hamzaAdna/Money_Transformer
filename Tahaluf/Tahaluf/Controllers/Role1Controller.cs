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
    public class Role1Controller : Controller
    {
        private readonly ModelContext _context;

        public Role1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Role1
        public async Task<IActionResult> Index()
        {
              return _context.Role1s != null ? 
                          View(await _context.Role1s.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Role1s'  is null.");
        }

        // GET: Role1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Role1s == null)
            {
                return NotFound();
            }

            var role1 = await _context.Role1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role1 == null)
            {
                return NotFound();
            }

            return View(role1);
        }

        // GET: Role1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Role1 role1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role1);
        }

        // GET: Role1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Role1s == null)
            {
                return NotFound();
            }

            var role1 = await _context.Role1s.FindAsync(id);
            if (role1 == null)
            {
                return NotFound();
            }
            return View(role1);
        }

        // POST: Role1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name")] Role1 role1)
        {
            if (id != role1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Role1Exists(role1.Id))
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
            return View(role1);
        }

        // GET: Role1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Role1s == null)
            {
                return NotFound();
            }

            var role1 = await _context.Role1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role1 == null)
            {
                return NotFound();
            }

            return View(role1);
        }

        // POST: Role1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Role1s == null)
            {
                return Problem("Entity set 'ModelContext.Role1s'  is null.");
            }
            var role1 = await _context.Role1s.FindAsync(id);
            if (role1 != null)
            {
                _context.Role1s.Remove(role1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Role1Exists(decimal id)
        {
          return (_context.Role1s?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
