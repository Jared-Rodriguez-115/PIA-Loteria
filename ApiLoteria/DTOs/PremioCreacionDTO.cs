using ApiLoteria.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiLoteria.DTOs
{
    public class PremioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Contenido { get; set; }

        public List<int> RifasId { get; set; }
    }
}
