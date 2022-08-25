using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Models.DbContexts;
using WebApi.Models.Store;
using WebApi.Models.ViewModels;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class CategoryController:ControllerBase
    {
        

        private readonly StoreDbContext dbContext;
        private IJwtUtils jwtUtils;
        public CategoryController(StoreDbContext dbContext,UserManager<Staff> user,IJwtUtils jwtUtils)
        {
            this.dbContext = dbContext;
            
            this.jwtUtils = jwtUtils;
        }
        [Authorize]
        [HttpPost]
        [Route("/categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryViewModel category)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!jwtUtils.IsInAdminRole(token)) return Forbid();

            if (string.IsNullOrEmpty(category.Name))
            {
                return BadRequest(new { message = "Category name field is empty" });
            }
            var cat = new Category() { CategoryName = category.Name, Description = category.Description };
            dbContext.Categories.Add(cat);
            await dbContext.SaveChangesAsync();
            return Ok(new {message="Category Added"});
        }
        [Authorize]
        [HttpPost]
        [Route("/categories/{categoryId}")]
        public async Task<IActionResult> RemoveCategory(int? categoryId)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!jwtUtils.IsInAdminRole(token)) return Forbid();
            if (!categoryId.HasValue) return BadRequest(new { message = "something went wrong" });
            var cat = await dbContext.Categories.FindAsync(categoryId);
            if(cat == null) return NotFound();
            dbContext.Categories.Remove(cat);
            await dbContext.SaveChangesAsync();
            return Ok();

        }
       
        [Authorize]
        [HttpGet]
        [Route("/categories/{categoryId}")]
        public async Task<IActionResult> GetAllProductsInACategory(int? categoryId)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!jwtUtils.IsInAdminRole(token)) return Forbid();
            if (!categoryId.HasValue) return BadRequest(new { message = "something went wrong" });
            if((await dbContext.Categories.FindAsync(categoryId)) == null) return NotFound(new {message="Category not found"});
            var productsInCat =await dbContext.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            if (productsInCat.Any())
            {
                return Ok(productsInCat);
            }
            return NoContent();
        }


    }
}
