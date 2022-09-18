using Api.DataContext;
using DataAccessLayer;
using Library_Management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        public readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public StudentController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _WebHostEnvironment = webHostEnvironment;
        }
        // GET: StudentController
        public async Task<ActionResult> Index()
        {

            var student = await _Context.Students.Include(x => x.BookBorrowing).ThenInclude(y => y.Book).ToListAsync();
            if (student == null)
            {
                return NotFound();
                // return View("NotFound",id);               
            }
            
            return View(student);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
                // return View("NotFound");
            }

            var student = _Context.Students.Include(x => x.BookBorrowing).ThenInclude(y => y.Book).FirstOrDefault(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
                // return View("NotFound",id);               
            }

            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.Books = _Context.Books.ToList();
            //var booksName = _Context.Books.Select(x => new SelectListItem()
            //{
            //    Text = x.BookName,
            //    Value = x.Id.ToString()
            //}).ToList();

            //StudentImageViewModel vm = new StudentImageViewModel();
            //vm.BookName = booksName;

            return View();
        }

        // POST: StudentController/Create   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentImageViewModel vm)
        {
            var student = new Students
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Age = vm.Age,
                MobileNo = vm.MobileNo,
                Email = vm.Email,
                Password = vm.Password
            };
            //var selectedBookName = vm.BookName.Where(x => x.Selected).Select(y => y.Value).ToList();
            //foreach (var item in selectedBookName)
            //{
            //    student.BookBorrowing.Add(new Borrowings()
            //    {
            //        BookId = int.Parse(item)

            //    });
            //}
            string fileName = null;
            if (vm.StudentNameImage != null)
            {
                string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.StudentNameImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.StudentNameImage.CopyTo(fileStream);
                    student.StudentImage = fileName;
                }
            }
            _Context.Students.Add(student);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var bookName = await _Context.Students.Include(x => x.BookBorrowing).Where(y => y.Id == id).FirstOrDefaultAsync();
            if (id == null)
            {
                return NotFound();
            }
            //// already checked
            //var selectedIds = bookName.BookBorrowing.Select(x => x.BookId).ToList();
            //// add new
            //var items = _Context.Books.Select(x => new SelectListItem()
            //{
            //    Text = x.BookName,
            //    Value = x.Id.ToString(),
            //    Selected = selectedIds.Contains(x.Id)
            //}).ToList();

            StudentImageViewModel vm = new StudentImageViewModel();
            vm.FirstName = bookName.FirstName;
            vm.LastName = bookName.LastName;
            vm.Age = bookName.Age;
            vm.MobileNo = bookName.MobileNo;
            vm.Email = bookName.Email;
            vm.Password = bookName.Password;
            //vm.BookName = items;
            if (bookName == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StudentImageViewModel vm)
        {
            var student = _Context.Students.Include(x => x.BookBorrowing).FirstOrDefault(y => y.Id == vm.Id);
            student.FirstName = vm.FirstName;
            student.LastName = vm.LastName;
            student.Age = vm.Age;
            student.MobileNo = vm.MobileNo;
            student.Email = vm.Email;
            student.Password = vm.Password;
            
            //var existingId = student.BookBorrowing.Select(x => x.BookId).ToList();
            //var selectIds = vm.BookName.Where(x => x.Selected).Select(y => y.Value).Select(int.Parse).ToList();
            //var toAdd = selectIds.Except(existingId);
            //var toRemove = existingId.Except(selectIds);
            //student.BookBorrowing = student.BookBorrowing.Where(x => !toRemove.Contains(x.BookId)).ToList();

            //foreach (var item in toAdd)
            //{
            //    student.BookBorrowing.Add(new Borrowings()
            //    {
            //        BookId = item
            //    });
            //}
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (vm.StudentNameImage != null)
                {
                    string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");
                    fileName = Guid.NewGuid().ToString() + "-" + vm.StudentNameImage.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.StudentNameImage.CopyTo(fileStream);
                        student.StudentImage = fileName;
                    }
                }
            }
            _Context.Students.Update(student);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
                // return View("NotFound");
            }

            var student = await _Context.Students.Include(x => x.BookBorrowing).ThenInclude(y => y.Book).FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
                // return View("NotFound",id);               
            }

            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Students student)
        {
            _Context.Students.Remove(student);
            await _Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
