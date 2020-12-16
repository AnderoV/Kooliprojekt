using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kooliprojekt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TenantHandler.Models;

namespace TenantHandler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromServices]IFileClient fileClient)
        {
            using (var fileStream = await fileClient.GetFile("", "../Kooliprojekt/tenants.json"))
            using (var reader = new StreamReader(fileStream))
            {
                var json = reader.ReadToEnd();
                ViewData["results"] = JsonConvert.DeserializeObject(json);
                return View();
                
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(string json, [FromServices]IFileClient fileClient)
        {
            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
            var mem = new System.IO.MemoryStream(jsonBytes);
            await fileClient.SaveFile("", "../Kooliprojekt/tenants.json", mem);

            ViewData["results"] = JsonConvert.DeserializeObject(json);
            return View();
        }   

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
