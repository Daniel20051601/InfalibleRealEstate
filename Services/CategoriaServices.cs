using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfalibleRealEstate.Services;

public class CategoriaServices(IDbContextFactory<ApplicationDbContext> DbContext)
{
    public async Task<List<Categoria>> Listar(Expression<Func<Categoria, bool>> criterio)
    {
        await using var contexto = await DbContext.CreateDbContextAsync();
        return await contexto.Categorias
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
