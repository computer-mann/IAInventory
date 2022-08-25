using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Models.DbContexts;
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
        public async Task<IActionResult> StartNewSale([FromBody] SaleViewModel sale)
        {
            if (sale == null) return BadRequest(new {message="Please try again."});
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var username = jwtutils.GetUsernameFromToken(token);
            return Ok(username);

        }
        //public async Task<IActionResult> CloseSale([FromBody] SaleViewModel sale)
        //public async Task<IActionResult> GenerateDailySaleReport([FromBody] SaleViewModel sale)
        //public async Task<IActionResult> AdminGenerateSaleReport([FromBody] SaleViewModel sale)
    }
}
