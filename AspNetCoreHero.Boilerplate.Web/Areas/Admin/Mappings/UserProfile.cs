﻿using AspNetCoreHero.Boilerplate.Domain.Entities.Identity;
using AspNetCoreHero.Boilerplate.Web.Areas.Admin.Models;
using AutoMapper;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}