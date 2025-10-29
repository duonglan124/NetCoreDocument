using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
namespace MVC.Controllers
{
    public class DaiLyControllers : Controller

    {
        private readonly ApplicationDbcontext _context;

        public DaiLyControllers(ApplicationDbcontext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var DaiLy = _context.DaiLy.ToList();
            return View(DaiLy);
        }
        // GET: DaiLy/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: DaiLy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDaiLy,TenDaiLy,DiaChi,NguoiDaiDien,DienThoai,MaHTPP")] DaiLy dl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dl);
        }
        // GET: DaiLy/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.DaiLy == null)
            {
                return NotFound();
            }

            var daiLy = await _context.DaiLy.FindAsync(id);
            if (daiLy == null)
            {
                return NotFound();
            }
            return View(daiLy);
        }
        // POST: DaiLy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDaiLy,TenDaiLy,DiaChi,NguoiDaiDien,DienThoai,MaHTPP")] DaiLy dl)
        {
            if (id != dl.MaDaiLy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaiLyExists(dl.MaDaiLy))
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
            return View(dl);
        }
        // GET: DaiLy/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.DaiLy == null)
            {
                return NotFound();
            }

            var daiLy = await _context.DaiLy
                .FirstOrDefaultAsync(m => m.MaDaiLy == id);
            if (daiLy == null)
            {
                return NotFound();
            }

            return View(daiLy);
        }
        // POST: DaiLy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var daiLy = await _context.DaiLy.FindAsync(id);
            if (daiLy != null)
            {
                _context.DaiLy.Remove(daiLy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool DaiLyExists(string id)
        {
            return _context.DaiLy.Any(e => e.MaDaiLy == id);
        }
    }
}
