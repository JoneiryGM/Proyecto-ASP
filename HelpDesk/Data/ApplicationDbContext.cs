using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<HelpDesk.Models.UserData> UserData { get; set; }

        public DbSet<HelpDesk.Models.SolicitudServicio> SolicitudServicio { get; set; }

        public DbSet<HelpDesk.Models.EstadoSolicitud> EstadoSolicitud { get; set; }

        public DbSet<HelpDesk.Models.TecnicoAsignado> TecnicoAsignado { get; set; }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        //public DbSet<IdentityRole> identityRole { get; set; }
    }
}
