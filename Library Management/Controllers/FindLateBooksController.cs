using Api.DataContext;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management.Controllers
{
    public class FindLateBooksController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public FindLateBooksController(ApplicationDbContext context)
        {
            _Context = context;
        }

        // GET: FineLateBooks
        public async Task<ActionResult> Index()
        {
            return View(await _Context.Borrowings.Include(x => x.Student).Include(x => x.Book).ToListAsync());
        }

        // GET: FineLateBooks/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fines = _Context.Borrowings.Include(x => x.Student).Include(x => x.Book).Where(x => x.Id == id).FirstOrDefault();
            if (fines == null)
            {
                return NotFound();
            }
            return View(fines);
        }


        public ActionResult PayFine()
        {
            ViewBag.Students = _Context.Students.ToList();
            ViewBag.Books = _Context.Books.ToList();
            return View();
        }




        // GET: FineLateBooks/Create
        public ActionResult Create()
        {
            ViewBag.Students = _Context.Students.ToList();
            ViewBag.Books = _Context.Books.ToList();
            return View();
        }

        // POST: FineLateBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Borrowings fines)
        {
            var fine = _Context.Borrowings.Add(fines);
            if (fine.Entity != null)
            {
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fine);
        }

        // GET: FineLateBooks/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var fine = await _Context.Borrowings.Where(a => a.Id == id).FirstOrDefaultAsync();
            ViewBag.Students = _Context.Students.ToList();
            ViewBag.Books = _Context.Books.ToList();
            if (fine == null)
            {
                return RedirectToAction("Index");
            }
            return View(fine);
        }
        // debug kar
        // POST: FineLateBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Borrowings fine)
        {
            if (id == null)
            {
                return NotFound();
            }
            _Context.Borrowings.Update(fine);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: FineLateBooks/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fine = await _Context.Borrowings.Include(x => x.Student).Include(x => x.Book).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (fine == null)
            {
                return NotFound();
            }
            return View(fine);
        }

        // POST: FineLateBooks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Borrowings fine)
        {
            _Context.Borrowings.Remove(fine);
            await _Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
