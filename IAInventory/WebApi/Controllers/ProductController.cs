using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Models.DbContexts;
using WebApi.Models.Store;
using WebApi.Models.ViewModels;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class ProductController:ControllerBase
    {
        private readonly StoreDbContext dbContext;
        private readonly IJwtUtils jwtUtils;

        public ProductController(StoreDbContext dbContext, IJwtUtils jwtUtils)
        {
            this.dbContext = dbContext;
            
            this.jwtUtils = jwtUtils;
        }
        [Authorize]
        [HttpPost]
        [Route("/product")]
        public async Task<IActionResult> AddNewProduct([FromBody]ProductViewModel viewModel)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!jwtUtils.IsInAdminRole(token)) return Forbid();
            if (viewModel == null || string.IsNullOrEmpty(viewModel.Name) || viewModel.CatId == null)
                return BadRequest(new {message="Please specify product name and category"});
            var cat = await dbContext.Categories.FindAsync(viewModel.CatId);
            var product = new Product()
            {
                Category = cat,
                UnitsInStock = viewModel.UnitsInStock,
                CurrentPrice = viewModel.Price,
                ProductName = viewModel.Name,
                Description = viewModel.Description
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return Ok(product);
        }
        [Authorize]
        [HttpPut]
        [Route("/product")]
        public async Task<IActionResult> EditProduct([FromBody]ProductViewModel viewModel)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!jwtUtils.IsInAdminRole(token)) return Forbid();
            if (viewModel == null || viewModel.CatId == null) return BadRequest();
            var prod = await dbContext.Products.FindAsync(viewModel.CatId);
            if (prod == null) return NotFound(new { message = "Product not found" });
            prod.ProductName = viewModel.Name;
            prod.Description = viewModel.Description;
            prod.UnitsInStock = viewModel.UnitsInStock;
            prod.CurrentPrice = viewModel.Price;
            dbContext.Products.Update(prod);
            await dbContext.SaveChangesAsync();
            return Ok(prod);
            
        }
        //public async Task<IActionResult> ([FromBody]ProductViewModel viewModel)
        //public async Task<IActionResult> ([FromBody]ProductViewModel viewModel)
    }
}
