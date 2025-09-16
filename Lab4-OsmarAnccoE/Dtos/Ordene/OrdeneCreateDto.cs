using System.ComponentModel.DataAnnotations;
using Lab4_OsmarAnccoE.Dtos.DetallesOrden;

namespace Lab4_OsmarAnccoE.Dtos.Ordene;

public class OrdeneCreateDto
{
    [Required]
    public int ClienteID { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "La orden debe tener al menos un producto.")]
    public List<DetalleOrdenCreateDto> Detalles { get; set; }
}