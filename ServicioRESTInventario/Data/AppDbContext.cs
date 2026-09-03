using Microsoft.EntityFrameworkCore;
using ServicioRESTInventario.Models;

namespace ServicioRESTInventario.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovimientoInventario>(entity =>
            {
                entity.ToTable("Movimiento_Inventario");

                entity.HasKey(e => e.IdMovimiento);

                entity.Property(e => e.TipoMovimiento)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.Cantidad)
                      .IsRequired();

                entity.Property(e => e.FechaMovimiento)
                      .IsRequired();

                entity.Property(e => e.Observacion)
                      .HasMaxLength(250);

                entity.Property(e => e.Estado)
                      .HasColumnType("bit");
            });
        }
    }
} 