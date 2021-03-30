using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries;
using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using AutoMapper;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Mappings
{
    internal class UserInterestProfile : Profile
    {
        public UserInterestProfile()
        {
            CreateMap<GetUserInterestResponse, UserInterestViewModel>().ReverseMap();
            CreateMap<GetUserInterestResponse, UserInterestViewModel>().ReverseMap();
            CreateMap<CreateUserInterestCommand, UserInterestViewModel>().ReverseMap();
            CreateMap<UpdateUserInterestCommand, UserInterestViewModel>().ReverseMap();
        }
    }
}