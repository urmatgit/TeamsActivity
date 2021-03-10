using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class UserInterestController : BaseController<UserInterestController>
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        public UserInterestController( IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }
        // GET: UserInterestController
        public IActionResult Index()
        {
            var model = new UserInterestModel();
            return View();
        }
        public async Task<JsonResult> LoadAll()
        {
            return await ReturnResult();
        }
         

        // GET: UserInterestController/Create
        public async Task<JsonResult> OnGetCreateOrEdit(string userid,int interestid)
        {
            if (string.IsNullOrEmpty(userid))
            {

                _notify.Error("Unknown user!");
                return null;
             }
            if (interestid == 0)
            {
                var userViewModel = new UserInterestModel() { UserId= _authenticatedUser.UserId};
                return new JsonResult(new {isValid=true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", userViewModel) });
        }
            else
            {

                var response = await _mediator.Send(new GetUserInterestByQuery() { 
                    UserId=userid,
                    InterestId=interestid
                });
                if (response.Succeeded)
                {
                    var userinterestViewModel = _mapper.Map<UserInterestModel>(response.Data);
                    return new JsonResult(new { isValid=true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", userinterestViewModel) });
                }
                return null;

            }
        }
        private async Task<JsonResult> ReturnResult( )
        {
            var response = await _mediator.Send(new GetUnterestsByUserIdQuery(_authenticatedUser.UserId));
            if (response.Succeeded)
            {

                var viewUserInterestModels = _mapper.Map<List<UserInterestModel>>(response.Data);
                var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewUserInterestModels);
                
                return new JsonResult(new { isValid = true, html = html });
            } else
            {
                _notify.Error(response.Message);
                return null;
            }

        }
        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, UserInterestModel userInterestModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createUserInterestCommand = _mapper.Map<CreateUserInterestCommand>(userInterestModel);
                    var result = await _mediator.Send(createUserInterestCommand);
                    if (result.Succeeded)
                    {
                        var viewModel = _mapper.Map<UserInterestModel>(result.Data);
                        _notify.Success($"Interest {viewModel.Interest?.Name} added to user");
                    }
                    else
                        _notify.Error(result.Message);
                    return await ReturnResult();
                }
                else
                {
                    var updateUserInterestCommand = _mapper.Map<UpdateUserInterestCommand>(userInterestModel);
                    var result = await _mediator.Send(updateUserInterestCommand);
                    if (result.Succeeded)
                    {
                        var viewModel = _mapper.Map<UserInterestModel>(result.Data);
                        _notify.Information($"User interest {viewModel.Interest?.Name} changed");
                    }
                    return await ReturnResult();
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", userInterestModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int interestid)
        {
            var userinterest = await _mediator.Send(new GetUserInterestByQuery() { UserId = _authenticatedUser.UserId, InterestId = interestid });
            if (userinterest.Succeeded)
            {
                var userInterestModel = _mapper.Map<UserInterestModel>(userinterest.Data);
                var deleteCommand = await _mediator.Send(new DeleteUserInterestCommand { InterestId = interestid, UserId = _authenticatedUser.UserId });
                if (deleteCommand.Succeeded)
                {
                    _notify.Information($"Interest {userInterestModel.Interest?.Name} deleted");
                    return await ReturnResult();
                }
                else
                {
                    _notify.Error(deleteCommand.Message);
                    return null;

                }
            }
            _notify.Error(userinterest.Message);
            return null;
        }
              
    }
}
