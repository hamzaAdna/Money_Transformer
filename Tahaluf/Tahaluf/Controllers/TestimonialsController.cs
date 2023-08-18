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
    public class TestimonialsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public TestimonialsController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Testimonials.Include(t => t.Useracount);
            return View(await modelContext.ToListAsync());
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Useracount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Id");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Imagepath,Message,Status,Useracountid,ImageFile")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                if (testimonial.ImageFile != null)
                {
                    string w3rootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + testimonial.ImageFile.FileName;
                    string path = Path.Combine(w3rootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await testimonial.ImageFile.CopyToAsync(fileStream);
                    }
                    testimonial.Imagepath = fileName;
                }
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Id", testimonial.Useracountid);
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Id", testimonial.Useracountid);
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Imagepath,Message,Status,Useracountid,ImageFile")] Testimonial testimonial)
        {
            if (id != testimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (testimonial.ImageFile != null)
                    {
                        string w3rootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + testimonial.ImageFile.FileName;
                        string path = Path.Combine(w3rootPath + "/images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await testimonial.ImageFile.CopyToAsync(fileStream);
                        }
                        testimonial.Imagepath = fileName;
                    }
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.Id))
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
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Id", testimonial.Useracountid);
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Useracount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Testimonials == null)
            {
                return Problem("Entity set 'ModelContext.Testimonials'  is null.");
            }
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AcceptCase(decimal? id, string Acceptname = "Valid")
        {

            if(id !=null)
            {

                var item=_context.Testimonials.FirstOrDefault(t=> t.Id == id);
                if (item != null)
                {
                    item.Status= Acceptname;
                    _context.Update(item);
                    await _context.SaveChangesAsync();

                }

                return RedirectToAction(nameof(Index));

            }else
            {
                return RedirectToAction(nameof(Index));


            }



        }

         public async Task<IActionResult> RejectCase(decimal? id, string Acceptname = "Invalid")
        {

            if(id !=null)
            {
                var testimonial = await _context.Testimonials.FindAsync(id);
                if (testimonial != null)
                {
                    _context.Testimonials.Remove(testimonial);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return RedirectToAction(nameof(Index));


            }



        }


        private bool TestimonialExists(decimal id)
        {
          return (_context.Testimonials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
