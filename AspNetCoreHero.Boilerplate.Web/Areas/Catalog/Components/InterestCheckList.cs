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

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Component.InterestCheckList
{
    public class InterestCheckList: ViewComponent
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private IMediator _mediatorInstance;
        private IMapper _mapperInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
        public InterestCheckList(IAuthenticatedUserService authenticatedUser )
        {
            _authenticatedUser = authenticatedUser;
         
        }
        public async Task<IViewComponentResult> InvokeAsync(UserInterestsCheckabelViewModel model )
        {
            
            return  await  Task.FromResult<IViewComponentResult>( View (model)) ;
        }
        
    }
}
