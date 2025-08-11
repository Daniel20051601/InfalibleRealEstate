using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace InfalibleRealEstate.Services;

public class ForoService(IDbContextFactory<ApplicationDbContext> DbContext, SupabaseStorageService supabaseStorage)
{
    public async Task<bool> Existe(int foroId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Foros.AnyAsync(f => f.ForoId == foroId);
    }

    public async Task<bool> Insertar(Foros foro)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.Foros.Add(foro);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Foros foro)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.Foros.Update(foro);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Foros foro)
    {
        if (await Existe(foro.ForoId))
        {
            return await Modificar(foro);
        }
        else
        {
            return await Insertar(foro);
        }
    }

    public async Task<Foros?> Buscar(int foroId)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Foros
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.ForoId == foroId);
    }

    public async Task<List<Foros>> Listar()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Foros.AsNoTracking().OrderByDescending(f => f.FechaCreacion).ToListAsync();
    }

    public async Task<(List<Foros> foros, int TotalCount)> ListarPaginado(int pagina, int tamanoPagina, string filtro, string valorFiltro, DateTime? desde, DateTime? hasta)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();

        var query = contexto.Foros.AsQueryable();

        if(!string.IsNullOrEmpty(valorFiltro))
        {
            switch (filtro.ToLower())
            {
                case "titulo":
                    query = query.Where(f => f.Titulo.ToLower().Contains(valorFiltro));
                    break;
                case "descripcion":
                    query = query.Where(f => f.Descripcion.ToLower().Contains(valorFiltro));
                    break;
            }
        }

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

        var foros = await query
            .OrderByDescending(f => f.FechaCreacion)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .AsNoTracking()
            .ToListAsync();

        return (foros, totalCount);
    }

    public async Task<bool> Eliminar(int foroId)
    {
        await using var context = await DbContext.CreateDbContextAsync();
        var foro = await context.Foros.FindAsync(foroId);
        if (foro == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(foro.ImagenUrl))
        {
            await supabaseStorage.DeleteFile(foro.ImagenUrl);
        }

        var affectedRows = await context.Foros.Where(f => f.ForoId == foroId).ExecuteDeleteAsync();
        return affectedRows > 0;
    }

    public async Task<int> ContarForos()
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Foros.CountAsync();
    }

}
