using System.ComponentModel.DataAnnotations;

namespace Front.Cliente.Models
{
    public class Cliente
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Ingrese solo números, con exactamente 10 dígitos.")]
        [StringLength(10, ErrorMessage = "El máximo permitido es de 10 dígitos.")]
        public string? Cedula { get; set; }
    }
}
