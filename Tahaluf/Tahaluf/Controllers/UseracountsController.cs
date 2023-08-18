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
    public class UseracountsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public UseracountsController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Useracounts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Useracounts.Include(u => u.Role).Where(x=>x.Roleid==2);
            return View(await modelContext.ToListAsync());
        }

        // GET: Useracounts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Useracounts == null)
            {
                return NotFound();
            }

            var useracount = await _context.Useracounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useracount == null)
            {
                return NotFound();
            }

            return View(useracount);
        }

        // GET: Useracounts/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Role1s, "Id", "Id");
            return View();
        }

        // POST: Useracounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Imagepath,Roleid,ImageFile")] Useracount useracount)
        {
            if (ModelState.IsValid)
            {
                if (useracount.ImageFile != null)
                {
                    string w3rootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + useracount.ImageFile.FileName;
                    string path = Path.Combine(w3rootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await useracount.ImageFile.CopyToAsync(fileStream);
                    }
                    useracount.Imagepath = fileName;
                }
                _context.Add(useracount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Role1s, "Id", "Id", useracount.Roleid);
            return View(useracount);
        }

        // GET: Useracounts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Useracounts == null)
            {
                return NotFound();
            }

            var useracount = await _context.Useracounts.FindAsync(id);
            if (useracount == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Role1s, "Id", "Id", useracount.Roleid);
            return View(useracount);
        }

        // POST: Useracounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Email,Password,Imagepath,Roleid,ImageFile")] Useracount useracount)
        {
            if (id != useracount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (useracount.ImageFile != null)
                    {
                        string w3rootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + useracount.ImageFile.FileName;
                        string path = Path.Combine(w3rootPath + "/images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await useracount.ImageFile.CopyToAsync(fileStream);
                        }
                        useracount.Imagepath = fileName;
                    }
                    _context.Update(useracount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseracountExists(useracount.Id))
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
            ViewData["Roleid"] = new SelectList(_context.Role1s, "Id", "Id", useracount.Roleid);
            return View(useracount);
        }

        // GET: Useracounts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Useracounts == null)
            {
                return NotFound();
            }

            var useracount = await _context.Useracounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useracount == null)
            {
                return NotFound();
            }

            return View(useracount);
        }

        // POST: Useracounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Useracounts == null)
            {
                return Problem("Entity set 'ModelContext.Useracounts'  is null.");
            }
            var useracount = await _context.Useracounts.FindAsync(id);
            if (useracount != null)
            {
                _context.Useracounts.Remove(useracount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseracountExists(decimal id)
        {
          return (_context.Useracounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
