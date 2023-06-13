using GamesAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configiguration;

        public AutorizaController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IConfiguration configiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configiguration = configiguration;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em : "
                + DateTime.Now.ToLongDateString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var user = new IdentityUser
            {
                UserName = usuarioDTO.Email,
                Email = usuarioDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(GeraToken(usuarioDTO));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var result = await _signInManager.PasswordSignInAsync(usuarioDTO.Email,
                usuarioDTO.Password, isPersistent: false, lockoutOnFailure: false); // Se tentar mais de 3 vezes irá bloquear

            if (result.Succeeded)
            {
                return Ok(GeraToken(usuarioDTO));
            } else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido...");
                return BadRequest(ModelState);
            }
        }

        private UsuarioToken GeraToken(UsuarioDTO usuarioDTO)
        {
            // Define declaração do usuário
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDTO.Email),
                new Claim("meuPet", "Pitty"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Gera uma chave com base em um algoritmo simétrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configiguration["JWT:Key"]));

            // Gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tempo de expiração do token
            var expiracao = _configiguration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            // Classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configiguration["TokenConfiguration:Issuer"],
                audience: _configiguration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            // Retorna os dados com o token e informações
            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT Criado com sucesso"
            };
        }
    }
}
