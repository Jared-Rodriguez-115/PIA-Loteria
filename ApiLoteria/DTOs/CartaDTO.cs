using ApiLoteria.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiLoteria.DTOs
{
    public class CartaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

    }
}
