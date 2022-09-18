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
    public class ClientAuthorController : Controller
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ClientAuthorController(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }
        // GET: ClientAuthorsController
        public async Task<ActionResult> Index()
        {
            List<Authors> authors = new List<Authors>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/author");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                authors = JsonConvert.DeserializeObject<List<Authors>>(result);
            }
            return View(authors);
        }


        private async Task<ActionResult> GetAuthors(int id)
        {
            Authors authors = new Authors();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/author/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                authors = JsonConvert.DeserializeObject<Authors>(results);
            }
            return View(authors);
        }



        // GET: ClientAuthorsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetAuthors(id);
        }

        // GET: ClientAuthorsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientAuthorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Authors authors)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            var response = await client.PostAsJsonAsync<Authors>("api/author", authors);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ClientAuthorsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetAuthors(id);
        }

        // POST: ClientAuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Authors authors)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.PutAsJsonAsync<Authors>($"api/author/{authors.Id}", authors);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ClientAuthorsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.DeleteAsync($"api/author/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> SearchingAuthor(string Name)
        {
            List<BookAuthLangViewModel> author = new List<BookAuthLangViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/author/serach?name={Name}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
            }
            return View(author);
        }


        // POST: ClientAuthorsController/Delete/5
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
