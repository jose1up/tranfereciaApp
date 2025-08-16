using GestiónDeImagenIA_Back.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestiónDeImagenIA_Back.Core.Repositories
{
    public class PersonaRepository: Repository<Persona> , IPersonaRepository
    {
        public PersonaRepository(AppDbContext context) : base(context) { }

        public async Task<Persona> BuscarPersonaPorCuitDniAsync(string cuitDni)
        {
            string NormalString = cuitDni.Replace("-", "");
            return await _context.Personas.Where(x=> x.CuitDni.Replace("-","")== NormalString).FirstOrDefaultAsync();

        }
    }
}
