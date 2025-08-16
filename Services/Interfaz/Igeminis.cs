using GestiónDeImagenIA_Back.Services.DTO;
using Refit;

namespace GestiónDeImagenIA_Back.Services.Interfaz
{
    public interface Igeminis
    {
        Task<ApiResponse<GeminiResponseDTO>> ConsultarGeminiConImagen(string promptTexto, string imagenBase64, string mimeTypeImagen);
    }
}
