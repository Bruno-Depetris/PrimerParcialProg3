using Microsoft.AspNetCore.Mvc;
using PrimerParcial.Models;

namespace PrimerParcial.Controllers
{
    public class ParticipantesController : Controller
    {
        private readonly DeportistaContentx _deportistaContentx;

        public ParticipantesController(DeportistaContentx deportistaContentx)
        {
            _deportistaContentx = deportistaContentx;
        }

        
        public IActionResult Index()
        {
            var participantes = _deportistaContentx.ObtenerParticipantes();

            
            var conteoPorDisciplina = participantes
                .GroupBy(p => p.Disciplina)
                .Select(g => new { Disciplina = g.Key, Cantidad = g.Count() })
                .ToList();

            ViewBag.ConteoPorDisciplina = conteoPorDisciplina;
            return View(participantes);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Deportista deportista)
        {
            if (ModelState.IsValid)
            {
                _deportistaContentx.AgregarParticipante(deportista);
                return RedirectToAction(nameof(Index));
            }
            return View(deportista);
        }

        
    }
}
