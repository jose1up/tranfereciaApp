using GestiónDeImagenIA_Back.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestiónDeImagenIA_Back.Core.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Cliente>> ObtenerPorNombre(string nombre)
        {
            var result = await _context.clientes
                .AsNoTracking()
                .Where(c => c.Nombre_cliente.Contains(nombre)).ToListAsync();
            return result;
        }
    }
}
