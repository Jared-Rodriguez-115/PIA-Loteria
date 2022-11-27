﻿using System.ComponentModel.DataAnnotations;
using ApiLoteria.Validaciones;

namespace ApiLoteria.DTOs
{
    public class RifaDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]

        public string Nombre { get; set; }


    }
}
