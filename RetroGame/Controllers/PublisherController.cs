using Microsoft.AspNetCore.Mvc;
using RetroGame.Data;
using RetroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Controllers
{
    public class PublisherController : Controller
    {

        private readonly ApplicationDbContext _db;

        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Publisher> objList = _db.Publisher;
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
        public IActionResult Create(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _db.Publisher.Add(obj);
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
            var obj = _db.Publisher.Find(id);

            if(obj == null)
            {
                return NotFound();
            }
           
            return View(obj);
        }
        //post edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _db.Publisher.Update(obj);
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
            var obj = _db.Publisher.Find(id);

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
            var obj = _db.Publisher.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

                _db.Publisher.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
    
        }
    }

}
