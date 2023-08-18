using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Tahaluf.Models;

namespace Tahaluf.Controllers
{
    public class HomesController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ModelContext _context;

        public HomesController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Homes
        public async Task<IActionResult> Index()
        {
              return _context.Homes != null ? 
                          View(await _context.Homes.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Homes'  is null.");
        }

        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imagepath,Logopath,Bankname,Paragraph,Websitephone,Wensiteemail,ImageFile")] Home home)
        {
            if (ModelState.IsValid)
            {
                if (home.ImageFile != null)
                {
                    string w3rootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + home.ImageFile.FileName;
                    string path = Path.Combine(w3rootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.ImageFile.CopyToAsync(fileStream);
                    }
                    home.Imagepath = fileName;
                }
                
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Imagepath,Logopath,Bankname,Paragraph,Websitephone,Wensiteemail,ImageFile")] Home home)
        {
            if (id != home.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (home.ImageFile != null)
                    {
                        string w3rootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + home.ImageFile.FileName;
                        string path = Path.Combine(w3rootPath + "/images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(fileStream);
                        }
                        home.Imagepath = fileName;
                    }
                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Id))
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
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homes == null)
            {
                return Problem("Entity set 'ModelContext.Homes'  is null.");
            }
            var home = await _context.Homes.FindAsync(id);
            if (home != null)
            {
                _context.Homes.Remove(home);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
          return (_context.Homes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
