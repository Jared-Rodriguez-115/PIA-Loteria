namespace ApiLoteria.DTOs
{
    public class ParticipanteDTOConCarta: ParticipanteDTO
    {
        public List<CartaDTO> Cartas { get; set; }
    }
}

