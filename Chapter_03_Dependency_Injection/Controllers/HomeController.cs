using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Chapter_03_Dependency_Injection.DataManager;
using Chapter_03_Dependency_Injection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chapter_03_Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMusicManager _musicManager;

        public HomeController(IMusicManager musicManager)
        {
            _musicManager = musicManager;
        }

        public IActionResult Index()
        {
            var songs = _musicManager.GetAllMusic();
            return View(songs);
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
