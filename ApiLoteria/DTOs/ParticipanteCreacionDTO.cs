using System.ComponentModel.DataAnnotations;
using ApiLoteria.Validaciones;

namespace ApiLoteria.DTOs
{
    public class ParticipanteCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Direccion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<int> RifasIds { get; set; }

        public List<int> CartasIds { get; set; }
    }
}
