using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public EmployeeController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var model = await _context.Employee.ToListAsync();
            return View(model);
        }

        // GET: Employee/Create
        [HttpGet("Employee/Create")]
        public IActionResult Create()
        {
            return View();

        }

        // POST: Employee/Create
        [HttpPost("Employee/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,PhoneNumber,EmployeeId,Age")] Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);

        }

        // GET: Employee/Edit/5
        [HttpGet("Employee/Edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await _context.Employee.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);

        }

        // POST: Employee/Edit/5
        [HttpPost("Employee/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address,PhoneNumber,EmployeeId,Age")] Employee emp)
        {
            if (id != emp.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employee.Any(e => e.PersonId == emp.PersonId))
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
            return View(emp);

        }

        // GET: Employee/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);

        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool EmployeeExists(string id)
        {
            return (_context.Employee?.Any(e => e.EmployeeId == id.ToString())).GetValueOrDefault();
        }
    }
}