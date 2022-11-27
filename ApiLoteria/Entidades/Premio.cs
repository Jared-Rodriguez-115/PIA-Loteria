using ApiLoteria.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiLoteria.Entidades
{
    public class Premio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Contenido { get; set; }

        public int RifaId { get; set; }

        public List<RPCP> RPCP { get; set; }
    }
}
