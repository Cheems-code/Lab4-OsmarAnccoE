using System.ComponentModel.DataAnnotations;

namespace Lab4_OsmarAnccoE.Dtos.Pago;

public class PagoUpdateDto
{
    [StringLength(50)]
    public string MetodoPago { get; set; }
}