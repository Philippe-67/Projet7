namespace P7CreateRestApi.Controllers
{
    using global::P7CreateRestApi.Models.Authentication.Login;
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
                        // TODO: G�n�rer un jeton de s�curit�
                        return Ok(new { Token = "VotreJetonDeSecurite" });
                    }
                }

                // L'authentification a �chou�
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }
    }
}






