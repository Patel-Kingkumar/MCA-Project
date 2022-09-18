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
    public class ClientBorrowingController : Controller
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ClientBorrowingController(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }
        // GET: ClientBorrowingsController
        public async Task<ActionResult> Index()
        {
            List<BorrowingStudBookViewModel> borrowings = new List<BorrowingStudBookViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/borrowing");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                borrowings = JsonConvert.DeserializeObject<List<BorrowingStudBookViewModel>>(result);
            }
            return View(borrowings);
        }

        private async Task<ActionResult> GetBorrowings(int id)
        {
            Borrowings borrowings = new Borrowings();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/Borrowing/GetBorrowings/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                borrowings = JsonConvert.DeserializeObject<Borrowings>(results); // 16
            }
            return View(borrowings);
        }


        // GET: ClientBorrowingsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetBorrowings(id);
        }

        // GET: ClientBorrowingsController/Create
        
        [HttpGet]
        public async Task<ActionResult> Create(int bookId)
        {            
            HttpContext.Session.SetInt32("bid", bookId);
            Borrowings borrowings = new Borrowings();
            borrowings.BookId = bookId;
            return View(borrowings);
        }

        // POST: ClientBorrowingsController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Borrowings borrowings)
        {
            Books books = new Books();
            var StudentId = (int)HttpContext.Session.GetInt32("Sid");
            var bId = HttpContext.Session.GetInt32("bid");
            borrowings.StudentId = StudentId;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            var response = await client.PostAsJsonAsync<Borrowings>("api/borrowing", borrowings);            
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<Books>(result);
                return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
            }
            return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
        }


        // GET: ClientBorrowingsController/Edit/5
        public async Task<ActionResult> Edit(int bookId)
        {
            HttpContext.Session.SetInt32("bid", bookId);
            return await GetBorrowings(bookId);
        }

        // POST: ClientBorrowingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Borrowings borrowings) // 16
        {
            var bid = HttpContext.Session.GetInt32("bid")?? 0;
            borrowings.BookId = bid;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.PutAsJsonAsync<Borrowings>($"api/Borrowing/UpdateBorrowings/{ borrowings.Id}", borrowings);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
            }
            return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
        }

        // GET: ClientBorrowingsController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.DeleteAsync($"api/borrowing/DeleteBorrowings/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
            }
            return RedirectToAction("BooksMediaList", "ClientBook", new { Areas = "Client" });
        }     
        
        // POST: ClientBorrowingsController/Delete/5
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
