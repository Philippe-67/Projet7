namespace P7CreateRestApi.Controllers
{
    using global::P7CreateRestApi.Models.Authentication.Login;
    //    [ApiController]
    //    [Route("[controller]")]
    //    public class LoginController : ControllerBase
    //    {
    //        [HttpPost]
    //        [Route("login")]
    //        public async Task<IActionResult> Login([FromBody] LoginModel model)
    //        {
    //            //TODO: implement the UserManager from Identity to validate User and return a security token.
    //            return Unauthorized();
    //        }
    //    }
    //}
    //    [ApiController]
    //    [Route("[controller]")]
    //    public class LoginController : ControllerBase
    //    {
    //        private readonly UserManager<IdentityUser> _userManager;
    //        private readonly SignInManager<IdentityUser> _signInManager;
    //        private readonly IConfiguration _configuration;

    //        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    //        {
    //            _userManager = userManager;
    //            _signInManager = signInManager;
    //            _configuration = configuration;
    //        }

    //        [HttpPost("login")]
    //        public async Task<IActionResult> Login([FromBody] LoginModel model)
    //        {
    //            var user = await _userManager.FindByNameAsync(model.UserName);
    //            if (user != null)
    //            {
    //                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

    //                if (result.Succeeded)
    //                {
    //                    Générer le jeton JWT
    //                    var token = GenerateJwtToken(user);

    //                    Retourner le jeton dans la réponse
    //                    return Ok(new { Token = token });
    //                }
    //            }

    //            L'authentification a échoué
    //            return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
    //        }

    //        private string GenerateJwtToken(IdentityUser user)
    //        {
    //            var claims = new List<Claim>
    //            {
    //                new Claim(ClaimTypes.NameIdentifier, user.Id),
    //                new Claim(ClaimTypes.Name, user.UserName),
    //                 Ajoutez d'autres revendications nécessaires
    //            };

    //            var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();
    //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
    //            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    //            var expires = DateTime.Now.AddMinutes(jwtSettings.ExpireMinutes);

    //            var token = new JwtSecurityToken(
    //                jwtSettings.Issuer,
    //                jwtSettings.Audience,
    //                claims,
    //                expires: expires,
    //                signingCredentials: creds
    //            );

    //            return new JwtSecurityTokenHandler().WriteToken(token);
    //        }
    //    }
    //}
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    // using P7CreateRestApi.Models;
    using System.Threading.Tasks;

    namespace P7CreateRestApi.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class LoginController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly SignInManager<IdentityUser> _signInManager;

            public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginModel model)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        // TODO: Générer un jeton de sécurité
                        return Ok(new { Token = "VotreJetonDeSecurite" });
                    }
                }

                // L'authentification a échoué
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }
    }
}






