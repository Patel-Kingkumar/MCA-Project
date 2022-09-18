using Api.ViewModel;
using DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Library_Management.Areas.Client.Controllers
{
    [Area("Client")]
    public class ClientStudentsController : Controller
    {
        // GET: ClientStudentsController
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ClientStudentsController(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }
        public async Task<ActionResult> Index()
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

        public async Task<ActionResult> Info(int id)
        {
            Students students = new Students();
            var Id = HttpContext.Session.GetInt32("Sid");
            //HttpContext.Session.SetString("StudentFirstName","students.FirstName");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/student/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<Students>(results);
            }
            return View(students);
        }          
       
        public async Task<ActionResult> MyBooks(int id)
        {
            BookAuthLangViewModel v = new BookAuthLangViewModel();
            List<Books> books = new List<Books>();
            var sid = HttpContext.Session.GetInt32("Sid");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/book/GetMyBooks/{sid}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<Books>>(results);
            }
            v.MyBooks = books;
            return View(v);
        }

        public async Task<ActionResult> DeleteMyBooks(int bookid)
        {
            Books books = new Books();
            var sid = HttpContext.Session.GetInt32("Sid");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.DeleteAsync($"api/Borrowing/DeleteBorrowings/{sid}/{bookid}");
            books.Quantity = books.Quantity - 1;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
            }

            return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
        }


        public async Task<ActionResult> GetStudents(int id)
        {
            Students students = new Students();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/student/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<Students>(results);
            }
            return View(students);
        }


        // GET: ClientStudentsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetStudents(id);
        }

        // GET: ClientStudentsController/Create
        [HttpGet]
        public ActionResult Create(int studentId)
        {
            Students students = new Students();
            students.Id = studentId;
            return View(students);
        }


        // POST: ClientStudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Students students)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            var response = await client.PostAsJsonAsync<Students>("api/student", students);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ClientStudentsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetStudents(id);
        }

        // POST: ClientStudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Students students)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.PutAsJsonAsync<Students>($"api/student/{students.Id}", students);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ClientStudentsController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.DeleteAsync($"api/student/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        


        // POST: ClientStudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
