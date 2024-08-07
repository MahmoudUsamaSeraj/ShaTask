
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShaTask.Models;
using ShaTask.Repository.BranchRepo;
using ShaTask.Repository.CasherRepo;

namespace ShaTask.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class InvoicesController : Controller
    {
        private readonly ShaTaskContext _context;
        private readonly ICasherRepo casherRepo;
        private readonly IBranchRepo branchRepo;
        public InvoicesController(ShaTaskContext context, ICasherRepo casherRepo, IBranchRepo branchRepo)
        {
            _context = context;
            this.casherRepo = casherRepo; 
            this.branchRepo = branchRepo;
        }

        
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.InvoiceHeaders.Include(i => i.Cashier).Include(i => i.InvoiceDetails).ToListAsync();
            return View(invoices);
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.InvoiceHeaders
                .Include(i => i.Cashier)
                .Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        public IActionResult Create()
        {
            ViewData["CashierID"] = new SelectList(_context.Cashiers, "ID", "CashierName");
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "BranchName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CustomerName,Invoicedate,CashierID,BranchID")] InvoiceHeader invoice, InvoiceDetail[] items)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                foreach (var item in items)
                {
                    invoice.InvoiceDetails.Add(item);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CashierID"] = new SelectList(_context.Cashiers, "ID", "CashierName", invoice.CashierID);
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "BranchName", invoice.BranchID);
            return View(invoice);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.InvoiceHeaders.Include(i => i.InvoiceDetails).FirstOrDefaultAsync(i => i.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["CashierID"] = new SelectList(_context.Cashiers, "ID", "CashierName", invoice.CashierID);
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "BranchName", invoice.BranchID);
            return View(invoice);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerName,Invoicedate,CashierID,BranchID")] InvoiceHeader invoice, InvoiceDetail[] items)
        {
            if (id != invoice.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Update(invoice);

                var existingItems = _context.InvoiceDetails.Where(i => i.InvoiceHeaderID == invoice.ID).ToList();
                foreach (var item in existingItems)
                {
                    _context.InvoiceDetails.Remove(item);
                }

                foreach (var item in items)
                {
                    invoice.InvoiceDetails.Add(item);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CashierID"] = new SelectList(_context.Cashiers, "ID", "CashierName", invoice.CashierID);
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "BranchName", invoice.BranchID);
            return View(invoice);
        }

        private bool InvoiceHeaderExists(int id)
        {
            return _context.InvoiceHeaders.Any(e => e.ID == id);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.InvoiceHeaders
                .Include(i => i.Cashier)
                .Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.InvoiceHeaders.Include(i => i.InvoiceDetails).FirstOrDefaultAsync(i => i.ID == id);
            _context.InvoiceHeaders.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        //private bool InvoiceExists(int id)
        //{
        //    return _context.InvoiceHeaders.Any(e => e.ID == id);
        //}





        [HttpGet]
        public JsonResult GetBranchByCashier(int cashierId)
        {
            //Console.WriteLine($"Received cashierId: {cashierId}");
            var cashier = casherRepo.getById(cashierId);
            if (cashier == null)
            {
                return Json(new { success = false, message = "Cashier not found" });
            }

            var branch = branchRepo.getById(cashier.BranchID);
            if (branch == null)
            {
                return Json(new { success = false, message = "Branch not found" });
            }

            return Json(new
            {
                success = true,
                BranchID = branch.ID,
                Branch = branch.BranchName
            });
        }

    }



}
