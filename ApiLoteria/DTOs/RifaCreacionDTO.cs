using System.ComponentModel.DataAnnotations;
using ApiLoteria.Validaciones;

namespace ApiLoteria.DTOs
{
    public class RifaCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]

        public string Nombre { get; set; }

        [Required]
        public int NumPrem { get; set; }
    }
}
