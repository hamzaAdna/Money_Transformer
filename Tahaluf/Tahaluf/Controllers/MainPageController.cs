using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tahaluf.Models;

namespace Tahaluf.Controllers
{
    public class MainPageController : Controller
    {

        private readonly ModelContext _context;
        public MainPageController(ModelContext _context)
        {
            this._context = _context;


        }
        public async Task<IActionResult> Index()
        {
            ViewBag.countofbank = _context.Banks.Count();
            ViewBag.countofwallet =_context.Wallets.Count();
            ViewBag.countofuser = _context.Useracounts.Where(x=>x.Roleid==2).Count();
            ViewBag.countoftransaction = _context.Transactions.Count();





            var itemofaboutus = await _context.Aboutus.OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
            var itemoftestmonial= await _context.Testimonials.Where(x=>x.Status=="Valid").ToListAsync();

            var tupleData = new Tuple<Aboutu, List<Testimonial>>(itemofaboutus, itemoftestmonial);

            return View( tupleData);
        }
    }
}
