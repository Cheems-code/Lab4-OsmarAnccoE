using System.ComponentModel.DataAnnotations;

namespace Lab4_OsmarAnccoE.Dtos.DetallesOrden;

public class DetalleOrdenCreateDto
{
    [Required]
    public int ProductoID { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
    public int Cantidad { get; set; }
}