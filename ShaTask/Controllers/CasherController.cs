﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShaTask.Models;
using ShaTask.Repository.BranchRepo;
using ShaTask.Repository.CasherRepo;

namespace ShaTask.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class CasherController : Controller
    {
        ICasherRepo casherRepo;
        IBranchRepo branchRepo;
        public CasherController(ICasherRepo casherRepo, IBranchRepo branchRepo)
        {
            this.branchRepo = branchRepo;
            this.casherRepo = casherRepo;
        }

        public IActionResult Index()
        {
            List<Cashier> cashiers = casherRepo.getAll();
            return View(cashiers);
        }


        public IActionResult create ()
        {
            ViewData["Branches"] = branchRepo.getAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult create (Cashier cashier)
        {
            if (ModelState.IsValid)
            {
                casherRepo.add(cashier);
                casherRepo.save();
                TempData["SuccessMessage"] = "Cashier Has Been Added !";
                return RedirectToAction("Index");
            }
            ViewData["Branches"] = branchRepo.getAll();
            return View(cashier);
        }
        public IActionResult Edit(int id)
        {
        Cashier cashier = casherRepo.getById(id);
            ViewData["Branches"] = branchRepo.getAll();

            return View(cashier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cashier c)
        {
            if (ModelState.IsValid)
            {
                Cashier cashier = casherRepo.getById(c.ID);
                cashier.CashierName = c.CashierName;
                cashier.BranchID = c.BranchID;
                casherRepo.update(cashier);
                casherRepo.save();
                return RedirectToAction("Index");
            }
            ViewData["Branches"] = branchRepo.getAll();

            return View(c);
        }
        public  IActionResult Delete(int id)
        {
          
            var cashier = casherRepo.getById(id);
            if (cashier == null)
            {
                return NotFound();
            }

            return View(cashier);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Cashier c = casherRepo.getById(id);
            casherRepo.remove(c.ID);
            casherRepo.save();
            
            return RedirectToAction(nameof(Index));
        }

        private bool CashierExists(int id)
        {
            return casherRepo.any(id);
        }
  
    }
}
