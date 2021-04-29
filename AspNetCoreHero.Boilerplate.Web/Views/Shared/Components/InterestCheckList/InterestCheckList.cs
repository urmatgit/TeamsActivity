using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.InterestCheckList
{
    public class InterestCheckList: ViewComponent
    {
       
        
        public async Task<IViewComponentResult> InvokeAsync(UserInterestsCheckabelViewModel model )
        {
            
            return  await  Task.FromResult<IViewComponentResult>( View (model)) ;
        }
        
    }
}
