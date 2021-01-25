using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries;

using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Mappings
{
    
    internal class UserInterestProfile : Profile
    {
        public UserInterestProfile()
        {
            CreateMap<CreateUserInterestCommand, UserInterest>().ReverseMap();
            CreateMap<GetUserInterestResponse, UserInterest>().ReverseMap();
         //   CreateMap<GetAllUserInterestsResponse, UserInterest>().ReverseMap();
            
        }
    }
}
