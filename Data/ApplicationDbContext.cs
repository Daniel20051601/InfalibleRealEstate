using InfalibleRealEstate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfalibleRealEstate.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Propiedad> Propiedades { get; set; }
    public DbSet<PropiedadDetalle> PropiedadDetalles { get; set; }
    public DbSet<ImagenPropiedad> ImagenesPropiedad { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<EstadoPropiedad> EstadosPropiedad { get; set; }
    public DbSet<SuscripcionNoticia> SuscripcionesNoticia { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.PropiedadesPublicadas)
            .WithOne(p => p.Administrador)
            .HasForeignKey(p => p.AdministradorId)
            .OnDelete(DeleteBehavior.SetNull); 

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.Suscripciones)
            .WithOne(s => s.Usuario)
            .HasForeignKey(s => s.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.Entity<Propiedad>()
            .HasOne(p => p.Detalle)
            .WithOne(d => d.Propiedad)
            .HasForeignKey<PropiedadDetalle>(d => d.PropiedadId);

        builder.Entity<Propiedad>()
               .Property(p => p.Precio)
               .HasColumnType("decimal(18,2)");
    }
}
