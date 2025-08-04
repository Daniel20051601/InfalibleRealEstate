using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;

namespace InfalibleRealEstate.Services
{
    public class SolicitudesUnirseService(IDbContextFactory<ApplicationDbContext> DbContext)
    {
        public async Task<bool> Existe(int solicitudUnirseId)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            return await contexto.SolicitudesUnirse.AnyAsync(s => s.SolicitudUnirseId == solicitudUnirseId);
        }

        public async Task<bool> Insertar(SolicitudUnirse solicitud)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            contexto.SolicitudesUnirse.Add(solicitud);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(SolicitudUnirse solicitud)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            contexto.SolicitudesUnirse.Update(solicitud);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(SolicitudUnirse solicitud)
        {
            if (await Existe(solicitud.SolicitudUnirseId))
            {
                return await Modificar(solicitud);
            }
            else
            {
                return await Insertar(solicitud);
            }
        }

        public async Task<SolicitudUnirse?> Buscar(int solicitudId)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            return await contexto.SolicitudesUnirse
                .Include(s => s.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SolicitudUnirseId == solicitudId);
        }

        public async Task<List<SolicitudUnirse>> ListarSolicitudesUnirse()
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            return await contexto.SolicitudesUnirse
                .Include(s => s.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int solicitudId)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            try
            {
                var filaAfectada = await contexto.SolicitudesUnirse
                    .Where(s => s.SolicitudUnirseId == solicitudId)
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
            return await contexto.SolicitudesUnirse.CountAsync();
        }
    }
}
