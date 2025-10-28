using AutoMapper;
using BoletosApi.Models;
using BoletosApi.Dtos;

namespace BoletosApi.Profiles
{
    public class BancoProfile : Profile
    {
        public BancoProfile()
        {
            CreateMap<BancoCreateDto, Banco>();
            CreateMap<Banco, BancoDto>();

            CreateMap<BoletoCreateDto, Models.Boleto>();
            CreateMap<Models.Boleto, Dtos.BoletoDto>();
        }
    }
}
