using System.ComponentModel.DataAnnotations;

namespace Lab4_OsmarAnccoE.Dtos.Producto;

public class ProductoUpdateDto
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [StringLength(255)]
    public string Descripcion { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    [Required]
    public int CategoriaID { get; set; }
}