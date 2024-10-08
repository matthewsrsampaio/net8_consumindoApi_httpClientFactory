using CategoriasMvc.Models;
using CategoriasMvc.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace CategoriasMvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private string token = string.Empty;

        public ProdutosController(IProdutoService _produtoService, ICategoriaService _categoriaService)
        {
            this._produtoService = _produtoService;
            this._categoriaService = _categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            //extrai o token do cookie
            var result = await _produtoService.GetProdutos(ObtemTokenJwt());

            if(result is null)
                return View("Error");

            return View(result);
        }

        private string ObtemTokenJwt()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
            {
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();
            }
                
            return token;
        }
    }
}
