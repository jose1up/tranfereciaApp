using GestiónDeImagenIA_Back.BusinessLogic.Interfaz;
using GestiónDeImagenIA_Back.Core.Entities;
using GestiónDeImagenIA_Back.Core.Repositories;
using GestiónDeImagenIA_Back.Services.DTO;
using Refit;
using Sprache;
using System.Text.Json;
using System.Globalization;
using static GestiónDeImagenIA_Back.BusinessLogic.Logic.ComprobanteLogic;

namespace GestiónDeImagenIA_Back.BusinessLogic.Logic
{
    public class ComprobanteLogic : IComprobanteLogic
    {
        private readonly ILogger<ComprobanteLogic> _logger;
        private readonly IComprobanteRepository _comprobanteRepository;

        public ComprobanteLogic(ILogger<ComprobanteLogic> logger, IComprobanteRepository comprobanteRepository)
        {
            _logger = logger;
            _comprobanteRepository= comprobanteRepository;
        }
        public async Task<bool> ComprobanteCarga(ApiResponse<GeminiResponseDTO> respuesta)
        {
            _logger.LogInformation("strart ---> comprobanteLogig");
            if (respuesta.Error != null)
            {
                _logger.LogError("No se resivio respuesta de geminis");
                return false;
                
                
            }

             


            var respuestaJson=  respuesta.Content.Candidates.FirstOrDefault().Content.Parts.FirstOrDefault().Text.Replace("```" , "").Replace("json","");


            var comprobanteDto = JsonSerializer.Deserialize<ComprobanteDTO>(respuestaJson);



            var comprobante = ComprobanteVsDtoMap(comprobanteDto);

            await _comprobanteRepository.TransantionCrearComprobante(comprobante);

            






            Console.WriteLine(respuestaJson);

            return true;


        }
        public class ComprobanteDTO
        {
            public string tipo_comprobante { get; set; }
            public string fecha_transferencia { get; set; }
            public string hora_transferencia { get; set; }
            public decimal monto { get; set; }
            public string moneda { get; set; }
            public PersonaDTO remitente { get; set; }
            public PersonaDTO beneficiario { get; set; }
            public string numero_operacion { get; set; }
            public string referencia { get; set; }
        }
        public class PersonaDTO
        {
            public string nombre_completo { get; set; }
            public string cuit_dni { get; set; }
            public string banco { get; set; }
            public string? cbu_alias { get; set; }
        }

        private Comprobante ComprobanteVsDtoMap(ComprobanteDTO comprobanteDto)
        {
            var comprobante = new Comprobante
            {
                TipoComprobante = comprobanteDto.tipo_comprobante,
                FechaTransferencia = DateTime.ParseExact(comprobanteDto.fecha_transferencia, "dd/MM/yyyy", null),
                HoraTransferencia = TimeSpan.Parse(comprobanteDto.hora_transferencia),
                Monto = comprobanteDto.monto,
                Moneda = comprobanteDto.moneda,
                NumeroOperacion = comprobanteDto.numero_operacion,
                Referencia = comprobanteDto.referencia,

                Remitente = new Persona
                {
                    NombreCompleto = comprobanteDto.remitente.nombre_completo,
                    CuitDni = comprobanteDto.remitente.cuit_dni,
                    Banco = comprobanteDto.remitente.banco,
                    CbuAlias = comprobanteDto.remitente.cbu_alias ?? ""
                },

                Beneficiario = new Persona
                {
                    NombreCompleto = comprobanteDto.beneficiario.nombre_completo,
                    CuitDni = comprobanteDto.beneficiario.cuit_dni,
                    Banco = comprobanteDto.beneficiario.banco,
                    CbuAlias = comprobanteDto.beneficiario.cbu_alias ?? ""
                }
            };

            return comprobante;
        }

    }
}
