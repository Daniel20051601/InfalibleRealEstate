using InfalibleRealEstate.Data;
using InfalibleRealEstate.Models;
using Microsoft.EntityFrameworkCore;

namespace InfalibleRealEstate.Services
{
    public class CarritoService(IDbContextFactory<ApplicationDbContext> DbContext)
    {
        public async Task<List<CarritoItem>> ObtenerItemsCarrito(string usuarioId)
        {
            using var contexto = DbContext.CreateDbContext();
            var carrito = await contexto.Carritos
                .Include(c => c.CarritoItems)
                    .ThenInclude(ci => ci.Propiedad)
                        .ThenInclude(p => p.Detalle)
                .Include(c => c.CarritoItems)
                    .ThenInclude(ci => ci.Propiedad)
                        .ThenInclude(p => p.Imagenes)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            return carrito?.CarritoItems.ToList() ?? new List<CarritoItem>();
        }

        public async Task<(List<CarritoItem> Items, int TotalCount)> ObtenerItemsCarritoPaginadoAsync(string usuarioId, int pagina, int tamanoPagina)
        {
            using var contexto = DbContext.CreateDbContext();
            var carrito = await contexto.Carritos
                .Include(c => c.CarritoItems)
                    .ThenInclude(ci => ci.Propiedad)
                        .ThenInclude(p => p.Detalle)
                .Include(c => c.CarritoItems)
                    .ThenInclude(ci => ci.Propiedad)
                        .ThenInclude(p => p.Imagenes)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrito == null || !carrito.CarritoItems.Any())
            {
                return (new List<CarritoItem>(), 0);
            }

            var totalItems = carrito.CarritoItems.Count;

            var itemsPaginados = carrito.CarritoItems
                .OrderBy(ci => ci.CarritoItemId)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            return (itemsPaginados, totalItems);
        }

        public async Task<bool> Agregar(string usuarioId, int propiedadId)
        {
            using var contexto = DbContext.CreateDbContext();
            var carrito = await contexto.Carritos
                .Include(c => c.CarritoItems)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrito == null)
            {
                carrito = new Carrito { UsuarioId = usuarioId, CarritoItems = new List<CarritoItem>() };
                contexto.Carritos.Add(carrito);
            }

            if (carrito.CarritoItems.Any(ci => ci.PropiedadId == propiedadId))
            {
                return false;
            }

            carrito.CarritoItems.Add(new CarritoItem { PropiedadId = propiedadId });
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(string usuarioId, int propiedadId)
        {
            using var contexto = DbContext.CreateDbContext();
            var carrito = await contexto.Carritos
                .Include(c => c.CarritoItems)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrito != null)
            {
                var item = carrito.CarritoItems.FirstOrDefault(ci => ci.PropiedadId == propiedadId);
                if (item != null)
                {
                    carrito.CarritoItems.Remove(item);
                    contexto.CarritoItems.Remove(item);
                    return await contexto.SaveChangesAsync() > 0;
                }
            }
            return false;
        }

        public async Task Vaciar(string usuarioId)
        {
            using var contexto = DbContext.CreateDbContext();
            var carrito = await contexto.Carritos
                .Include(c => c.CarritoItems)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrito != null)
            {
                contexto.CarritoItems.RemoveRange(carrito.CarritoItems);
                carrito.CarritoItems.Clear();
                await contexto.SaveChangesAsync();
            }
        }
    }
}