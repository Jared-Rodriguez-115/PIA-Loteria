namespace ApiLoteria.DTOs
{
    public class ParticipanteDTOConCarta: ParticipanteDTO
    {
        public List<CartaDTO> Carta { get; set; }
    }
}

