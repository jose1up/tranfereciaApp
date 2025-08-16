using GestiónDeImagenIA_Back.Core.Entities;

namespace GestiónDeImagenIA_Back.Core.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> ObtenerPorNombre(string nombre);
    }
}
