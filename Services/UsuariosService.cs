using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfalibleRealEstate.Services;

public class UsuariosService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    public async Task<bool> ExisteUsuario(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> InsertarUsuario(ApplicationUser usuario)
    {
        context.Users.Add(usuario);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ModificarUsuario(ApplicationUser usuario)
    {
        var usuarioExistente = await context.Users.FindAsync(usuario.Id);
        if (usuarioExistente == null)
        {
            return false;
        }
        context.Entry(usuarioExistente).CurrentValues.SetValues(usuario);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<(bool Exito, string? Error)> ModificarUsuarioConRol(ApplicationUser usuario, string nuevoRol)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var usuarioExistente = await userManager.FindByIdAsync(usuario.Id);
            if (usuarioExistente == null)
            {
                return (false, "Usuario no encontrado.");
            }
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.PhoneNumber = usuario.PhoneNumber;
            usuarioExistente.EstadoUsuarioId = usuario.EstadoUsuarioId;

            var updateResult = await userManager.UpdateAsync(usuarioExistente);
            if (!updateResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return (false, string.Join(", ", updateResult.Errors.Select(e => e.Description)));
            }

            if (!string.IsNullOrEmpty(nuevoRol) && !await roleManager.RoleExistsAsync(nuevoRol))
            {
                await transaction.RollbackAsync();
                return (false, $"El rol '{nuevoRol}' no existe.");
            }

            var rolesActuales = await userManager.GetRolesAsync(usuarioExistente);
            var removeResult = await userManager.RemoveFromRolesAsync(usuarioExistente, rolesActuales);
            if (!removeResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return (false, string.Join(", ", removeResult.Errors.Select(e => e.Description)));
            }

            if (!string.IsNullOrEmpty(nuevoRol))
            {
                var addResult = await userManager.AddToRoleAsync(usuarioExistente, nuevoRol);
                if (!addResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return (false, string.Join(", ", addResult.Errors.Select(e => e.Description)));
                }
            }

            await transaction.CommitAsync();
            return (true, null);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, ex.Message);
        }
    }

    public async Task<List<ApplicationUser>> Listar(Expression<Func<ApplicationUser, bool>> criterio)
    {
        return await context.Users
            .Include(u => u.EstadoUsuario)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ApplicationUser?> Buscar(string userId)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<int> ContarUsuarios()
    {
        return await context.Users.CountAsync();
    }

    public async Task<List<EstadoUsuario>> ListarEstados()
    {
        return await context.EstadosUsuarios
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<IdentityRole>> ListarRoles()
    {
        return await context.Roles
            .AsNoTracking()
            .ToListAsync();
    }
}