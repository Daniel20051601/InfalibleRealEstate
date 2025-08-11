using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace InfalibleRealEstate.Services
{
    public class SolicitudesUnirseService(IDbContextFactory<ApplicationDbContext> DbContext)
    {
        public async Task<bool> Existe(int solicitudUnirseId)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            return await contexto.SolicitudesUnirse.AnyAsync(s => s.SolicitudUnirseId == solicitudUnirseId);
        }

        public async Task<bool> Insertar(SolicitudesUnirse solicitud)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            contexto.SolicitudesUnirse.Add(solicitud);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(SolicitudesUnirse solicitud)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            contexto.SolicitudesUnirse.Update(solicitud);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(SolicitudesUnirse solicitud)
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

        public async Task<SolicitudesUnirse?> Buscar(int solicitudId)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            return await contexto.SolicitudesUnirse
                .Include(s => s.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SolicitudUnirseId == solicitudId);
        }

        public async Task<List<SolicitudesUnirse>> ListarSolicitudesUnirse()
        {
            await using var contexto = await DbContext.CreateDbContextAsync();
            return await contexto.SolicitudesUnirse
                .Include(s => s.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<SolicitudesUnirse> Solicitudes, int TotalCount)> ListarPaginado(int pagina, int tamanoPagina, string filtro, string valorFiltro, DateTime? desde, DateTime? hasta)
        {
            await using var contexto = await DbContext.CreateDbContextAsync();

            var query = contexto.SolicitudesUnirse.AsQueryable();

            if (!string.IsNullOrEmpty(valorFiltro))
            {
                var valorFiltroLower = valorFiltro.ToLower();
                switch (filtro?.ToLower())
                {
                    case "nombre":
                        query = query.Where(f => (f.Nombre + " " + f.Apellido).ToLower().Contains(valorFiltroLower));
                        break;
                    case "email":
                        query = query.Where(f => f.CorreoElectronico.ToLower().Contains(valorFiltroLower));
                        break;
                    case "profesion":
                        query = query.Where(f => f.Profesion.ToLower().Contains(valorFiltroLower));
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
