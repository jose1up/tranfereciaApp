using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestiónDeImagenIA_Back.Core.Entities
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_cliente { get; set; }
        public string Nombre_cliente { get; set; }
        [ForeignKey(nameof(comprobante))]
        public int? id_comprobante { get; set; }
        public Comprobante? comprobante {  get; set; }

        

        



    }
}
