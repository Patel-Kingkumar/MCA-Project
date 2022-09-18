using Api.DataContext;
using DataAccessLayer;
using Library_Management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AuthorController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public AuthorController(ApplicationDbContext context)
        {
            _Context = context;
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            return View(await _Context.Authors.ToListAsync());
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var auth = _Context.Authors.Where(a => a.Id == id).FirstOrDefault();
            if (auth == null)
            {
                return NotFound();
            }

            return View(auth);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Authors author)
        {
            var auth = _Context.Authors.Add(author);
            if (auth .Entity != null)
            {
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(auth);
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var auth = await _Context.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (auth == null)
            {
                return RedirectToAction("Index");
            }
            return View(auth);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Authors author)
        {
            if (id == null)
            {
                return NotFound();
            }
            _Context.Authors.Update(author);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AuthorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var auth = await _Context.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (auth == null)
            {
                return NotFound();
            }
            return View(auth);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Authors author)
        {
            _Context.Authors.Remove(author);
            await _Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
