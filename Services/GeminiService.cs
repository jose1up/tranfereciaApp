using GestiónDeImagenIA_Back.Controllers.DTO.TuProyecto.DTOs.Gemini;
using GestiónDeImagenIA_Back.Services.DTO;
using GestiónDeImagenIA_Back.Services.Interfaz;
using Refit;
namespace GestiónDeImagenIA_Back.Services
{
    public class GeminiService : Igeminis
    {

        private readonly IGeminics _geminics;
        private readonly string _geminiApiKey;
        public GeminiService(IGeminics geminics)
        {
            _geminics = geminics;

            _geminiApiKey = Environment.GetEnvironmentVariable("geminiApikey");

            if (string.IsNullOrEmpty(_geminiApiKey))
            {
                throw new ArgumentNullException(nameof(_geminiApiKey), "No se encontró la API Key en el entorno.");
            }
        }
        public async Task<ApiResponse<GeminiResponseDTO>> ConsultarGeminiConImagen(string promptTexto, string imagenBase64, string mimeTypeImagen)
        {
            try
            {

                var request = new GeminiRequestDTO
                {
                    Contents = new List<Content>{
                    new Content{
                    Parts = new List<Part>
                {
                new Part
                {
                    Text = promptTexto
                },
                new Part
                {
                    InlineData = new InlineData
                    {
                        MimeType = mimeTypeImagen,
                        Data= imagenBase64

                    } }

                     }
                        }
                     }
                };

                var result = await _geminics.GenerateContent("gemini-2.0-flash", request, _geminiApiKey);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar Gemini con imagen", ex);
            }
        }




    }
}

