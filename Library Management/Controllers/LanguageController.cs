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
    public class LanguageController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public LanguageController(ApplicationDbContext context)
        {
            _Context = context;
        }
        // GET: LanguageController
        public async Task<ActionResult> Index()
        {
            return View(await _Context.Languages.ToListAsync());
        }

        // GET: LanguageController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lang = _Context.Languages.Where(a => a.Id == id).FirstOrDefault();
            if (lang == null)
            {
                return NotFound();
            }
            return View(lang);
        }

        // GET: LanguageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LanguageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Languages languag)
        {
            var lang = _Context.Languages.Add(languag);
            if (lang.Entity != null)
            {
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(languag);
        }

        // GET: LanguageController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lang = await _Context.Languages.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (lang == null)
            {
                return RedirectToAction("Index");
            }
            return View(lang);
        }

        // POST: LanguageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Languages languag)
        {
            if (id == null)
            {
                return NotFound();
            }
            _Context.Languages.Update(languag);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: LanguageController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lang = await _Context.Languages.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (lang == null)
            {
                return NotFound();
            }
            return View(lang);
        }

        // POST: LanguageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Languages languag)
        {
            _Context.Languages.Remove(languag);
            await _Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
