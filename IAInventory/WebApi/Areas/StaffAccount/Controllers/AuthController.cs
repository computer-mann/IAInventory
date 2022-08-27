using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Areas.StaffAccount.ViewModels;
using WebApi.Services;

namespace WebApi.Areas.StaffAccount.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<Staff> _userManager;
        private SignInManager<Staff> _signInManager;
        private IConfiguration Configuration;
        private IJwtUtils jwtUtils;

        public AuthController(UserManager<Staff> userManager,SignInManager<Staff> signInManager,
            IConfiguration configuration, IJwtUtils jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = configuration;
            jwtUtils = jwt;
        }

        [HttpPost]
        [Route("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login login)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByNameAsync(login.Username);
                if(user==null) return NotFound(new {message="User not found"});
                if(await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    //generate jwtif (result.Succeeded)
                    {
                        var role =(await _userManager.GetRolesAsync(user)).AsEnumerable().First()=="Admin" ? "Admin" : "Employee";
                        
                        var claims = new Claim[]
                        {
                           new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString("N")),
                           new Claim(JwtRegisteredClaimNames.Email,user.Email),
                           new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                           new Claim ("Role",role)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SignInkey"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: Configuration["Jwt:Issuer"],
                            audience: Configuration["Jwt:Audience"],
                            claims: claims,
                            signingCredentials: credentials,
                            expires: DateTime.Now.AddDays(1)
                            );
                        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                        return View(jwt);
                    }
                }
                else
                {
                    return Unauthorized(new { message = "Wrong Password" });
                }
                
            }
            //jwt to the desktop app
            return BadRequest(new {message="Something went wrong. Please try again."}) ;
        }
        [Authorize]
        [HttpPost]
        [Route("/users")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUser user)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!jwtUtils.IsInAdminRole(token)) return Forbid();
            if (ModelState.IsValid)
            {
                var newUser = new Staff() { Email = user.Email, UserName = user.Username, FullName = user.FullName ,
                                                EmailConfirmed=true};
                if((await _userManager.FindByNameAsync(user.Username)) != null)
                {
                    return BadRequest(new { message = "Username exists." });
                }
               var createUser=await _userManager.CreateAsync(newUser,user.Password);
                if (createUser.Succeeded)
                {
                    return Ok(new {email=user.Email,username=user.Username});
                }
            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost]
        [Route("/users/resetpassword")]
        public async Task<IActionResult> ResetUserPassword([FromBody] ResetUserPassword user)
        {
            string token= HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!jwtUtils.IsInAdminRole(token)) return Forbid();
            if (!ModelState.IsValid)
            {
                return BadRequest();
               
            }
            var existingUser = await _userManager.FindByNameAsync(user.Username);
            if(existingUser==null) return NotFound();
            await _userManager.RemovePasswordAsync(existingUser);
           var result= await _userManager.AddPasswordAsync(existingUser, user.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(new { message = "Password changed successfully" });
        }
    }
}
