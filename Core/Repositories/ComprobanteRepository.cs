using GestiónDeImagenIA_Back.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestiónDeImagenIA_Back.Core.Repositories
{
    public class ComprobanteRepository : Repository<Comprobante>, IComprobanteRepository
    {
        readonly IPersonaRepository _personaRepository;
        public ComprobanteRepository(AppDbContext context, IPersonaRepository personaRepository) : base(context)
        {
            _personaRepository = personaRepository;
        }

        public async Task<List<Comprobante>> GetAllbyRemitenteandBeneficiario()
        {
            var result = await _context.Comprobantes.Include(r => r.Remitente).Include(b => b.Beneficiario).ToListAsync();
            return result;
        }

        public async Task<bool> TransantionCrearComprobante(Comprobante comprobante)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (comprobante.Remitente != null)
                {
                    var remitente = await _personaRepository.BuscarPersonaPorCuitDniAsync(comprobante.Remitente.CuitDni);
                    if (remitente != null)
                    {
                        comprobante.Remitente = remitente;

                    }
                }

                if (comprobante.Beneficiario != null)
                {
                    var beneficiario = await _personaRepository.BuscarPersonaPorCuitDniAsync(comprobante.Beneficiario.CuitDni);
                    if (beneficiario != null)
                    {
                        comprobante.Beneficiario = beneficiario;

                    }
                }


                await _context.Comprobantes.AddAsync(comprobante);

                var result = await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return result == 0 ? true : false;
               
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;

            }
        }
    }
}
