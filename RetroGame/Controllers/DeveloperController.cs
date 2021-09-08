using Microsoft.AspNetCore.Mvc;
using RetroGame.Data;
using RetroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Controllers
{
    public class DeveloperController : Controller
    {

        private readonly ApplicationDbContext _db;

        public DeveloperController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Developer> objList = _db.Developer;
            return View(objList);
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }
        //post create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Developer obj)
        {
            if (ModelState.IsValid)
            {
                _db.Developer.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }
        //GET EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id ==0)
            {
                return NotFound();
            }
            var obj = _db.Developer.Find(id);

            if(obj == null)
            {
                return NotFound();
            }
           
            return View(obj);
        }
        //post edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Developer obj)
        {
            if (ModelState.IsValid)
            {
                _db.Developer.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }
        //GET delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Developer.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        //post delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Developer.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

                _db.Developer.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
    
        }
    }

}
