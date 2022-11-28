using ApiLoteria.Validaciones;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApiLoteria.Entidades
{
    public class Cartas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public int ParticipanteId { get; set; }

        public List<RPCP> RPCP { get; set; }

        //public Participante Participante { get; set; } 

        //public string UsuarioId { get; set; }

        //public IdentityUser Usuario { get; set; }
    }
}
