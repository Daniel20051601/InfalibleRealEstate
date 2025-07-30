using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using InfalibleRealEstate.Data;

namespace InfalibleRealEstate.Services;

public class PropiedadService(IDbContextFactory<ApplicationDbContext> DbContext)
{
    public async Task<bool> Existe(int PropiedadId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Propiedades.AnyAsync(p => p.PropiedadId == PropiedadId);
    }

    public async Task<bool> Insertar(Propiedad propiedad)
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

    public async Task<bool> Modificar(Propiedad propiedad)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        contexto.Propiedades.Update(propiedad);

        if (propiedad.Detalle != null)
        {
            var detalleExistente = await contexto.PropiedadDetalles
                .FirstOrDefaultAsync(d => d.PropiedadId == propiedad.PropiedadId);

            if (detalleExistente != null)
            {
                detalleExistente.Descripcion = propiedad.Detalle.Descripcion;
                detalleExistente.Habitaciones = propiedad.Detalle.Habitaciones;
                detalleExistente.Banos = propiedad.Detalle.Banos;
                detalleExistente.MetrosCuadrados = propiedad.Detalle.MetrosCuadrados;

                contexto.PropiedadDetalles.Update(detalleExistente);
            }
            else
            {
                propiedad.Detalle.PropiedadId = propiedad.PropiedadId;
                contexto.PropiedadDetalles.Add(propiedad.Detalle);
            }
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Propiedad propiedad)
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

    public async Task<Propiedad?> Buscar(int PropiedadId)
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
        return await contexto.Propiedades
            .Where(p => p.PropiedadId == propiedadId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Propiedad>> Listar(Expression<Func<Propiedad, bool>> criterio)
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

    public async Task<List<Propiedad>> GetUltimasPropiedadesAsync(int cantidad)
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

}


