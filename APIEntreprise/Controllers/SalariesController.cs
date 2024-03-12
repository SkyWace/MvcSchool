using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Views.Home;

namespace MVC.Controllers
{
    public class SalariesController : Controller
    {
        private readonly MVCContext _context;

        public SalariesController(MVCContext context)
        {
            _context = context;
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salaries.ToListAsync());
        }

        public async Task<IActionResult> Check()
        {
            return View(await _context.Salaries.ToListAsync());
        }

        // GET: Salaries/Search
        [HttpGet]
        public IActionResult Search(string searchString)
        {
            var salaries = _context.Salaries.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                salaries = salaries.Where(s => s.Name.Contains(searchString) || s.Service.Contains(searchString));
            }

            var searchResults = salaries.Select(s => new { name = s.Name, prenom = s.Prenom, service = s.Service, localisation = s.Ville.Id }).ToList(); // Remplacer Localisation par Ville.Name

            return Json(searchResults);
        }



        // GET: Salaries/Search2




        // GET: Salries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salaries
                .Include(s => s.Ville) // Assurez-vous que la propriété Ville est incluse dans les données chargées
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }


        // GET: Salaries/Create
        // GET: Salaries/Create
        public async Task<IActionResult> Create()
        {
            // Récupérer les villes depuis la base de données et les ajouter à ViewBag.Villes
            ViewBag.Villes = new SelectList(await _context.Villes.ToListAsync(), "Id", "Nom_Ville");
            return View();
        }


        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Prenom,Email,TelephonePortable,TelephoneFix,Service,VilleId")] Salarie salarie)
        {
            if (ModelState.IsValid)
            {
                // Récupérer la ville associée à l'ID sélectionné
                salarie.Ville = await _context.Villes.FindAsync(salarie.VilleId);

                _context.Add(salarie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salarie);
        }



        // GET: Salaries/Edit/5
        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salaries
                .Include(s => s.Ville)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salarie == null)
            {
                return NotFound();
            }

            // Remplir la liste déroulante avec les villes
            var villes = await _context.Villes.ToListAsync();
            ViewData["Villes"] = new SelectList(villes, "Id", "Nom_Ville", salarie.VilleId);

            return View(salarie);
        }











        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Prenom,Email,TelephonePortable,TelephoneFix,Service,VilleId")] Salarie salarie)
        {
            if (id != salarie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Mettre à jour la propriété Ville de l'objet Salarie en fonction de la sélection du menu déroulant
                    salarie.Ville = await _context.Villes.FindAsync(salarie.VilleId);

                    _context.Update(salarie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarieExists(salarie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salarie);
        }




        private bool SalarieExists(int id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salarie = await _context.Salaries.FindAsync(id);
            _context.Salaries.Remove(salarie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateVille()
        {
            // Récupérer les noms de villes à partir de votre table Ville
            var cities = _context.Villes.Select(v => v.Id).ToList();

            // Ajouter les noms de villes existantes des salariés à la liste
            var existingCities = _context.Salaries.Select(s => s.Ville.Id).Distinct();
            cities.AddRange(existingCities);

            // Trier les noms de villes par ordre alphabétique
            cities.Sort();

            ViewData["Cities"] = new SelectList(cities);
            return View();

           
            }

    }
}
