using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Request;
using ApiTest.SQL.DBModels;
using AutoMapper;

namespace ApiTest.Abstraction.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProcessPaymentDTO, CardDetails>();
            CreateMap<CardDetails, Payment>();
        }
    }
}
