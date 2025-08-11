using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using InfalibleRealEstate.Data;

namespace InfalibleRealEstate.Services;

public class PropiedadService(IDbContextFactory<ApplicationDbContext> DbContext, StorageService supabaseStorage)
{
    public async Task<bool> Existe(int PropiedadId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Propiedades.AnyAsync(p => p.PropiedadId == PropiedadId);
    }

    public async Task<bool> Insertar(Propiedades propiedad)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        contexto.Propiedades.Add(propiedad);

        if (propiedad.Detalle != null)
        {
            propiedad.Detalle.PropiedadId = propiedad.PropiedadId;
            contexto.PropiedadDetalles.Add(propiedad.Detalle);
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Propiedades propiedad)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        var propiedadExistente = await contexto.Propiedades
            .Include(p => p.Detalle)
            .FirstOrDefaultAsync(p => p.PropiedadId == propiedad.PropiedadId);

        if (propiedadExistente == null)
        {
            return false;
        }

        contexto.Entry(propiedadExistente).CurrentValues.SetValues(propiedad);


        if (propiedad.Detalle != null)
        {
            if (propiedadExistente.Detalle != null)
            {
                contexto.Entry(propiedadExistente.Detalle).CurrentValues.SetValues(propiedad.Detalle);
            }
            else
            {
                propiedadExistente.Detalle = propiedad.Detalle;
                contexto.PropiedadDetalles.Add(propiedadExistente.Detalle);
            }
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Propiedades propiedad)
    {
        if (await Existe(propiedad.PropiedadId))
        {
            return await Modificar(propiedad);
        }
        else
        {
            return await Insertar(propiedad);
        }
    }

    public async Task<Propiedades?> Buscar(int PropiedadId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Propiedades
            .Include(p => p.Detalle)
            .Include(i => i.Imagenes)
            .Include(c => c.Categoria)
            .Include(e => e.EstadoPropiedad)
            .Include(a => a.Administrador)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PropiedadId == PropiedadId);
    }

    public async Task<bool> Eliminar(int propiedadId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        var propiedad = await contexto.Propiedades
            .Include(p => p.Imagenes)
            .FirstOrDefaultAsync(p => p.PropiedadId == propiedadId);

        if (propiedad == null)
        {
            return false; 
        }

        if (propiedad.Imagenes != null && propiedad.Imagenes.Any())
        {
            var tareasEliminacion = new List<Task>();
            foreach (var imagen in propiedad.Imagenes)
            {
                tareasEliminacion.Add(supabaseStorage.DeleteFile(imagen.UrlImagen));
            }
            await Task.WhenAll(tareasEliminacion);
        }
        contexto.Propiedades.Remove(propiedad);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Propiedades>> Listar(Expression<Func<Propiedades, bool>> criterio)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Propiedades
            .Include(p => p.Detalle)
            .Include(i => i.Imagenes)
            .Include(c => c.Categoria)
            .Include(e => e.EstadoPropiedad)
            .Include(a => a.Administrador)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<(List<Propiedades> Propiedades, int TotalCount)> ListarPaginado(
       int pagina,
       int tamanoPagina,
       string? filtro = null,
       string? valorFiltro = null,
       DateTime? desde = null,
       DateTime? hasta = null)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        var query = contexto.Propiedades
                            .Include(p => p.Detalle)
                            .Include(p => p.Imagenes)
                            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(valorFiltro))
        {
            var valorFiltroLower = valorFiltro.ToLower();
            switch (filtro)
            {
                case "Titulo":
                    query = query.Where(p => p.Titulo.ToLower().Contains(valorFiltroLower));
                    break;
                case "Ciudad":
                    query = query.Where(p => p.Ciudad != null && p.Ciudad.ToLower().Contains(valorFiltroLower));
                    break;
                case "Transaccion":
                    query = query.Where(p => p.TipoTransaccion != null && p.TipoTransaccion.ToLower().Contains(valorFiltroLower));
                    break;
            }
        }

        if (desde.HasValue && hasta.HasValue && desde.Value <= hasta.Value)
        {
            var desdeUtc = desde.Value.ToUniversalTime().Date;
            var hastaUtc = hasta.Value.ToUniversalTime().Date.AddDays(1).AddTicks(-1);
            query = query.Where(p => p.FechaPublicacion >= desdeUtc && p.FechaPublicacion <= hastaUtc);
        }
        else if (desde.HasValue)
        {
            var desdeUtc = desde.Value.ToUniversalTime().Date;
            query = query.Where(p => p.FechaPublicacion >= desdeUtc);
        }
        else if (hasta.HasValue)
        {
            var hastaUtc = hasta.Value.ToUniversalTime().Date.AddDays(1).AddTicks(-1);
            query = query.Where(p => p.FechaPublicacion <= hastaUtc);
        }

        var totalCount = await query.CountAsync();

        var propiedades = await query
            .OrderByDescending(p => p.FechaPublicacion)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync();

        return (propiedades, totalCount);
    }


    public async Task<List<Propiedades>> GetUltimasPropiedadesAsync(int cantidad)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Propiedades
            .Include(p => p.Detalle)
            .Include(p => p.Imagenes)
            .Include(p => p.Categoria)
            .Include(p => p.EstadoPropiedad)
            .Include(p => p.Administrador)
            .OrderByDescending(p => p.FechaPublicacion)
            .Take(cantidad)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> AgregarImagen(ImagenPropiedad imagen)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        var propiedadExiste = await contexto.Propiedades.AnyAsync(p => p.PropiedadId == imagen.PropiedadId);

        if (!propiedadExiste)
        {
            return false;
        }

        contexto.ImagenesPropiedad.Add(imagen);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<int> ContarPropiedades()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Propiedades.CountAsync();
    }

    public async Task<bool> EliminarImagen(int imagenId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        var imagen = await contexto.ImagenesPropiedad.FindAsync(imagenId);
        if (imagen == null)
        {
            return false;
        }
        contexto.ImagenesPropiedad.Remove(imagen);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<int> ContarImagenes(int propiedadId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.ImagenesPropiedad.CountAsync(i => i.PropiedadId == propiedadId);
    }

    public async Task<List<EstadoPropiedades>> ListarEstados()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.EstadosPropiedad.ToListAsync();
    }

}


