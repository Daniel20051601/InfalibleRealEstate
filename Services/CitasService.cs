using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;

namespace InfalibleRealEstate.Services;

public class CitasService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
{
    public async Task<bool> Existe(int CitaId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Citas.AnyAsync(c => c.CitaId == CitaId);
    }

    public async Task<Citas?> CrearCitaConPropiedades(Citas cita, List<int> propiedadesIds)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            context.Citas.Add(cita);
            await context.SaveChangesAsync();

            foreach (var propId in propiedadesIds)
            {
                var citaPropiedad = new CitasPropiedades
                {
                    CitaId = cita.CitaId,
                    PropiedadId = propId
                };
                context.CitasPropiedades.Add(citaPropiedad);
            }
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
            return cita;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return null;
        }
    }

    public async Task<bool> Modificar(Citas cita)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        context.Update(cita);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Insertar(Citas cita)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        context.Citas.Add(cita);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Citas cita)
    {
        if (await Existe(cita.CitaId))
        {
            return await Modificar(cita);
        }
        else
        {
            return await Insertar(cita);
        }
    }

    public async Task<Citas?> Buscar(int CitaId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Citas
            .Include(c => c.Usuario)
            .Include(c => c.EstadoCita)
            .Include(c => c.CitaPropiedades)
                .ThenInclude(cp => cp.Propiedad)
            .FirstOrDefaultAsync(c => c.CitaId == CitaId);
    }

    public async Task<List<Citas>> Listar()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Citas
            .Include(c => c.Usuario)
            .Include(c => c.EstadoCita)
            .Include(c => c.CitaPropiedades)
                .ThenInclude(cp => cp.Propiedad)
                    .ThenInclude(p => p.Imagenes)
            .ToListAsync();
    }

    public async Task<(List<Citas> Citas, int TotalCount)> ListarPaginado(int pagina, int tamanoPagina, DateTime? desde, DateTime? hasta)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var query = context.Citas
                            .AsQueryable();

        if (desde.HasValue && hasta.HasValue && desde.Value <= hasta.Value)
        {
            var desdeUtc = desde.Value.ToUniversalTime().Date;
            var hastaUtc = hasta.Value.ToUniversalTime().Date.AddDays(1).AddTicks(-1);
            query = query.Where(p => p.FechaCreacion >= desdeUtc && p.FechaCreacion <= hastaUtc);
        }
        else if (desde.HasValue)
        {
            var desdeUtc = desde.Value.ToUniversalTime().Date;
            query = query.Where(p => p.FechaCreacion >= desdeUtc);
        }
        else if (hasta.HasValue)
        {
            var hastaUtc = hasta.Value.ToUniversalTime().Date.AddDays(1).AddTicks(-1);
            query = query.Where(p => p.FechaCreacion <= hastaUtc);
        }

        var totalCount = await query.CountAsync();

        var citas = await query
            .OrderByDescending(c => c.FechaHora)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .Include(c => c.Usuario)
            .Include(c => c.EstadoCita)
            .Include(c => c.CitaPropiedades)
                .ThenInclude(cp => cp.Propiedad)
                    .ThenInclude(p => p.Imagenes)
            .ToListAsync();

        return (citas, totalCount);
    }

    public async Task<(List<Citas> Citas, int TotalCount)> ListarPaginadoPorUsuarioAsync(string usuarioId, int pagina, int tamanoPagina, DateTime? desde, DateTime? hasta)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        var query = context.Citas
            .Where(c => c.UsuarioId == usuarioId)
            .AsQueryable();

        if (desde.HasValue && hasta.HasValue && desde.Value <= hasta.Value)
        {
            var desdeUtc = desde.Value.ToUniversalTime().Date;
            var hastaUtc = hasta.Value.ToUniversalTime().Date.AddDays(1).AddTicks(-1);
            query = query.Where(p => p.FechaCreacion >= desdeUtc && p.FechaCreacion <= hastaUtc);
        }
        else if (desde.HasValue)
        {
            var desdeUtc = desde.Value.ToUniversalTime().Date;
            query = query.Where(p => p.FechaCreacion >= desdeUtc);
        }
        else if (hasta.HasValue)
        {
            var hastaUtc = hasta.Value.ToUniversalTime().Date.AddDays(1).AddTicks(-1);
            query = query.Where(p => p.FechaCreacion <= hastaUtc);
        }

        var totalCount = await query.CountAsync();

        var citas = await query
            .OrderByDescending(c => c.FechaHora)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .Include(c => c.Usuario)
            .Include(c => c.EstadoCita)
            .Include(c => c.CitaPropiedades)
                .ThenInclude(cp => cp.Propiedad)
                    .ThenInclude(p => p.Imagenes)
            .ToListAsync();
        return (citas, totalCount);
    }

    public async Task<bool> ActualizarEstado(int citaId, int nuevoEstadoId)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Citas
            .Where(c => c.CitaId == citaId)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.EstadoCitaId, nuevoEstadoId)) > 0;
    }

    public async Task<List<EstadoCitas>> ListarEstados()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.EstadoCitas.ToListAsync();
    }

    public async Task<int> CitasPendientes()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Citas.CountAsync(c => c.EstadoCitaId == 1);
    }

    public async Task<int> CitasCompletadas()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Citas.CountAsync(c => c.EstadoCitaId == 4);
    }
}
