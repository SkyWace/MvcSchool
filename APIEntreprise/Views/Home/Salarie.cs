using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Views.Home
{
    public class Salarie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string? TelephonePortable { get; set; }
        public string? TelephoneFix { get; set; }
        public string Service { get; set; }
        public int VilleId { get; set; }
        public Ville Ville { get; set; }
        // Propriété de navigation pour accéder à la ville associée
    }
}
