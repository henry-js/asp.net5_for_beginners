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
        private readonly InstrumentalMusicManager _insMusicManager;

        public HomeController(IMusicManager musicManager, InstrumentalMusicManager insMusicManager)
        {
            _musicManager = musicManager;
            _insMusicManager = insMusicManager;
        }

        public IActionResult Index()
        {
            var musicManagerReqId = _musicManager.RequestId;
            var insMusicManagerReqId = _insMusicManager.RequestId;

            _musicManager.Notify = new Notifier();
            var songs = _musicManager.GetAllMusicThenNotify();
            return View(songs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
