using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Models.DbContexts;
using WebApi.Models.ViewModels;

namespace WebApi.Controllers
{
    [Authorize]
    public class SaleController:ControllerBase
    {
        private readonly StoreDbContext dbContext;
        private readonly UserManager<Staff> user;

        public SaleController(StoreDbContext dbContext, UserManager<Staff> user)
        {
            this.dbContext = dbContext;
            this.user = user;
        }
        public async Task<IActionResult> StartNewSale([FromBody] SaleViewModel sale)
        {
            if (sale == null) return BadRequest(new {message="Please try again."});

        }
        //public async Task<IActionResult> CloseSale([FromBody] SaleViewModel sale)
        //public async Task<IActionResult> GenerateDailySaleReport([FromBody] SaleViewModel sale)
        //public async Task<IActionResult> AdminGenerateSaleReport([FromBody] SaleViewModel sale)
    }
}
