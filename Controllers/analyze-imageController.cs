using GestiónDeImagenIA_Back.BusinessLogic.Interfaz;
using GestiónDeImagenIA_Back.BusinessLogic.Logic;
using GestiónDeImagenIA_Back.Core.Entities;
using GestiónDeImagenIA_Back.Core.Repositories;
using GestiónDeImagenIA_Back.Services.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GestiónDeImagenIA_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class analyze_imageController : ControllerBase
    {
        private readonly Igeminis _Genimis;
        private readonly IComprobanteLogic _ComporbanteLogic;
        private readonly IComprobanteRepository _comprobanteRepository;
        public analyze_imageController(Igeminis geminics, IComprobanteLogic comprobanteLogic , IComprobanteRepository comprobanteRepository)
        {
            _Genimis = geminics;
            _ComporbanteLogic = comprobanteLogic;
            _comprobanteRepository = comprobanteRepository;
            
        }

        [HttpPost("Subir")]
        public async Task<ActionResult<string>> uploadImage(IFormFile file)
        {
            var imagenBase64 = await SubirFoto(file);
            var proms = "Extrae la siguiente información de este comprobante de transferencia y devuélvela estrictamente en formato JSON." +
                " Si algún campo no se encuentra, omítelo o déjalo como null según corresponda," +
                " pero no generes campos adicionales.\\n\\nEl objeto JSON debe contener las siguientes claves:\\n- 'tipo_comprobante': Siempre 'TRANSFERENCIA'.\\n- 'fecha_transferencia': La fecha de la operación en formato DD/MM/AAAA.\\n- 'hora_transferencia': La hora de la operación en formato HH:MM.\\n- 'monto': El valor numérico de la transferencia. Debe ser un número con decimales si aplica.\\n- 'moneda': La moneda de la transferencia (ej. ARS, USD).\\n- 'remitente': Un objeto con 'nombre_completo', 'cuit_dni', 'banco' y 'cbu_alias' del origen de la transferencia.\\n- 'beneficiario': Un objeto con 'nombre_completo', 'cuit_dni', 'banco' y 'cbu_alias' del destino de la transferencia.\\n- 'numero_operacion': El número único de operación o transacción.\\n- 'referencia': Cualquier referencia o descripción adicional de la transferencia.\\n\\nEjemplo de formato JSON esperado:\\n```json\\n{\\n  \\\"tipo_comprobante\\\": \\\"TRANSFERENCIA\\\",\\n  \\\"fecha_transferencia\\\": \\\"25/07/2025\\\",\\n  \\\"hora_transferencia\\\": \\\"14:35\\\",\\n  \\\"monto\\\": 15000.75,\\n  \\\"moneda\\\": \\\"ARS\\\",\\n  \\\"remitente\\\": {\\n    \\\"nombre_completo\\\": \\\"Juan Perez\\\",\\n    \\\"cuit_dni\\\": \\\"20-12345678-9\\\",\\n    \\\"banco\\\": \\\"Banco Nación\\\",\\n    \\\"cbu_alias\\\": \\\"JUAN.PEREZ.ALIAS\\\"\\n  },\\n  \\\"beneficiario\\\": {\\n    \\\"nombre_completo\\\": \\\"Maria Rodriguez\\\",\\n    \\\"cuit_dni\\\": \\\"27-98765432-1\\\",\\n    \\\"banco\\\": \\\"Banco Provincia\\\",\\n    \\\"cbu_alias\\\": \\\"MARIA.RODRIGUEZ.CBU\\\"\\n  },\\n  \\\"numero_operacion\\\": \\\"TRX789ABC\\\",\\n  \\\"referencia\\\": \\\"Pago Alquiler Julio\\\"\\n}\\n```\\n\"";

            var respuesta = await _Genimis.ConsultarGeminiConImagen(proms, imagenBase64, file.ContentType.ToString());
            var carga = await _ComporbanteLogic.ComprobanteCarga(respuesta);

            return Ok();

        }

        [HttpGet("allComprobante")]
        public async Task<List<Comprobante>> allComprobante()
        {
            var result = await _comprobanteRepository.GetAllbyRemitenteandBeneficiario();
            return result;
        }

       


        private async Task<string> SubirFoto(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                byte[] bytes = ms.ToArray();
                return Convert.ToBase64String(bytes);
            }

        }
    }
}
