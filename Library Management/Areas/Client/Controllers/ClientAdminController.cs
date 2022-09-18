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
    public class ClientAdminController : Controller
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ClientAdminController(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }
        // GET: ClientAuthorsController
        public async Task<ActionResult> Index()
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

        public async Task<ActionResult> StaffInfo()
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


        private async Task<ActionResult> GetAdmins(int id)
        {
            Admins admins = new Admins();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.GetAsync($"api/admin/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                admins = JsonConvert.DeserializeObject<Admins>(results);
            }
            return View(admins);
        }



        // GET: ClientAuthorsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await GetAdmins(id);
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
        public async Task<ActionResult> Create(Admins admins)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            var response = await client.PostAsJsonAsync<Admins>("api/admin", admins);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ClientAuthorsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await GetAdmins(id);
        }

        // POST: ClientAuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Admins admins)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            HttpResponseMessage response = await client.PutAsJsonAsync<Admins>($"api/admin/{admins.Id}", admins);
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
            HttpResponseMessage response = await client.DeleteAsync($"api/admin/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
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
