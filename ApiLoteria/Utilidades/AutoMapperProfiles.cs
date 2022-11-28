using AutoMapper;
using ApiLoteria.Entidades;
using ApiLoteria.DTOs;


namespace ApiLoteria.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CartaCreacionDTO, Cartas>();
            CreateMap<Cartas, CartaDTO>();
            CreateMap<RifaCreacionDTO, Rifa>();
            CreateMap<Rifa, GetRifaDTO>();
            CreateMap<Rifa, RifaDTOConCartas>()
                .ForMember(rifaDTO => rifaDTO.Cartas, opciones => opciones.MapFrom(MapRifaDTOConCartas));

            CreateMap<Rifa, RifaDTOConParticipantes>()
                .ForMember(rifaDTO => rifaDTO.Participantes, opciones => opciones.MapFrom(MapRifaDTOParticipantes));

            CreateMap<Participante, ParticipanteDTO>();
            CreateMap<Participante, ParticipanteDTOConRifas>()
            .ForMember(participanteDTO => participanteDTO.Rifas, opciones => opciones.MapFrom(MapParticipanteDTORifas));

            //aqui me falta comprobar participante dro que se encuentre rifas


            CreateMap<ParticipanteCreacionDTO, Participante>();
            //.ForMember(participante => participante.RPCP, opciones => opciones.MapFrom(MapRPC));
           // CreateMap<Participante, ParticipanteDTO>();
                //.ForMember(participanteDTO => participanteDTO.Cartas, opciones => opciones.MapFrom(MapParticipanteDTO));
          //  CreateMap<Participante, ParticipanteDTOConRifas>();
            //CreateMap<Participante, ParticipanteDTOConRifa>()
                //.ForMember(participanteDTOR => participanteDTOR.Rifas, opciones => opciones.MapFrom(MapParticipanteDTOConRifa));
            //CreateMap<Participante, ParticipanteDTOConCarta>();
            //CreateMap<Participante, ParticipanteDTOConCarta>()
                //.ForMember(participanteDTOC => participanteDTOC.Cartas, opciones => opciones.MapFrom(MapParticipanteDTOConCarta));
        }

        private List<ParticipanteDTO> MapRifaDTOParticipantes(Rifa rifa, GetRifaDTO getRifaDTO) // lo que escribi
        {
            var resultado = new List<ParticipanteDTO>();

            if (rifa.RPCP == null) { return resultado; }

            foreach (var rPCP in rifa.RPCP)
            {
                resultado.Add(new ParticipanteDTO()
                {
                    Id = rPCP.ParticipanteId,
                    Nombre = rPCP.Participante.Nombre,
                    Direccion = rPCP.Participante.Direccion
                });
            }

            return resultado;
        }

        private List<GetRifaDTO> MapParticipanteDTORifas(Participante participante, ParticipanteDTO participanteDTO)
        {
            var resultado = new List<GetRifaDTO>();

            if (participante.RPCP == null)
            {
                return resultado;
            }

            foreach (var rPCP in participante.RPCP)
            {
                resultado.Add(new GetRifaDTO()
                {

                    Id = rPCP.RifaId,
                    Nombre = rPCP.Rifa.Nombre,
                    NumPrem = rPCP.Rifa.NumPrem
                });
            }

            return resultado;
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

        private List<RPCP> MapRPC(ParticipanteCreacionDTO participanteCreacionDTO, Participante participante)
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

            foreach (var cartasId in participanteCreacionDTO.CartasIds)
            {
                resultado.Add(new RPCP()
                {
                    CartasId = cartasId
                });
            }

            return resultado;
        
        }
       
         /*private List<CartaDTO> MapParticipanteDTO(Participante participante, ParticipanteDTO participanteDTO)
         {
             var resultado = new List<CartaDTO>();

             if(participante.RPCP == null) { return resultado; }

             foreach(var rPCP in participanteDTO.Cartas)
             {
                 resultado.Add(new CartaDTO()
                 {
                     Id = rPCP.Id,
                     Nombre = rPCP.Nombre
                 });
             }

             return resultado;
         }*/


        private List<CartaDTO> MapParticipanteDTOConCarta(Cartas cartas, ParticipanteDTO participanteDTO)
        {
            var resultado = new List<CartaDTO>();

            if (cartas.RPCP == null) { return resultado; }

            foreach (var rPCP in cartas.RPCP)
            {
                resultado.Add(new CartaDTO()
                {
                    Id = rPCP.CartasId,
                    Nombre = rPCP.Cartas.Nombre
                });
            }

            return resultado;
        }
        

    }
}
