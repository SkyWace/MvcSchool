using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Views.Home
{
    public class MVCContext : DbContext
    {
        public MVCContext(DbContextOptions<MVCContext> options)
            : base(options)
        {

        }
        public DbSet<Salarie> Salaries { get; set; }
        public DbSet<Ville> Villes { get; set; }

       


    }
}
