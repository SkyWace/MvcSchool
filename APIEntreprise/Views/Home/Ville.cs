using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Views.Home
{
    public class Ville
    {
        public int Id { get; set; }
        public string Nom_Ville { get; set; }
        public List<Salarie> Salaries { get; set; }
    }

}
