using System.ComponentModel.DataAnnotations;

namespace Lab4_OsmarAnccoE.Dtos.Ordene;

public class OrdeneUpdateDto
{
    [Required]
    public int ClienteID { get; set; }
}