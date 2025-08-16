namespace GestiónDeImagenIA_Back.Controllers.DTO
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization; // Para System.Text.Json

    namespace TuProyecto.DTOs.Gemini
    {
        public class GeminiRequestDTO
        {
            [JsonPropertyName("contents")]
            public List<Content> Contents { get; set; } = new List<Content>();
        }

        public class Content
        {
            [JsonPropertyName("parts")]
            public List<Part> Parts { get; set; } = new List<Part>();
        }

        public class Part
        {
            [JsonPropertyName("text")]
            public string? Text { get; set; } 

            [JsonPropertyName("inlineData")]
            public InlineData? InlineData { get; set; } 
        }

        public class InlineData
        {
            [JsonPropertyName("mime_type")]
            public string MimeType { get; set; } = string.Empty; // Ej. "image/jpeg", "image/png"

            [JsonPropertyName("data")]
            public string Data { get; set; } = string.Empty; // La imagen codificada en Base64
        }

    }
}
