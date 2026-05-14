using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DiseñosFlores.Models
{
    public class Cliente : IValidatableObject
    {
        public int IdCliente { get; set; }
        public string TipoDocumento { get; set; }


        [Display(Name = "Nro documento")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? NroDocumento { get; set; }

        [Display(Name = "Razon social")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string RazonSocial { get; set; }


        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Direccion {  get; set; }
        public string? Telefono {  get; set; } 
        public int IdDepartamento { get; set; }

        [ValidateNever]
        public Departamento Departamento { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TipoDocumento == "DNI" && (NroDocumento == null || NroDocumento.Length != 8))
            {
                yield return new ValidationResult(
                    "El DNI debe tener 8 dígitos",
                    new[] { nameof(NroDocumento) });
            }

            if (TipoDocumento == "RUC" && (NroDocumento == null || NroDocumento.Length != 11))
            {
                yield return new ValidationResult(
                    "El RUC debe tener 11 dígitos",
                    new[] { nameof(NroDocumento) });
            }
        }

    }
}
