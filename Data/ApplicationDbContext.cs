using InfalibleRealEstate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace InfalibleRealEstate.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Propiedad> Propiedades { get; set; }
    public DbSet<PropiedadDetalle> PropiedadDetalles { get; set; }
    public DbSet<ImagenPropiedad> ImagenesPropiedad { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<EstadoPropiedad> EstadosPropiedad { get; set; }
    public DbSet<SuscripcionNoticia> SuscripcionesNoticia { get; set; }
    public DbSet<EstadoUsuario> EstadosUsuarios { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<CarritoItem> CarritoItems { get; set; }
    public DbSet<SolicitudVenta> SolicitudesVenta { get; set; }
    public DbSet<SolicitudUnirse> SolicitudesUnirse { get; set; }
    public DbSet<Foros> Foros { get; set; }
    public DbSet<Citas> Citas { get; set; }
    public DbSet<EstadoCitas> EstadoCitas { get; set; }
    public DbSet<CitasPropiedades> CitasPropiedades { get; set; }

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

        builder.Entity<Propiedad>()
        .HasOne(p => p.Categoria)
        .WithMany(c => c.Propiedades)
        .HasForeignKey(p => p.CategoriaId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
        .HasOne(u => u.EstadoUsuario)
        .WithMany(e => e.Usuarios)
        .HasForeignKey(u => u.EstadoUsuarioId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
                .HasOne(a => a.Carrito)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Carrito>(c => c.UsuarioId);

        builder.Entity<Categoria>().HasData(
            new Categoria { CategoriaId = 1, NombreCategoria = "Apartamento", Descripcion = "Unidad de vivienda en un edificio de apartamentos." },
            new Categoria { CategoriaId = 2, NombreCategoria = "Casa", Descripcion = "Vivienda unifamiliar o adosada." },
            new Categoria { CategoriaId = 3, NombreCategoria = "Terreno", Descripcion = "Lote de tierra disponible para construcción." },
            new Categoria { CategoriaId = 4, NombreCategoria = "Local Comercial", Descripcion = "Espacio para negocios y oficinas." },
            new Categoria { CategoriaId = 5, NombreCategoria = "Villa", Descripcion = "Casa de lujo, a menudo con jardín o terreno grande." },
            new Categoria { CategoriaId = 6, NombreCategoria = "Penthouse", Descripcion = "Apartamento de dos niveles, regularmente con Jacuzzi" }
        );

        builder.Entity<EstadoPropiedad>().HasData(
            new EstadoPropiedad { EstadoPropiedadId = 1, NombreEstado = "Activa", Descripcion = "La propiedad está visible y disponible." },
            new EstadoPropiedad { EstadoPropiedadId = 3, NombreEstado = "Vendida", Descripcion = "La propiedad ha sido vendida y ya no está disponible." },
            new EstadoPropiedad { EstadoPropiedadId = 4, NombreEstado = "Alquilada", Descripcion = "La propiedad ha sido alquilada y ya no está disponible." },
            new EstadoPropiedad { EstadoPropiedadId = 6, NombreEstado = "Eliminada", Descripcion = "La propiedad ha sido eliminada del sistema." }
        );

        builder.Entity<EstadoUsuario>().HasData(
            new EstadoUsuario { EstadoUsuarioId = 1, Nombre = "Activo", Descripcion = "El usuario está activo y puede acceder al sistema." },
            new EstadoUsuario { EstadoUsuarioId = 2, Nombre = "Inactivo", Descripcion = "El usuario no puede acceder al sistema." },
            new EstadoUsuario { EstadoUsuarioId = 3, Nombre = "Suspendido", Descripcion = "El usuario ha sido suspendido temporalmente." }
        );
        builder.Entity<EstadoCitas>().HasData(
            new EstadoCitas { EstadoCitaId = 1, Nombre = "Pendiente"},
            new EstadoCitas { EstadoCitaId = 2, Nombre = "Confirmada"},
            new EstadoCitas { EstadoCitaId = 3, Nombre = "Cancelada" },
            new EstadoCitas { EstadoCitaId = 4, Nombre = "Completada" }
        );
    }
}
