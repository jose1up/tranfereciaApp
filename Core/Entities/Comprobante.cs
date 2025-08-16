namespace GestiónDeImagenIA_Back.Core.Entities
{
    public class Comprobante
    {
        public int Id { get; set; }

        public string TipoComprobante { get; set; } = null!;

        public DateTime FechaTransferencia { get; set; }

        public TimeSpan HoraTransferencia { get; set; }

        public decimal Monto { get; set; }

        public string? Moneda { get; set; }

        public int RemitenteId { get; set; }
        public Persona? Remitente { get; set; } = null!;

        public int BeneficiarioId { get; set; }
        public Persona? Beneficiario { get; set; } = null!;

        public string NumeroOperacion { get; set; } = null!;

        public string? Referencia { get; set; }



    }
    public class Persona
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string CuitDni { get; set; } = null!;
        public string Banco { get; set; } = null!;
        public string CbuAlias { get; set; } = null!;
    }
}
