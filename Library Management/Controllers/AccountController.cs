using Api.DataContext;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _Context;
        public AccountController(ApplicationDbContext context)
        {
            _Context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        

        public async Task<ActionResult> AdminList()
        {
            List<Admins> admins = new List<Admins>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/admin");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                admins = JsonConvert.DeserializeObject<List<Admins>>(result);
            }
            return View(admins);
        }
        [HttpPost]
        public IActionResult Login(string username, string email, string password)
        {
            var admin = _Context.Admins.Where(x => x.Name == username && x.Email == email && x.Password == password).FirstOrDefault();
            if (admin == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var adminName = admin.Name;
            var adminEmail = admin.Email;
            var adminPassword = admin.Password;
            if (username != null && email != null && password != null && username.Equals(adminName) && email.Equals(adminEmail) && password.Equals(adminPassword))
            {
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("adminImage", admin.AdminImage);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Logout","Account");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("username");
            return View();
        }
    }
}
