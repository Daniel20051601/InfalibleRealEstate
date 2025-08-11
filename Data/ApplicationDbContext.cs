using InfalibleRealEstate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace InfalibleRealEstate.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Propiedades> Propiedades { get; set; }
    public DbSet<PropiedadDetalle> PropiedadDetalles { get; set; }
    public DbSet<ImagenPropiedad> ImagenesPropiedad { get; set; }
    public DbSet<Categorias> Categorias { get; set; }
    public DbSet<EstadoPropiedades> EstadosPropiedad { get; set; }
    public DbSet<SuscripcionesNoticia> SuscripcionesNoticia { get; set; }
    public DbSet<EstadoUsuarios> EstadosUsuarios { get; set; }
    public DbSet<Carritos> Carritos { get; set; }
    public DbSet<CarritoItems> CarritoItems { get; set; }
    public DbSet<SolicitudesVenta> SolicitudesVenta { get; set; }
    public DbSet<SolicitudesUnirse> SolicitudesUnirse { get; set; }
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

        builder.Entity<Propiedades>()
            .HasOne(p => p.Detalle)
            .WithOne(d => d.Propiedad)
            .HasForeignKey<PropiedadDetalle>(d => d.PropiedadId);

        builder.Entity<Propiedades>()
               .Property(p => p.Precio)
               .HasColumnType("decimal(18,2)");

        builder.Entity<Propiedades>()
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
                .HasForeignKey<Carritos>(c => c.UsuarioId);

        builder.Entity<Categorias>().HasData(
            new Categorias { CategoriaId = 1, NombreCategoria = "Apartamento", Descripcion = "Unidad de vivienda en un edificio de apartamentos." },
            new Categorias { CategoriaId = 2, NombreCategoria = "Casa", Descripcion = "Vivienda unifamiliar o adosada." },
            new Categorias { CategoriaId = 3, NombreCategoria = "Terreno", Descripcion = "Lote de tierra disponible para construcción." },
            new Categorias { CategoriaId = 4, NombreCategoria = "Local Comercial", Descripcion = "Espacio para negocios y oficinas." },
            new Categorias { CategoriaId = 5, NombreCategoria = "Villa", Descripcion = "Casa de lujo, a menudo con jardín o terreno grande." },
            new Categorias { CategoriaId = 6, NombreCategoria = "Penthouse", Descripcion = "Apartamento de dos niveles, regularmente con Jacuzzi" }
        );

        builder.Entity<EstadoPropiedades>().HasData(
            new EstadoPropiedades { EstadoPropiedadId = 1, NombreEstado = "Activa", Descripcion = "La propiedad está visible y disponible." },
            new EstadoPropiedades { EstadoPropiedadId = 3, NombreEstado = "Vendida", Descripcion = "La propiedad ha sido vendida y ya no está disponible." },
            new EstadoPropiedades { EstadoPropiedadId = 4, NombreEstado = "Alquilada", Descripcion = "La propiedad ha sido alquilada y ya no está disponible." },
            new EstadoPropiedades { EstadoPropiedadId = 6, NombreEstado = "Eliminada", Descripcion = "La propiedad ha sido eliminada del sistema." }
        );

        builder.Entity<EstadoUsuarios>().HasData(
            new EstadoUsuarios { EstadoUsuarioId = 1, Nombre = "Activo", Descripcion = "El usuario está activo y puede acceder al sistema." },
            new EstadoUsuarios { EstadoUsuarioId = 2, Nombre = "Inactivo", Descripcion = "El usuario no puede acceder al sistema." },
            new EstadoUsuarios { EstadoUsuarioId = 3, Nombre = "Suspendido", Descripcion = "El usuario ha sido suspendido temporalmente." }
        );
        builder.Entity<EstadoCitas>().HasData(
            new EstadoCitas { EstadoCitaId = 1, Nombre = "Pendiente"},
            new EstadoCitas { EstadoCitaId = 2, Nombre = "Confirmada"},
            new EstadoCitas { EstadoCitaId = 3, Nombre = "Cancelada" },
            new EstadoCitas { EstadoCitaId = 4, Nombre = "Completada" }
        );
    }
}
