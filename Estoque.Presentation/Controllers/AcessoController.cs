using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Estoque.Domain.Entities;
using Estoque.Infra.Repositories;

namespace Estoque.Presentation.Controllers
{
    public class AcessoController : Controller
    {
        AcessoRepository repository = new AcessoRepository();
        public IActionResult Index() => View();

        public ActionResult Create() => View();

        public ActionResult Edit() => View();

        public ActionResult Delete() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Hora,Final")] Acesso acesso, string user, string senha)
        {
            try
            {
                HttpClient client = new HttpClient();                

                UsuarioRepository usuarioRepository = new UsuarioRepository();
                Usuario usuario = usuarioRepository.Login(user, senha);

                acesso.UsuarioId = usuario.Id;
                acesso.Usuario = usuario;

                if (usuario != null)
                {
                    List<Claim> direitosAcesso = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user),
                        new Claim(ClaimTypes.Name, user)
                    };

                    var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
                    var userPrincipal = new ClaimsPrincipal(new[] { identity });

                    await HttpContext.SignInAsync(userPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = false,
                            ExpiresUtc = DateTime.Now.AddHours(1)
                        }); ;

                    acesso.Hora = DateTime.Now;

                    repository.Add(acesso);

                    return RedirectToAction("Index", "Home");
                }               

                return View();
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,UsuarioId,Hora,Final")] Acesso acesso, string nome, string password)
        {
            try
            {
                HttpClient client = new HttpClient();

                UsuarioRepository usuarioRepository = new UsuarioRepository();
                Usuario usuario = usuarioRepository.Login(nome, password);

                Acesso model = acesso;
                repository.Update(model);

                if (User.Identity.IsAuthenticated)
                {
                    await HttpContext.SignOutAsync();
                }

                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }
    }
}
