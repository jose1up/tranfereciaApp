using GestiónDeImagenIA_Back.Controllers.DTO.TuProyecto.DTOs.Gemini;
using GestiónDeImagenIA_Back.Services.DTO;
using Refit;
using DotNetEnv;

namespace GestiónDeImagenIA_Back.Services.Interfaz
{
    public interface IGeminics
    {
        [Post("/v1beta/models/{modelId}:generateContent")]
        [Headers("Content-Type: application/json")]
        Task<ApiResponse<GeminiResponseDTO>> GenerateContent(
            [AliasAs("modelId")] string modelId,
            [Body] GeminiRequestDTO request,
            [Header("x-goog-api-key")] string ApiKey);
    }

}
