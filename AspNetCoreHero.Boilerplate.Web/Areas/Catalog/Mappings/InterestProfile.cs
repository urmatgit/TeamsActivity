using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetById;
using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using AutoMapper;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Mappings
{
    internal class InterestProfile : Profile
    {
        public InterestProfile()
        {
            CreateMap<GetAllInterestsCachedResponse, InterestViewModel>().ReverseMap();
            CreateMap<GetInterestByIdResponse, InterestViewModel>().ReverseMap();
            CreateMap<CreateInterestCommand, InterestViewModel>().ReverseMap();
            CreateMap<UpdateInterestCommand, InterestViewModel>().ReverseMap();
        }
    }
}