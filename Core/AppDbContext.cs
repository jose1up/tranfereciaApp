using GestiónDeImagenIA_Back.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestiónDeImagenIA_Back.Core
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> clientes { get; set; } = null;
        public DbSet<Persona> Personas { get; set; } = null;
        public DbSet<Comprobante> Comprobantes { get; set; } = null;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasOne(C => C.comprobante)
                .WithMany()
                .HasForeignKey(c => c.id_comprobante)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
