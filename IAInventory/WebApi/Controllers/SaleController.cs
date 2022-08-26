using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Models;
using WebApi.Models.DbContexts;
using WebApi.Models.Store;
using WebApi.Models.ViewModels;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    public class SaleController:ControllerBase
    {
        private readonly StoreDbContext dbContext;
        private readonly UserManager<Staff> user;
        private readonly IJwtUtils jwtutils;

        public SaleController(StoreDbContext dbContext, IJwtUtils jwtutils)
        {
            this.dbContext = dbContext;
            this.jwtutils = jwtutils;
        }

        [HttpPost]
        [Route("/sales")]
        public async Task<IActionResult> NewSale([FromBody] SaleViewModel sale)
        {
            if (sale == null) return BadRequest(new {message="Please try again."});
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var username = jwtutils.GetUsernameFromToken(token);
            var till = await dbContext.Tills.FindAsync(sale.TillId);
            var sales = new Sale()
            {
                Customername = sale.CustomerName,
                Till = till,
                OrderDate = DateTime.Now,
                AmountCustomerPaid = sale.AmountCustomerPaid,
                OrderTotal = sale.TotalCost,
                AttendantName = username
            };
            var saleEntry=await dbContext.Sales.AddAsync(sales);
            
            var detailSales=new List<SaleDetail>();
            foreach(var detail in sale.Details)
            {
                var pr=await dbContext.Products.FindAsync(detail.ProductId);
                detailSales.Add(new SaleDetail()
                {
                    Sale = saleEntry.Entity,
                    Product = pr,
                    LineItemCost = detail.UnitCost * detail.Units,
                    Quantity = detail.Units
                });
            }
            dbContext.SaleDetails.AddRange(detailSales);    
            await dbContext.SaveChangesAsync();
            return Ok(sales);

        }


        [HttpGet]
        [Route("/sales/{saleId}")]
        public async Task<IActionResult> GetSaleDetails(int? saleId)
        {
            if (saleId.HasValue) return BadRequest(new {message="Provide sale details"});
            var saleDetails = await dbContext.SaleDetails.Where(x => x.SaleId == saleId).ToListAsync();
            if (saleDetails.Any())
            {
                return Ok(saleDetails);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("/till")]
        public async Task<IActionResult> OpenTill()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var username = jwtutils.GetUsernameFromToken(token);
            var till = new Till() { TillDate = DateTime.Now.Date, Status = "Open", TillOpenedBy = username };
            var entry=await dbContext.Tills.AddAsync(till);
            await dbContext.SaveChangesAsync();
            return Ok(new { tillId = entry.Entity.Id });
        }


        [HttpPut]
        [Route("/till/{tillId}")]
        public async Task<IActionResult> CloseTill(int? tillId)
        {
            if (!tillId.HasValue) return BadRequest();
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var username = jwtutils.GetUsernameFromToken(token);
            var till = await dbContext.Tills.FindAsync(tillId);
            if(till == null) return NotFound( new { message = "Till not found" });
            if(till.Status=="Closed") return NoContent();
            till.Status = "Closed";
            dbContext.Tills.Update(till);
            await dbContext.SaveChangesAsync();
            return Ok(till);


        }

        //public async Task<IActionResult> Sale([FromBody] SaleViewModel sale)
        [HttpGet]
        [Route("/sales/dailyreport")]
        public async Task<IActionResult> GenerateDailySaleReport([FromBody]string dateString)
        {
            DateTime date=DateTime.Parse(dateString);
            var saleForDate = await dbContext.Sales.Where(x => x.OrderDate == date).ToListAsync();
            var allSaleDet=new List<SaleDetail>();
            foreach(var sale in saleForDate)
            {
                allSaleDet.AddRange(await dbContext.SaleDetails.Where(x => x.SaleId == sale.Id).ToListAsync());
            }
            return Ok(allSaleDet);
        }
        //public async Task<IActionResult> AdminGenerateSaleReport([FromBody] SaleViewModel sale)
    }
}
