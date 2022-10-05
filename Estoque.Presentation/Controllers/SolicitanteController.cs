using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Estoque.Domain.Entities;
using Estoque.Infra.Repositories;

namespace Estoque.Presentation.Controllers
{
    // GUSTAVO ESTEVE AQUI!!!!
    public class SolicitanteController : Controller
    {
        SolicitanteRepository repository = new SolicitanteRepository();
        public IActionResult Index()
        {
            try
            {
                return View(repository.Get());
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Nome")] Solicitante solicitante)
        {
            try
            {
                repository.Add(solicitante);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                Solicitante solicitante = repository.GetById(id);

                return View(solicitante);
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Solicitante solicitante = repository.GetById(id);

                return View(solicitante);
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Nome")] Solicitante solicitante)
        {
            try
            {
                Solicitante model = repository.GetById(id);

                model.Nome = solicitante.Nome;

                repository.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Solicitante solicitante = repository.GetById(id);

                return View(solicitante);
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Solicitante solicitante = repository.GetById(id);
                repository.Delete(solicitante);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }  
        }
    }
}
