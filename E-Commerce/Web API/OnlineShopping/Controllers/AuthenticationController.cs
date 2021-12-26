using CustomIdentityProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShoppingBL.ViewModelBL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;



        public AuthenticationController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , IConfiguration configuration, RoleManager<AppRole> roleManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // model.Role = "User";

            var UserExist = await _userManager.FindByNameAsync(model.UserName);

            if (UserExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User Already Exist" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            // await _userManager.AddToRoleAsync(user, model.Role);

            if (model.Password != model.Conform_Pwd)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Password and conform password does not match" });
            }
            else if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User Creation Failed" });

            }

           

            return Ok(new { Status = "Success", Message = "User Created Successfully" });
        }



        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var UserExist = await _userManager.FindByNameAsync(model.UserName);

            if (UserExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User Already Exist" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (model.Password != model.Conform_Pwd)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "Password and conform password does not match" });
            }
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User Creation Failed" });

            }
            if (!await _roleManager.RoleExistsAsync(UserRoleModel.Admin))
                await _roleManager.CreateAsync(new AppRole(UserRoleModel.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoleModel.user))
                await _roleManager.CreateAsync(new AppRole(UserRoleModel.user));

            if (await _roleManager.RoleExistsAsync(UserRoleModel.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoleModel.Admin);
            }

            return Ok(new { Status = "Success", Message = "User Created Successfully" });
        }





        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.UserName);
                if (user != null)
                {


                    var result =
                        await _signInManager.CheckPasswordSignInAsync
                        (user, login.Password, false);

                    if (result.Succeeded)
                    {
                        //1  --16:28
                            var userRoles = await _userManager.GetRolesAsync(user);

                        //
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        };

                        //2
                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                           
                        }

                        authClaims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

                        //

                        var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                        var token = new JwtSecurityToken(
                           issuer: _configuration["JWT:ValidIssuer"],
                           audience: _configuration["JWT:ValidAudience"],
                           expires: DateTime.UtcNow.AddHours(3),
                           claims: authClaims,
                           
                           signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));
                        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token)
                        });

                    }

                }
            }
            return Unauthorized();
        }



        














    }




}
