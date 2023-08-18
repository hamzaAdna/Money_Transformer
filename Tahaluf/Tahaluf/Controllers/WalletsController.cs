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
    public class WalletsController : Controller
    {
        private readonly ModelContext _context;

        public WalletsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Wallets
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Wallets.Include(w => w.Bank).Include(w => w.Useracount);
            return View(await modelContext.ToListAsync());
        }


    
            // GET: Wallets/Details/5

        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Wallets == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.Bank)
                .Include(w => w.Useracount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // GET: Wallets/Create
        public IActionResult Create()
        {
            ViewData["Bankid"] = new SelectList(_context.Banks, "Id", "Name");
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Name");
            return View();
        }

        // POST: Wallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,Iban,Balance,Createdate,Bankid,Useracountid")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {


                wallet.Iban = Convert.ToDecimal(DateTime.UtcNow.Ticks.ToString().Substring(0, 10));
                wallet.Createdate=DateTime.Now;
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bankid"] = new SelectList(_context.Banks, "Id", "Id", wallet.Bankid);
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Id", wallet.Useracountid);
            return View(wallet);
        }

        // GET: Wallets/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Wallets == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }
            ViewData["Bankid"] = new SelectList(_context.Banks, "Name", "Name", wallet);
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Id", wallet.Useracountid);
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Status,Iban,Balance,Createdate,Bankid,Useracountid")] Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.Id))
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
            ViewData["Bankid"] = new SelectList(_context.Banks, "Id", "Id", wallet.Bankid);
            ViewData["Useracountid"] = new SelectList(_context.Useracounts, "Id", "Id", wallet.Useracountid);
            return View(wallet);
        }

        // GET: Wallets/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Wallets == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.Bank)
                .Include(w => w.Useracount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Wallets == null)
            {
                return Problem("Entity set 'ModelContext.Wallets'  is null.");
            }
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletExists(decimal id)
        {
          return (_context.Wallets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
