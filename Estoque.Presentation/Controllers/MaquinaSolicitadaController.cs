using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Estoque.Domain.Entities;
using Estoque.Infra.Repositories;

namespace Estoque.Presentation.Controllers
{
    public class MaquinaSolicitadaController : Controller
    {
        MaquinaSolicitadaRepository repository = new MaquinaSolicitadaRepository();
        public IActionResult Index()
        {
            try
            {
                return RedirectToAction("Index", "Solicitacao");
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
        public IActionResult Create([Bind("Id,QtdSolicitada,SolicitanteId,EquipamentoId,Concluida")] MaquinaSolicitada maquinaSolicitada, string nomeEquipamento, string nomeSolicitante)
        {
            try
            {
                HttpClient client = new HttpClient();

                SolicitanteRepository solicitanteRepository = new SolicitanteRepository();
                Solicitante solicitante = solicitanteRepository.ProcurarPorNome(nomeSolicitante);
                maquinaSolicitada.SolicitanteId = solicitante.Id;

                EquipamentoRepository equipamentoRepository = new EquipamentoRepository();
                Equipamento equipamento = equipamentoRepository.ProcurarPorNome(nomeEquipamento);
                maquinaSolicitada.EquipamentoId = equipamento.Id;

                if(maquinaSolicitada.Concluida == true)
                {
                    equipamento.QtdEstoque -= maquinaSolicitada.QtdSolicitada;

                    equipamentoRepository.Update(equipamento);
                }

                repository.Add(maquinaSolicitada);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Erro = "Ocorreu um Erro, contate o Administrador do Sistema!";

                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id, string nomeEquipamento, string nomeSolicitante)
        {
            MaquinaSolicitadaRepository repository = new MaquinaSolicitadaRepository();
            var maquinaSolicitada = repository.GetMaquinaSolicitada(id);

            EquipamentoRepository equipamentoRepository = new EquipamentoRepository();
            var equipamento = equipamentoRepository.GetById(maquinaSolicitada.EquipamentoId);
            nomeEquipamento = equipamento.Nome;

            SolicitanteRepository solicitanteRepository = new SolicitanteRepository();
            var solicitante = solicitanteRepository.GetById(maquinaSolicitada.SolicitanteId);
            nomeSolicitante = solicitante.Nome;

            if (maquinaSolicitada.Concluida == true)
            {
                equipamento.QtdEstoque -= maquinaSolicitada.QtdSolicitada;

                equipamentoRepository.Update(equipamento);
            }

            return View(maquinaSolicitada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,QtdSolicitada,SolicitanteId,EquipamentoId,Concluida")] MaquinaSolicitada maquinaSolicitada, string nomeEquipamento, string nomeSolicitante)
        {
            EquipamentoRepository equipamentoRepository = new EquipamentoRepository();
            Equipamento equipamento = equipamentoRepository.ProcurarPorNome(nomeEquipamento);
            maquinaSolicitada.EquipamentoId = equipamento.Id;

            SolicitanteRepository solicitanteRepository = new SolicitanteRepository();
            Solicitante solicitante = solicitanteRepository.ProcurarPorNome(nomeSolicitante);
            maquinaSolicitada.SolicitanteId = solicitante.Id;

            MaquinaSolicitadaRepository repository = new MaquinaSolicitadaRepository();
            repository.Update(maquinaSolicitada);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            MaquinaSolicitadaRepository repository = new MaquinaSolicitadaRepository();
            var maquinaSolicitada = repository.GetMaquinaSolicitada(id);

            return View(maquinaSolicitada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            MaquinaSolicitadaRepository repository = new MaquinaSolicitadaRepository();
            var maquinaSolicitada = repository.GetMaquinaSolicitada(id);

            if(maquinaSolicitada != null)
            {
                repository.Delete(maquinaSolicitada);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
