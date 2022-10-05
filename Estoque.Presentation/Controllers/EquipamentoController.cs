using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Estoque.Domain.Entities;
using Estoque.Infra.Repositories;

namespace Estoque.Presentation.Controllers
{
    public class EquipamentoController : Controller
    {
        EquipamentoRepository repository = new EquipamentoRepository();
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
            try
            {
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
        public async Task<ActionResult> Create([Bind("Id,Nome,QtdEstoque,QtdTotal,Site")] Equipamento equipamento)
        {
            try
            {
                repository.Add(equipamento);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Equipamento equipamento = repository.GetById(id);

                return View(equipamento);
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Equipamento equipamento = repository.GetById(id);

                return View(equipamento);
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Nome,QtdEstoque,QtdTotal,Site")] Equipamento equipamento)
        {
            try
            {
                Equipamento equ = repository.GetById(equipamento.Id);
                equ.Nome = equipamento.Nome;
                equ.QtdEstoque = equipamento.QtdEstoque;
                equ.QtdTotal = equipamento.QtdTotal;
                equ.Site = equipamento.Site;

                repository.Update(equ);

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
                Equipamento equipamento = repository.GetById(id);

                return View(equipamento);
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
                EquipamentoRepository repository = new EquipamentoRepository();
                var equipamento = repository.GetById(id);

                repository.Delete(equipamento);

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
