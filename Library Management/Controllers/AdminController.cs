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
    
    public class AdminController : Controller
    {
        public readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public AdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _WebHostEnvironment = webHostEnvironment;
        }
        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            //if (HttpContext.Session != null)
            //{
            //    return View(await _Context.Admins.ToListAsync());

            //}
            //return null;
            return View(await _Context.Admins.ToListAsync());
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var admin = _Context.Admins.Where(a => a.Id == id).FirstOrDefault();
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminImageViewModel vm)
        {
            var admin = new Admins
            {
                Name = vm.Name,
                Email = vm.Email,
                Password = vm.Password,
                MobileNo = vm.MobileNo,
            };
            string fileName = null;
            if (vm.AdminNameImage != null)
            {
                string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.AdminNameImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.AdminNameImage.CopyTo(fileStream);
                    admin.AdminImage = fileName;
                }
            }
            _Context.Admins.Add(admin);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AdminController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var admin = await _Context.Admins.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (id == null)
            {
                return NotFound();
            }
            AdminImageViewModel vm = new AdminImageViewModel();
            vm.Name = admin.Name;
            vm.Email = admin.Email;
            vm.Password = admin.Password;
            vm.MobileNo = admin.MobileNo;
            return View(vm);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AdminImageViewModel vm)
        {
            var admin = _Context.Admins.Find(vm.Id);
            admin.Name = vm.Name;
            admin.Email = vm.Email;
            admin.Password = vm.Password;
            admin.MobileNo = vm.MobileNo;
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (vm.AdminNameImage != null)
                {
                    string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");
                    fileName = Guid.NewGuid().ToString() + "-" + vm.AdminNameImage.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.AdminNameImage.CopyTo(fileStream);
                        admin.AdminImage = fileName;
                    }
                }
            }
            _Context.Admins.Update(admin);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AdminController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _Context.Admins.FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Admins admin)
        {
            _Context.Admins.Remove(admin);
            await _Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
