using LaboratorioBack.Model;
using LaboratorioBack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LaboratorioBack.Data
{
    //public class ApplicationDbContex : DbContext
    public class ApplicationDbContext : IdentityDbContext // para utilizar el sistema de ususarios de ASP.NET Identity
    {
        protected ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}


        public DbSet<Estudio> Estudios { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }   
    }
}
