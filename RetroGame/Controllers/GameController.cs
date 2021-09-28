using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroGame.Data;
using RetroGame.Models;
using RetroGame.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Controllers
{
    public class GameController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GameController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> objList = _db.Game.Include(u=>u.Platform).Include(u=>u.Developer);

            //foreach (var obj in objList)
            //{
            //    obj.Developer = _db.Developer.FirstOrDefault(u => u.Id == obj.DeveloperId);
            //    obj.Platform = _db.Platform.FirstOrDefault(u => u.Id == obj.PlatformId);
            //}



            return View(objList);
        }

        //GET Upsert
        public IActionResult Upsert(int? id)
        {
            GameViewModel gameVM = new GameViewModel()
            {
                Game = new Game(),
                PlatformSelectList = _db.Platform.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                DeveloperSelectList = _db.Developer.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            
            if (id==null)
            {
                ///create
                return View(gameVM);
            }
            else
            {
                gameVM.Game = _db.Game.Find(id);
                if (gameVM.Game == null)
                {
                    return NotFound();

                }
                return View(gameVM);
            }
            
        }
        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(GameViewModel gameView)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (gameView.Game.Id ==0)
                {
                    //create
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    gameView.Game.Image = fileName + extension;

                    _db.Game.Add(gameView.Game);
                }
                else
                {
                    //update
                    var objFromDb = _db.Game.AsNoTracking().FirstOrDefault(u => u.Id == gameView.Game.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var filestream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }

                        gameView.Game.Image = fileName + extension;
                    }
                    else
                    {
                        gameView.Game.Image = objFromDb.Image;
                    }
                    _db.Game.Update(gameView.Game);
                }


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ///if empty model repopulate the lists
            gameView.DeveloperSelectList = _db.Developer.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            gameView.PlatformSelectList = _db.Platform.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(gameView);


        }
        //GET delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Game game = _db.Game.Include(u=>u.Platform).Include(u=>u.Developer).FirstOrDefault(u=>u.Id==id);


            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }
        //post delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Game.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;

            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Game.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
    
        }
    }

}
