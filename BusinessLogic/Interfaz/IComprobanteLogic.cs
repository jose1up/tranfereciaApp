using GestiónDeImagenIA_Back.Services.DTO;
using Refit;

namespace GestiónDeImagenIA_Back.BusinessLogic.Interfaz
{
    public interface IComprobanteLogic
    {
        Task<bool> ComprobanteCarga(ApiResponse<GeminiResponseDTO> RespuestaGeminis);
    }
}
