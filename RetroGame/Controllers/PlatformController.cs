using Microsoft.AspNetCore.Mvc;
using RetroGame.Data;
using RetroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Controllers
{
    public class PlatformController : Controller
    {

        private readonly ApplicationDbContext _db;

        public PlatformController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Platform> objList = _db.Platform;
            return View(objList);
        }
    }
}
