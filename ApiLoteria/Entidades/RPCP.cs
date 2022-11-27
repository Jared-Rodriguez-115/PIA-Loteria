namespace ApiLoteria.Entidades
{
    public class RPCP
    {
        public int RifaId { get; set; }

        public int ParticipanteId { get; set; }

        public int CartasId { get; set; }

        public int PremioId { get; set; }

        public int Orden { get; set; }

        public Rifa Rifa { get; set; }

        public Participante Participante { get; set; }

        public Cartas Cartas { get; set; }  

        public Premio Premio { get; set; }
    }
}
