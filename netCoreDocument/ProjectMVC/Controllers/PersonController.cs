using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using ProjectMVC.Models;

namespace ProjectMVC.Controllers // namespace của controller
{
    public class PersonController : Controller // lớp PersonController kế thừa từ Controller
    {
        private readonly ApplicationDbContext _context; // khai báo biến _context để tương tác với database

        public PersonController(ApplicationDbContext context) //    constructor của controller
        {
            _context = context; //  gán biến _context với context được truyền vào
        }

        // GET: Person
        public async Task<IActionResult> Index() // phương thức Index để hiển thị danh sách Person
        {
            return View(await _context.Person.ToListAsync()); // trả về view với danh sách Person từ database   
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(string id) // phương thức Details để hiển thị chi tiết một Person
        {
            if (id == null) //  kiểm tra nếu id là null
            {
                return NotFound();//    trả về trang NotFound
            }

            var person = await _context.Person //   lấy Person từ database theo id
                .FirstOrDefaultAsync(m => m.PersonId == id); // tìm Person có PersonId bằng id
            if (person == null) // kiểm tra nếu Person không tồn tại
            {
                return NotFound();
            }

            return View(person); // trả về view với Person tìm được
        }

        // GET: Person/Create
        public IActionResult Create()// phương thức Create để hiển thị form tạo mới Person
        {
            return View();// trả về view tạo mới Person
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] // chỉ định phương thức HTTP POST// chỉ định pương thức nhận dữ liệu từ view gửi lên 
        [ValidateAntiForgeryToken] // bảo vệ chống lại tấn công CSRF
        public async Task<IActionResult> Create([Bind("PersonId,FullName")] Person person) // phương thức Create để xử lý dữ liệu từ form tạo mới Person
        {
            if (ModelState.IsValid)// kiểm tra tính hợp lệ của dữ liệu
            {
                _context.Add(person);// thêm Person vào context
                await _context.SaveChangesAsync();//  lưu thay đổi vào database
                return RedirectToAction(nameof(Index));//   chuyển hướng về trang Index
            }
            return View(person);// trả về view với Person nếu dữ liệu không hợp lệ
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)// phương thức Edit để hiển thị form chỉnh sửa Person
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);// tìm Person theo id
            if (person == null)
            {
                return NotFound();
            }
            return View(person);// trả về view với Person tìm được
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]// chỉ định phương thức HTTP POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName")] Person person)// phương thức Edit để xử lý dữ liệu từ form chỉnh sửa Person
        {
            if (id != person.PersonId)// kiểm tra nếu id không khớp với PersonId
            {
                return NotFound();
            }

            if (ModelState.IsValid)// kiểm tra tính hợp lệ của dữ liệu
            {
                try// khối try-catch để xử lý ngoại lệ
                {
                    _context.Update(person);// cập nhật Person trong context
                    await _context.SaveChangesAsync();//  lưu thay đổi vào database
                }
                catch (DbUpdateConcurrencyException)// bắt ngoại lệ khi có xung đột cập nhật
                {
                    if (!PersonExists(person.PersonId))// kiểm tra nếu Person không tồn tại
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;// ném lại ngoại lệ nếu có lỗi khác
                    }
                }
                return RedirectToAction(nameof(Index));//`   chuyển hướng về trang Index
            }
            return View(person);// trả về view với Person nếu dữ liệu không hợp lệ
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)// phương thức Delete để hiển thị trang xác nhận xóa Person
        {
            if (id == null)//   kiểm tra nếu id là null
            {
                return NotFound();
            }

            var person = await _context.Person//    lấy Person từ database theo id
                .FirstOrDefaultAsync(m => m.PersonId == id);//  tìm Person có PersonId bằng id
            if (person == null)//   kiểm tra nếu Person không tồn tại
            {
                return NotFound();//    trả về trang NotFound nếu Person không tồn tại
            }

            return View(person);// trả về view với Person tìm được
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]// phương thức DeleteConfirmed để xử lý xóa Person
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)//   phương thức DeleteConfirmed để xử lý xóa Person
        {
            var person = await _context.Person.FindAsync(id);//  tìm Person theo id
            if (person != null)//   kiểm tra nếu Person tồn tại
            {
                _context.Person.Remove(person);//   xóa Person khỏi database
            }

            await _context.SaveChangesAsync();//  lưu thay đổi vào database
            return RedirectToAction(nameof(Index));//   chuyển hướng về trang Index
        }

        private bool PersonExists(string id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }
    }
}
