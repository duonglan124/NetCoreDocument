using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
namespace MVC.Controllers
{

    public class PersonController : Controller
    {
        private readonly ApplicationDbcontext _context;
        public PersonController(ApplicationDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
           var model = await _context.Person.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,NamSinh")] Person ps)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ps);
        }
        public IActionResult Edit(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }
                var ps = _context.Person.Find(id);
                if (ps == null)
                {
                    return NotFound();
                }
                return View(ps);
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FullName,NamSinh")] Person ps)
        {
            if (id != ps.FullName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(ps.FullName))
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
            return View(ps);
        }
        public IActionResult Delete(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var ps = _context.Person
                .FirstOrDefault(m => m.FullName == id);
            if (ps == null)
            {
                return NotFound();
            }

            return View(ps);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Person'  is null.");
            }
            var ps = await _context.Person.FindAsync(id);
            if (ps != null)
            {
                _context.Person.Remove(ps);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PersonExists(string FullName)
        {
            return (_context.Person?.Any(e => e.FullName == FullName)).GetValueOrDefault();
        }
        public IActionResult Welcome()
        {
            ViewData["Message"] = "Your welcome message";

            return View();
        }
        [HttpPost]
        public IActionResult Index(Person ps)
        {
            ViewBag.Message = "xin chào " + ps.FullName + " - " + ps.NamSinh;
            return View();
        }
    }
}