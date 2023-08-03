using CaddyShackMVC.DataAccess;
using CaddyShackMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CaddyShackMVC.Controllers
{
    public class GolfBagsController : Controller
    {
        private readonly CaddyShackContext _context;

        public GolfBagsController(CaddyShackContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var gBags = _context.GolfBags.ToList();
            ViewData["clubs"] = _context.Clubs.ToList();

            return View(gBags);
        }

        [Route("/golfbags/{id:int}")]
        public IActionResult Show(int id)
        {
            var gBag = _context.GolfBags.Include(e => e.Clubs).Where(e => e.Id == id).Single();
            return View(gBag);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(GolfBag gBag)
        {
            _context.GolfBags.Add(gBag);
            _context.SaveChanges();

            return Redirect($"/golfbags/{gBag.Id}");
        }

        public IActionResult Edit(int id)
        {
            var gBag = _context.GolfBags.Find(id);

            return View(gBag);
        }

        [HttpPost]
        [Route("/golfbags/{id:int}")]
        public IActionResult Update(int id, GolfBag gBag)
        {
            gBag.Id = id;
            _context.GolfBags.Update(gBag);
            _context.SaveChanges();

            return Redirect($"/golfbags/{gBag.Id}");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var gBag = _context.GolfBags.Find(id);
            _context.GolfBags.Remove(gBag);
            _context.SaveChanges();

            return Redirect("/golfbags");
        }
    }
}
