using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Areas.StaffAccount.Models;
using WebApi.Areas.StaffAccount.ViewModels;

namespace WebApi.Areas.StaffAccount.Controllers
{
    public class AuthController : ControllerBase
    {
        private UserManager<Staff> _userManager;
        private SignInManager<Staff> _signInManager;
        private IConfiguration Configuration;

        public AuthController(UserManager<Staff> userManager,SignInManager<Staff> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = configuration;
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
                        return Ok(new { access_token = jwt });
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
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("/users")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUser user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Staff() { Email = user.Email, UserName = user.Username, FullName = user.FullName ,
                                                EmailConfirmed=true};
               var createUser=await _userManager.CreateAsync(newUser,user.Password);
                if (createUser.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/users/resetpassword")]
        public async Task<IActionResult> ResetUserPassword([FromBody] ResetUserPassword user)
        {
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
