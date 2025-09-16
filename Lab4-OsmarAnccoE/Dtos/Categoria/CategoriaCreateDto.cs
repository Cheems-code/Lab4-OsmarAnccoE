using System.ComponentModel.DataAnnotations;

namespace Lab4_OsmarAnccoE.Dtos.Categoria;

public class CategoriaCreateDto
{
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; set; }
}