using System.ComponentModel.DataAnnotations;

namespace GestionClientes.Models;

/// <summary>Entidad del diagrama de clases: Cliente.</summary>
public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^[0-9]{7,10}$", ErrorMessage = "El teléfono debe tener entre 7 y 10 dígitos.")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;
}
