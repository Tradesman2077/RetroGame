using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RetroGame.Data;
using RetroGame.Models;
using RetroGame.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM()
            {
                //get last 6 games
                //Games = _db.Game.TakeLast(6).Include(u => u.Developer).Include(u => u.Platform).Include(u => u.Publisher),
                //Developers = _db.Developer,
                //Platforms = _db.Platform,
                //Publishers = _db.Publisher

                //different method to get last 6 records
                Games = _db.Game.Skip(Math.Max(0, _db.Game.Count() - 9)).Include(u => u.Developer).Include(u => u.Platform).Include(u => u.Publisher),
                Developers = _db.Developer,
                Platforms = _db.Platform,
                Publishers = _db.Publisher

            };
            
            return View(homeVm);
        }
        public IActionResult Details(int id)
        {
            Game game = _db.Game.Include(u=>u.Platform).Include(u => u.Publisher).Include(u=>u.Developer).Where(u => u.Id == id).FirstOrDefault();


            return View(game);
        }
        public IActionResult PlatformsList()
        {
            IEnumerable<Platform> platList = _db.Platform.OrderBy(u => u.Name);

            return View(platList);
        }
        public IActionResult PublishersList()
        {
            IEnumerable<Publisher> pubList = _db.Publisher.OrderBy(u => u.Name);

            return View(pubList);
        }
        public IActionResult DevelopersList()
        {
            IEnumerable<Developer> devList = _db.Developer.OrderBy(u => u.Name);

            return View(devList);
        }
        public IActionResult GamesByYear()
        {
            List<int> gameYears = _db.Game
                                  .Select(p => p.ReleaseYear)
                                  .Distinct().ToList();

            return View(gameYears);
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
