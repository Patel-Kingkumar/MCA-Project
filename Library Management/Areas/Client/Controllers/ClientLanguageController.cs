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
    public class ClientLanguageController : Controller
    {
        // GET: ClientLanguagesController
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ClientLanguageController(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }
        public async Task<ActionResult> Index()
        {
            List<Languages> languages = new List<Languages>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync("api/language");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                languages = JsonConvert.DeserializeObject<List<Languages>>(result);
            }
            return View(languages);
        }

        private async Task<ActionResult> GetLanguages(int id)
        {
            Languages languages = new Languages();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/language/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                languages = JsonConvert.DeserializeObject<Languages>(results);
            }
            return View(languages);
        }


        // GET: ClientLanguagesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetLanguages(id);
        }

        // GET: ClientLanguagesController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientLanguagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Languages languages)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            var response = await client.PostAsJsonAsync<Languages>("api/language", languages);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ClientLanguagesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetLanguages(id);
        }

        // POST: ClientLanguagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Languages languages)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.PutAsJsonAsync<Languages>($"api/language/{languages.Id}", languages);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        //api/language/search? language = hindi
        public async Task<ActionResult> SearchingLanguage(string Languages)
        {
            List<BookAuthLangViewModel> language = new List<BookAuthLangViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/language/search?language={Languages}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                language = JsonConvert.DeserializeObject<List<BookAuthLangViewModel>>(result);
            }
            return View(language);
        }
        // GET: ClientLanguagesController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.DeleteAsync($"api/language/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: ClientLanguagesController/Delete/5
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
