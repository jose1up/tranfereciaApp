using GestiónDeImagenIA_Back.Controllers.DTO.TuProyecto.DTOs.Gemini;
using System.Text.Json.Serialization;

namespace GestiónDeImagenIA_Back.Services.DTO
{
    public class GeminiResponseDTO
    {
        [JsonPropertyName("candidates")]
        public List<Candidate>? Candidates { get; set; }
        public class Candidate
        {
            [JsonPropertyName("content")]
            public Content? Content { get; set; }
        }
    }
}
