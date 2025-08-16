using GestiónDeImagenIA_Back.Core.Entities;

namespace GestiónDeImagenIA_Back.Core.Repositories
{
    public interface IPersonaRepository: IRepository<Persona>
    {

        Task<Persona> BuscarPersonaPorCuitDniAsync(string cuitDni);
       
    }
}
