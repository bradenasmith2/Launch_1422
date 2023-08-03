using CaddyShackMVC.DataAccess;
using Microsoft.AspNetCore.Mvc;
using CaddyShackMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CaddyShackMVC.Controllers
{
    public class ClubsController : Controller
    {
        private readonly CaddyShackContext _context;

        public ClubsController(CaddyShackContext context)
        {
            _context = context;
        }

        [Route("/golfbags/{id:int}/clubs/new")]
        public IActionResult New(int id)
        {
            var gBag = _context.GolfBags.Include(gBag => gBag.Clubs).Where(e => e.Id == id).Single();

            return View(gBag);
        }

        [HttpPost]
        [Route("/golfbags/{id:int}/clubs")]
        public IActionResult Index(int id, Club club)
        {
            var gBag = _context.GolfBags.Include(gBag => gBag.Clubs).Where(e => e.Id == id).Single();

            gBag.Clubs.Add(club);
            _context.SaveChanges();

            return Redirect($"/golfbags/{gBag.Id}"); 
        }
    }
}
