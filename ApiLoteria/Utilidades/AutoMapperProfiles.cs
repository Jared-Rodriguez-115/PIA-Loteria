using AutoMapper;
using ApiLoteria.Entidades;
using ApiLoteria.DTOs;
using ApiLoteria.Migrations;

namespace ApiLoteria.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<RifaCreacionDTO, Rifa>();
            CreateMap<Rifa, GetRifaDTO>();
            CreateMap<Rifa, RifaDTOConCartas>()
                .ForMember(rifaDTO => rifaDTO.Cartas, opciones => opciones.MapFrom(MapRifaDTOConCartas));
            CreateMap<ParticipanteCreacionDTO, Participante>()
                .ForMember(participante => participante.RPCP, opciones => opciones.MapFrom(MapRPC));
            CreateMap<Participante, ParticipanteDTO>()
                .ForMember(participanteDTO => participanteDTO, opciones => opciones.MapFrom(MapParticipanteDTO));
            CreateMap<Participante, ParticipanteDTOConRifa>();
            //CreateMap<Participante, ParticipanteDTOConRifa>()
            //.ForMember(participanteDTO => participanteDTO.Rifas, opciones => opciones.MapFrom(MapParticipanteDTOConRifa));
            CreateMap<Participante, ParticipanteDTOConCarta>();
            //CreateMap<Participante, ParticipanteDTOConCarta>()
                //.ForMember(participanteDTOC => participanteDTOC.Cartas, opciones => opciones.MapFrom(MapParticipanteDTOConCarta));
        }

        

        private List<CartaDTO> MapRifaDTOConCartas(Rifa rifa, GetRifaDTO getRifaDTO)
        {
            var resultado = new List<CartaDTO>();

            if (rifa.RPCP == null) { return resultado; }

            foreach (var rPCP in rifa.RPCP)
            {
                resultado.Add(new CartaDTO()
                {
                    Id = rPCP.CartasId,
                    Nombre = rPCP.Cartas.Nombre   
                });
            }

            return resultado;
        }

        private List<RPCP> MapRPC (ParticipanteCreacionDTO participanteCreacionDTO, Participante participante)
        {
            var resultado = new List<RPCP>();

            if(participanteCreacionDTO.RifasIds == null) { return resultado; }
            if(participanteCreacionDTO.CartasIds == null) { return resultado; }

            foreach (var rifasId in participanteCreacionDTO.RifasIds)
            {
                resultado.Add(new RPCP()
                {
                    RifaId = rifasId

                });
            }

            foreach (var cartaId in participanteCreacionDTO.CartasIds)
            {
                resultado.Add(new RPCP()
                {
                    CartasId = cartaId

                });
            }

            return resultado;
        }


        private List<CartaDTO> MapParticipanteDTO(Participante participante, ParticipanteDTO participanteDTO)
        {
            var resultado = new List<CartaDTO>();

            if(participante.RPCP == null) { return resultado; }

            foreach(var rPCP in participanteDTO.Carta)
            {
                resultado.Add(new CartaDTO()
                {
                    Id = rPCP.Id,
                    Nombre = rPCP.Nombre
                });
            }

            return resultado;
        }

        private List<GetRifaDTO> MapParticipanteDTOConRifa(Rifa rifas, ParticipanteDTO participanteDTO)
        {
            var resultado = new List<GetRifaDTO>();

            if (rifas.RPCP == null) { return resultado; }

            foreach (var rPCP in rifas.RPCP)
            {
                resultado.Add(new GetRifaDTO()
                {
                    Id = rPCP.RifaId
                    //Nombre = rPCP.Rifa.Nombre
                });
            }

            return resultado;
        }

        private List<CartaDTO> MapParticipanteDTOConCarta(Cartas cartas, ParticipanteDTO participanteDTO)
        {
            var resultado = new List<CartaDTO>();

            if (cartas.RPCP == null) { return resultado; }

            foreach (var rPCP in cartas.RPCP)
            {
                resultado.Add(new CartaDTO()
                {
                    Id = rPCP.CartasId
                    //Nombre = rPCP.Cartas.Nombre
                });
            }

            return resultado;
        }
        

    }
}
