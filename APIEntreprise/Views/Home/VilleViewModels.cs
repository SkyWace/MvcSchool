using System.ComponentModel.DataAnnotations;

namespace MVC.Views.Home
{
    public class VilleViewModels
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Nom_Ville est requis.")]
        public string Nom_Ville { get; set; }
    }
}
