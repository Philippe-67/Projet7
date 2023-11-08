using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Models;
using P7CreateRestApi.Models.Authentication.Login;
using P7CreateRestApi.Models.Authentication.SignUp;
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
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IConfiguration configuration,
                                        ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            _logger.LogInformation($"Tentative d'inscription pour l'utilisateur avec l'e-mail : {registerUser.Email}");

            // check user exist
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                _logger.LogWarning($"L'utilisateur avec l'e-mail {registerUser.Email} existe déjà");
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already exists!" });
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
                    _logger.LogError("Échec de la création de l'utilisateur");
                    if (!result.Succeeded)
                    {
                        _logger.LogError("Échec de la création de l'utilisateur");

                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = result.Errors.FirstOrDefault()?.Code ?? "Error",
                            Message = "User failed to create! ",
                            Description = result.Errors.FirstOrDefault()?.Description
                        });
                    }

                }

                //add role to the user
                await _userManager.AddToRoleAsync(user, role);

                _logger.LogInformation($"Utilisateur avec l'e-mail {registerUser.Email} créé avec succès");
                return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = "User created successfully" });
            }
            else
            {
                _logger.LogError($"Le rôle {role} n'existe pas");
                return StatusCode(StatusCodes.Status500InternalServerError,
                            new Response { Status = "Error", Message = "This role does not exist!" });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            _logger.LogInformation($"Tentative de connexion pour l'utilisateur avec le nom d'utilisateur : {loginModel.Username}");

            // checking the User
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                _logger.LogInformation($"Utilisateur avec le nom d'utilisateur {loginModel.Username} authentifié avec succès");

                // claimList creation
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // we add roles to the claims list
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

            _logger.LogWarning($"Échec de l'authentification pour l'utilisateur avec le nom d'utilisateur {loginModel.Username}");
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
