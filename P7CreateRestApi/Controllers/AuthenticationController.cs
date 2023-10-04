using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Models.Authentication.Login;
using P7CreateRestApi.Models.Authentication.SignUp;
using P7CreateRestApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace P7CreateRestApi.Controllers
{
   
        
    [Route("api/[controller]")]
        [ApiController]
        public class AuthenticationController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IConfiguration _configuration;


            public AuthenticationController(UserManager<IdentityUser> userManager,
                                            RoleManager<IdentityRole> roleManager,
                                            IConfiguration configuration)

            {
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
            }
            [HttpPost]
            public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
            {
                // check user exist
                var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
                if (userExist != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        new Response { Status = "Error", Message = "User already exist!" });
                }
                // add the user in the database
                IdentityUser user = new()
                {
                    Email = registerUser.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerUser.Username
                };
                if (await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _userManager.CreateAsync(user, registerUser.Password);
                    if (!result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "User Failes to create!" });
                    }

                    //add role to the user
                    await _userManager.AddToRoleAsync(user, role);
                    return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = "User  created successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                new Response { Status = "Error", Message = "This role doesnot exist !" });
                }
                // Assign a role
            }
            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
            {
                ///checking the User
                var user = await _userManager.FindByNameAsync(loginModel.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    /// claimList creation
                    var authClaims = new List<Claim>()
                 {
                     new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 };
                    /// we add roles to the claims liste
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var jwtToken = GetToken(authClaims);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo
                    });
                }
                return Unauthorized();
            }

            private JwtSecurityToken GetToken(List<Claim> authClaims)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                return token;
               

            }
        }
    }






