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
    public class ClientBookController : Controller
    {
        // GET: ClientBooksController
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ClientBookController(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Index(int id)
        {
            List<BookAuthLangViewModel> books = new List<BookAuthLangViewModel>();
            List<Borrowings> borrowings = new List<Borrowings>();
            List<Students> students = new List<Students>();

            var StudentId = Convert.ToInt32(HttpContext.Session.GetInt32("StudentId"));
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");            
            HttpResponseMessage res = await client.GetAsync("api/borrowing/");
            if (res.IsSuccessStatusCode)
            {
                var result2 = res.Content.ReadAsStringAsync().Result;
                borrowings = JsonConvert.DeserializeObject<List<Borrowings>>(result2);
            }
                return View(students);
        }

        
        private async Task<ActionResult> GetBooks(int id)
        {
            Books books = new Books();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/book/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<Books>(results);
            }
            return View(books);
        }         

       
        // GET: ClientBooksController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetBooks(id);
        }

        // GET: ClientBooksController/Create
        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }


        // POST: ClientBooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Books books)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            var response = await client.PostAsJsonAsync<Books>("api/book", books);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ClientBooksController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetBooks(id);
        }

        // POST: ClientBooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Books books)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.PutAsJsonAsync<Books>($"api/book/{books.Id}", books);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<ActionResult> GridView1()
        {
            List<BookAuthLangViewModel> books = new List<BookAuthLangViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/book");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
            }
            return View(books);
        }


        public async Task<ActionResult> GridView2()
        {
            List<BookAuthLangViewModel> books = new List<BookAuthLangViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/book");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
            }
            return View(books);
        }


        public async Task<ActionResult> BooksMediaList(string type = "")
        {
            List<BookAuthLangViewModel> books = new List<BookAuthLangViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/book");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
            }
            if (!string.IsNullOrEmpty(type))
                books = books.Where(x => x.Title.ToLower() == type.ToLower()).ToList();
            return View(books);
        }     

        public async Task<ActionResult> ReadMore(int id)
        {
            Authors authors = new Authors();
            Books books = new Books();
            //var Id = HttpContext.Session.GetInt32("Bid");
            //HttpContext.Session.SetString("StudentFirstName","students.FirstName");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/book/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                authors = JsonConvert.DeserializeObject<Authors>(results);
                books = JsonConvert.DeserializeObject<Books>(results);
            }
            return View(books);
        }


        public async Task<ActionResult> BorrowingBook()
        {
            List<BookAuthLangViewModel> books = new List<BookAuthLangViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/book");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
            }
            return View(books);
        }


        //public async Task<ActionResult> BookSorting()
        //{
        //    List<BookAuthLangViewModel> books = new List<BookAuthLangViewModel>();
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:44328/");
        //    HttpResponseMessage response = await client.GetAsync("api/book");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = response.Content.ReadAsStringAsync().Result;
        //        books = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
        //        var sort = books.OrderByDescending(s => s.Title).ToList();
        //    }
        //    return View(books);
        //}


        public async Task<ActionResult> SearchingBook(string Title)
        {
            List<BookAuthLangViewModel> books = new List<BookAuthLangViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/book/serach?title={Title}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
            }
            return View(books);
        }

       
        // GET: ClientBooksController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.DeleteAsync($"api/book/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: ClientBooksController/Delete/5
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
