using Api.DataContext;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class BorrowingController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public BorrowingController(ApplicationDbContext context)
        {
            _Context = context;
        }
        // GET: BorrowingController
        public async Task<ActionResult> Index()
        {
            return View(await _Context.Borrowings.Include(x => x.Student).Include(x => x.Book).ToListAsync());
        }

        // GET: BorrowingController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var borrowing = _Context.Borrowings.Where(a => a.Id == id).FirstOrDefault();
            var borrowings = _Context.Borrowings.Include(x => x.Student).Include(x => x.Book).Where(x => x.Id == id).FirstOrDefault();
            if (borrowings == null)
            {
                return NotFound();
            }

            return View(borrowings);
        }

        // GET: BorrowingController/Create
        public ActionResult Create()
        {
            ViewBag.Students = _Context.Students.ToList();
            ViewBag.Books = _Context.Books.ToList();
            return View();
        }

        // POST: BorrowingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Borrowings borrowing)
        {
            var borrow = _Context.Borrowings.Add(borrowing);
            if (borrow.Entity != null)
            {
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrow);
        }

        // GET: BorrowingController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var borrow = await _Context.Borrowings.Where(a => a.Id == id).FirstOrDefaultAsync();
            ViewBag.Students = _Context.Students.ToList();
            ViewBag.Books = _Context.Books.ToList();
            if (borrow == null)
            {
                return RedirectToAction("Index");
            }
            return View(borrow);
        }

        // POST: BorrowingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Borrowings borrowing)
        {
            if (id == null)
            {
                return NotFound();
            }
            _Context.Borrowings.Update(borrowing);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BorrowingController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var borrow = await _Context.Borrowings.Include(x => x.Student).Include(x => x.Book).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (borrow == null)
            {
                return NotFound();
            }
            return View(borrow);
        }

        // POST: BorrowingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Borrowings borrowing)
        {
            _Context.Borrowings.Remove(borrowing);
            await _Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
