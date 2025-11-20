
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.Models.Process;
using System.Data;
using X.PagedList;


namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbcontext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index(int? page)
      {
        int pageNumber = page ?? 1;
        int pageSize = 5;

        var list = await _context.Person.ToListAsync();
        var model = list.ToPagedList(pageNumber, pageSize);

        return View(model);
    }


        // GET: Person/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
       public IActionResult Uploads()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Uploads(IFormFile file)
{
    if (file != null)
    {
        string fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension != ".xls" && fileExtension != ".xlsx")
        {
            ModelState.AddModelError("", "Please choose an Excel file (.xls or .xlsx) to upload!");
        }
        else
        {
            //Tạo thư mục lưu file nếu chưa có
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Excels");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            //Đặt lại tên file (tránh ký tự ':' trong giờ)
            var filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExtension;
            var filePath = Path.Combine(uploadFolder, filename);

            //Lưu file Excel lên server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //BẮT BUỘC: set license cho EPPlus (đây là chỗ lỗi LicenseNotSetException)
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Đọc dữ liệu từ file Excel
            var dt = _excelProcess.ExcelToDataTable(filePath);

            // Ghi dữ liệu xuống database
            foreach (DataRow row in dt.Rows)
            {
                var ps = new Person
                {
                    PersonId = row[0].ToString(), 
                    FullName = row[1].ToString(),
                    Address = row[2].ToString(),
                    PhoneNumber = row[3].ToString()
                };
                _context.Person.Add(ps);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

    ModelState.AddModelError("", "Vui lòng chọn file Excel để tải lên!");
    return View();
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,PhoneNumber")] Person ps)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ps);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address,PhoneNumber")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
