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

        // Edit a Record
        [HttpGet]
        public IActionResult Edit(int bowlerID)
        {
            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerID);
            ViewBag.Teams = _context.Teams.ToList();
            return View("AddBowler", bowler);
        }

        // Confirm Changes
        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _context.Update(b);
                _context.SaveChanges();
                return Redirect("Index");
            }
            else
            {
                ViewBag.Teams = _context.Teams.ToList();
                return View("Index");
            }
        }

        // Delete a Bowler
        [HttpGet]
        public IActionResult Delete(int bowlerID)
        {
            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerID);
            ViewBag.Teams = _context.Teams.ToList();
            return View("Delete", bowler);
        }

        // Confirm Deletion
        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            _context.Bowlers.Remove(b);
            _context.SaveChanges();
            return Redirect("Index");
        }
    }
}
