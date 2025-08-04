using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace InfalibleRealEstate.Services
{
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
}
