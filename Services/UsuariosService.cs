using InfalibleRealEstate.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfalibleRealEstate.Services;

public class UsuariosService(IDbContextFactory<ApplicationDbContext> DbContext)
{
    public async Task<bool> ExisteUsuario(string email)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Users.AnyAsync(u => u.Email == email);
    }
    public async Task<bool> InsertarUsuario(ApplicationUser usuario)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.Users.Add(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> ModificarUsuario(ApplicationUser usuario)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        contexto.Users.Update(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<ApplicationUser>> Listar(Expression<Func<ApplicationUser, bool>> criterio)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Users
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
