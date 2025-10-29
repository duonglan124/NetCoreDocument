using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class HeThongPhanPhoiController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public HeThongPhanPhoiController(ApplicationDbcontext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var heThongPhanPhoi = _context.HeThongPhanPhoi.ToList();
            return View(heThongPhanPhoi);
        }
        // GET: HeThongPhanPhoi/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: HeThongPhanPhoi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHTPP,TenHTPP")] HeThongPhanPhoi htpp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(htpp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(htpp);
        }
        // GET: HeThongPhanPhoi/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.HeThongPhanPhoi == null)
            {
                return NotFound();
            }

            var heThongPhanPhoi = await _context.HeThongPhanPhoi.FindAsync(id);
            if (heThongPhanPhoi == null)
            {
                return NotFound();
            }
            return View(heThongPhanPhoi);
        }
        // POST: HeThongPhanPhoi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHTPP,TenHTPP")] HeThongPhanPhoi htpp)
        {
            if (id != htpp.MaHTPP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(htpp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeThongPhanPhoiExists(htpp.MaHTPP))
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
            return View(htpp);
        }
        // GET: HeThongPhanPhoi/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.HeThongPhanPhoi == null)
            {
                return NotFound();
            }

            var heThongPhanPhoi = await _context.HeThongPhanPhoi
                .FirstOrDefaultAsync(m => m.MaHTPP == id);
            if (heThongPhanPhoi == null)
            {
                return NotFound();
            }

            return View(heThongPhanPhoi);
        }
        // POST: HeThongPhanPhoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var heThongPhanPhoi = await _context.HeThongPhanPhoi.FindAsync(id);
            if (heThongPhanPhoi != null)
            {
                _context.HeThongPhanPhoi.Remove(heThongPhanPhoi);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool HeThongPhanPhoiExists(string maHTPP)
        {
            return _context.HeThongPhanPhoi.Any(e => e.MaHTPP == maHTPP);
        }
        
    }

}
