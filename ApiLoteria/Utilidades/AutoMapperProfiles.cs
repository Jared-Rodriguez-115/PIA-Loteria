using AutoMapper;
using ApiLoteria.Entidades;
using ApiLoteria.DTOs;

namespace ApiLoteria.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<RifaDTO, Rifa>();
            CreateMap<Rifa, GetRifaDTO>();

        }

    }
}
