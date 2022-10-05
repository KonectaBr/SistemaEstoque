using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Estoque.Domain.Entities;
using Estoque.Infra.Repositories;

namespace Estoque.Presentation.Controllers
{
    public class SolicitacaoController : Controller
    {
        SolicitacaoRepository repository = new SolicitacaoRepository();
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Solicitacao> lista = (IEnumerable<Solicitacao>)repository.Get();
                lista = lista.OrderByDescending(t => t.IdSolicitacao);

                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }
    }
}
