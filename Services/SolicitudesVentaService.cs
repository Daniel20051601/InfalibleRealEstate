using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;

namespace InfalibleRealEstate.Services;

public class SolicitudesVentaService(IDbContextFactory<ApplicationDbContext> DbContext)
{
    public async Task<bool> Existe(int SolicitudVentaId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.SolicitudesVenta.AnyAsync(s => s.SolicitudVentaId == SolicitudVentaId);
    }


    public async Task<bool> Insertar(SolicitudVenta solicitud)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.SolicitudesVenta.Add(solicitud);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> ModificarEstado(SolicitudVenta solicitud, string Estado)
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

    public async Task<bool> Guardar(SolicitudVenta solicitud)
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

    public async Task<SolicitudVenta?> Buscar(int solicitudId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.SolicitudesVenta
            .Include(s => s.Categoria)
            .Include(s => s.Usuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SolicitudVentaId == solicitudId);
    }

    public async Task<List<SolicitudVenta>> ListarSolicitudesVenta()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.SolicitudesVenta
            .Include(s => s.Categoria)
            .Include(s => s.Usuario)
            .AsNoTracking()
            .ToListAsync();
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
