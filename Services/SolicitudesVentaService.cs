using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace InfalibleRealEstate.Services;

public class SolicitudesVentaService(IDbContextFactory<ApplicationDbContext> DbContext)
{
    public async Task<bool> Existe(int SolicitudVentaId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.SolicitudesVenta.AnyAsync(s => s.SolicitudVentaId == SolicitudVentaId);
    }


    public async Task<bool> Insertar(SolicitudesVenta solicitud)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.SolicitudesVenta.Add(solicitud);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> ModificarEstado(SolicitudesVenta solicitud, string Estado)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        var solicitudExistente = await contexto.SolicitudesVenta
            .FirstOrDefaultAsync(s => s.SolicitudVentaId == solicitud.SolicitudVentaId);
        if (solicitudExistente == null)
        {
            return false;
        }
        solicitudExistente.Estado = Estado;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(SolicitudesVenta solicitud)
    {
        if (await Existe(solicitud.SolicitudVentaId))
            {
            return await ModificarEstado(solicitud, solicitud.Estado);
        }
        else
        {
            return await Insertar(solicitud);
        }
    }

    public async Task<SolicitudesVenta?> Buscar(int solicitudId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.SolicitudesVenta
            .Include(s => s.Categoria)
            .Include(s => s.Usuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SolicitudVentaId == solicitudId);
    }

    public async Task<List<SolicitudesVenta>> ListarSolicitudesVenta()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.SolicitudesVenta
            .Include(s => s.Categoria)
            .Include(s => s.Usuario)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<(List<SolicitudesVenta> Solicitudes, int TotalCount)> ListarPaginado(int pagina, int tamanoPagina, string filtro, string valorFiltro, DateTime? desde, DateTime? hasta)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        var query = contexto.SolicitudesVenta.Include(s => s.Categoria).AsQueryable();

        if (!string.IsNullOrEmpty(valorFiltro))
        {
            switch (filtro?.ToLower())
            {
                case "nombre":
                    query = query.Where(f => (f.Nombre + " " + f.Apellido).ToLower().Contains(valorFiltro));
                    break;
                case "email":
                    query = query.Where(f => f.CorreoElectronico.ToLower().Contains(valorFiltro));
                    break;
            }
        }

        if (desde.HasValue)
        {
            query = query.Where(p => p.FechaSolicitud.Date >= desde.Value.Date);
        }

        if (hasta.HasValue)
        {
            query = query.Where(p => p.FechaSolicitud.Date <= hasta.Value.Date);
        }

        var totalCount = await query.CountAsync();

        var solicitudes = await query
            .OrderByDescending(f => f.FechaSolicitud)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .AsNoTracking()
            .ToListAsync();

        return (solicitudes, totalCount);
    }
    public async Task<bool> Eliminar(int solicitudId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        try
        {
            var filaAfectada = await contexto.SolicitudesVenta
            .Where(s => s.SolicitudVentaId == solicitudId)
            .ExecuteDeleteAsync();
            return filaAfectada > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar la solicitud: {ex.Message}");
            return false;
        }
    }

    public async Task<int> ContarSolicitudes()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.SolicitudesVenta.CountAsync();
    }

}
