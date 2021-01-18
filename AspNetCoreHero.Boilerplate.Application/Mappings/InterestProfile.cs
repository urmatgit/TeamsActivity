using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetById;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AutoMapper;

namespace AspNetCoreHero.Boilerplate.Application.Mappings
{
    internal class InterestProfile : Profile
    {
        public InterestProfile()
        {
            CreateMap<CreateInterestCommand, Interest>().ReverseMap();
            CreateMap<GetInterestByIdResponse, Interest>().ReverseMap();
            CreateMap<GetAllInterestsCachedResponse, Interest>().ReverseMap();
            CreateMap<GetAllInterestsResponse, Interest>().ReverseMap();
        }
    }
}