using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Views.Home;

namespace MVC.Controllers
{
    public class VillesController : Controller
    {
        private readonly MVCContext _context;

        public VillesController(MVCContext context)
        {
            _context = context;
        }

        // GET: Villes/Create
        public async Task<IActionResult> Index()
        {
            // Récupérer la liste des villes depuis la base de données
            var villes = await _context.Villes.ToListAsync();

            // Passer la liste des villes à la vue pour l'affichage
            return View(villes);
        }

        // POST: Villes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VilleViewModels villeViewModel)
        {
            if (ModelState.IsValid)
            {
                // Créer une nouvelle instance de Ville avec l'ID spécifié
                var ville = new Ville { Id = villeViewModel.Id, Nom_Ville = villeViewModel.Nom_Ville };

                // Ajouter la nouvelle ville à la base de données
                _context.Add(ville);
                await _context.SaveChangesAsync();

                // Rediriger vers une action de votre choix après l'ajout de la ville
                return RedirectToAction("Index", "Salaries");
            }
            return View(villeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // Trouver la ville à supprimer par son ID
            var ville = await _context.Villes.FindAsync(id);
            if (ville == null)
            {
                return NotFound();
            }

            // Vérifier s'il y a des salariés associés à cette ville
            var salarieWithVille = await _context.Salaries.AnyAsync(s => s.VilleId == id);
            if (salarieWithVille)
            {
                // S'il y a des salariés avec cette ville, rediriger avec un message d'erreur
                TempData["ErrorMessage"] = "Des salariés sont associés à cette ville. La ville ne peut pas être supprimée.";
                return RedirectToAction(nameof(Create));
            }

            try
            {
                // Supprimer la ville de la base de données
                _context.Villes.Remove(ville);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            catch (DbUpdateException /* ex */)
            {
                // Gérer l'erreur de suppression, si nécessaire
                // Loguez l'erreur ici
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

    }




}

