using System.ComponentModel.DataAnnotations;

namespace Lab4_OsmarAnccoE.Dtos.Cliente;

public class ClienteCreateDto
{
    [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    [StringLength(100)]
    public string Correo { get; set; }
}
