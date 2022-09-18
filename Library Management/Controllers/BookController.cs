using Api.DataContext;
using DataAccessLayer;
using Library_Management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        public readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public BookController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _WebHostEnvironment = webHostEnvironment;
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            return View(await _Context.Books.Include(x => x.Language).Include(x => x.Author).ToListAsync());
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var books = _Context.Books.Where(x => x.Id == id).FirstOrDefault();
            var books = _Context.Books.Include(x => x.Language).Include(x => x.Author).Where(x => x.Id == id).FirstOrDefault();
            HttpContext.Session.SetInt32("Bid", books.Id);
            return View(books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {

            ViewBag.Authors = _Context.Authors.ToList();
            ViewBag.Languages = _Context.Languages.ToList();
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookImageViewModel vm)
        {
            var book = new Books
            {
                Title = vm.Title,
                BookName = vm.BookName,
                AuthorId = vm.AuthorId,
                Publisher = vm.Publisher,
                PublishDate = vm.PublishDate,
                LanguageId = vm.LanguageId,
                Description = vm.Description,
                Quantity = vm.Quantity,
                PageNo = vm.PageNo
                
            };
            string fileName = null;
            if (vm.BookNameImage != null)
            {
                string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.BookNameImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.BookNameImage.CopyTo(fileStream);
                    book.BookImage = fileName;
                }
            }
            _Context.Books.Add(book);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var book = await _Context.Books.Where(a => a.Id == id).FirstOrDefaultAsync();
            ViewBag.Authors = _Context.Authors.ToList();
            ViewBag.Languages = _Context.Languages.ToList();
            if (id == null)
            {
                return NotFound();
            }
            BookImageViewModel vm = new BookImageViewModel();
            vm.Title = book.Title;
            vm.BookName = book.BookName;
            vm.AuthorId = book.AuthorId;            
            vm.Publisher = book.Publisher;
            vm.PublishDate = book.PublishDate;
            vm.LanguageId = book.LanguageId;
            vm.Description = book.Description;
            vm.PageNo = book.PageNo;
            vm.Quantity = book.Quantity;
            return View(vm);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookImageViewModel vm)
        {
            var book = _Context.Books.Find(vm.Id);
            book.Title = vm.Title;
            book.BookName = vm.BookName;
            book.AuthorId = vm.AuthorId;
            book.Publisher = vm.Publisher;
            book.PublishDate = vm.PublishDate;
            book.LanguageId = vm.LanguageId;
            book.Description = vm.Description;
            book.Quantity = vm.Quantity;
            book.PageNo = vm.PageNo;
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (vm.BookNameImage != null)
                {
                    string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");
                    fileName = Guid.NewGuid().ToString() + "-" + vm.BookNameImage.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.BookNameImage.CopyTo(fileStream);
                        book.BookImage = fileName;
                    }

                }
            }
            _Context.Books.Update(book);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var book = await _Context.Books.Where(a => a.Id == id).FirstOrDefaultAsync();
            var books = await _Context.Books.Include(x => x.Language).Include(x => x.Author).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Books book)
        {
            _Context.Books.Remove(book);
            await _Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
    }
}