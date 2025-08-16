using GestiónDeImagenIA_Back.Core.Entities;

namespace GestiónDeImagenIA_Back.Core.Repositories
{
    public interface IComprobanteRepository : IRepository<Comprobante>
    {
        Task<List<Comprobante>> GetAllbyRemitenteandBeneficiario();
        Task<bool> TransantionCrearComprobante(Comprobante comprobante);
    }
}
