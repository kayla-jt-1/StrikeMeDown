using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StrikeMeDown.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeMeDown.Controllers
{
    public class HomeController : Controller
    {
        private Context _context { get; set; }
        
        // Constructor
        public HomeController(Context temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            // display data from the database on the index page
            List<Bowler> testList = _context.Bowlers
                .Include(x => x.Team)
                .ToList();
            return View(testList);
        }
        
        // Load the form
        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _context.Teams.ToList();
            Bowler bowler = new Bowler();
            return View(bowler); // Passes a blank bowler to the AddBowler View 
        }

        // Submit the form 
        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bowler);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
    }
}
