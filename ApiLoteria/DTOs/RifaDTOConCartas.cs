namespace ApiLoteria.DTOs
{
    public class RifaDTOConCartas: GetRifaDTO
    {
       public List<CartaDTO> Cartas { get; set; }
    }
}
