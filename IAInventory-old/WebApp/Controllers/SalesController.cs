using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DbContexts;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("{controller}/{action}/{id?}")]
    public class SalesController : Controller
    {
        private readonly StoreDbContext _context;

        public SalesController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var storeDbContext = _context.Sales;
            return View(await storeDbContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
           ViewData["Products"] = _context.Products.Select(c => new SelectListItem() { Text = c.ProductName, Value = c.ProductId.ToString() }).ToList();
           
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleViewModel sale)
        {
            if (ModelState.IsValid)
            {
                var pr =await _context.Products.FindAsync(int.Parse(sale.Products));
                var sa = new Sale()
                {
                    AmountCustomerPaid = sale.AmountCustomerPaid,
                    OrderTotal = 0,
                    OrderDate = DateTime.Now,
                    AttendantName = sale.AttendantName,
                    Customername = sale.CustomerName,

                };
               var entry= await _context.Sales.AddAsync(sa);
                var sd = new SaleDetail()
                {
                    ProductId = pr.ProductId,
                    LineItemCost = sale.Quantity *pr.CurrentPrice,
                    Sale=entry.Entity,
                    Quantity=sale.Quantity,

                };

                await _context.SaleDetails.AddAsync(sd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(sale);
        }


        [HttpGet]

        public async Task<IActionResult> GenerateReport()
        {
            var allsales = _context.SaleDetails.Include(x=>x.Sale).Include(o=>o.Product);
            double totalSales=0;
            foreach(var sale in allsales)
            {
                totalSales += sale.LineItemCost;
            }
            ViewData["totalSales"]=totalSales;
            return View(allsales);
        }




        private bool SaleExists(int id)
        {
          return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
