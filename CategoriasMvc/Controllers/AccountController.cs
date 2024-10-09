using CategoriasMvc.Models;
using CategoriasMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CategoriasMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAutenticacao _autenticacaoService;

        public AccountController(IAutenticacao autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Login inválido...");
                return View(model);
            }

            //Verifica as credenciais do usuário e retorna um valor
            var result = await _autenticacaoService.AutenticaUsuario(model);

            if(result is null)
            {
                ModelState.AddModelError(string.Empty, "Login inválido...");
                return View(model);
            }

            //armazenar o token no cookie
            Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(1),
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
            });

            return Redirect("/");

        }
    }
}
