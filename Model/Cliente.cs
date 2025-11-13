using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Clientes")]
    public class Cliente : EntidadBase, IValidatableObject
    {
        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Debe ingresar una fecha válida.")]
        public DateTime Fecha_Nacimiento { get; set; }

        [Required]
        [RegularExpression(@"^(20|23|24|25|26|27|30|33|34)-?\d{8}-?\d$", ErrorMessage = "El CUIT no tiene un formato válido.")]
        public string Cuit { get; set; }

        public string Domicilio { get; set; }

        [Phone]
        [Required]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Fecha_Nacimiento > DateTime.Today)
                yield return new ValidationResult("La fecha de nacimiento no puede ser futura.", new[] { nameof(Fecha_Nacimiento) });
        }
    }
}
