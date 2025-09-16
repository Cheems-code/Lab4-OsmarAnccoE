using System.ComponentModel.DataAnnotations;

namespace Lab4_OsmarAnccoE.Dtos.Pago;

public class PagoCreateDto
{
    [Required]
    public int OrdenID { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto del pago debe ser positivo.")]
    public decimal Monto { get; set; }

    [StringLength(50)]
    public string MetodoPago { get; set; }
}