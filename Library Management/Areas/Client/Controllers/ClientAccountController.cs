using Api.DataContext;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library_Management.Areas.Client.Controllers
{
    [Area("Client")]
    public class ClientAccountController : Controller
    {
        private readonly ApplicationDbContext _Context;
        public ClientAccountController(ApplicationDbContext context)
        {
            _Context = context;
        }
        // GET: ClientAccountController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public async Task<ActionResult> StudentList()
        {
            List<Students> students = new List<Students>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/student");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<Students>>(result);
            }
            return View(students);
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var student = _Context.Students.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

            if (student != null)
            {
                HttpContext.Session.SetInt32("Sid", student.Id);
                //return RedirectToAction("Home", "ClientAccount");
                return RedirectToAction("BooksMediaList", "ClientBook");
            }
            else
            {
                return RedirectToAction("Logout", "ClientAccount");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("FirstName");
            return View();
        }

    }
}
