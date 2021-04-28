using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Edit;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using AspNetCoreHero.Boilerplate.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var model = new UserInterestViewModel();
            if (User.IsUserAdmins())
                return View("AdminIndex", model);
            return View("UserIndex",model);
        }
        public async Task<IActionResult> LoadAll()
        {

          

            var response = await _mediator.Send(new GetInterestsByUserIdQuery(_authenticatedUser.UserId));
            if (response.Succeeded)
            {

                var viewUserInterestModels = _mapper.Map<List<UserInterestViewModel>>(response.Data);
          
                return PartialView("_ViewAll", viewUserInterestModels);
            }
            return null;
        }
        [HttpPost]
        public async Task<JsonResult> GetUserInterestEdit( UserInterestsCheckabelViewModel userinterests)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(userinterests.UserId))
                {
                    userinterests.UserId = _authenticatedUser.UserId;
                    
                }
                var editUserInterts = await _mediator.Send(new EditUserInterestCommand() 
                { 
                    UserId = userinterests.UserId, Interests = _mapper.Map<IList<InterestCheckedCachedResponse>>(userinterests.Interestes) 
                });
                if (editUserInterts.Succeeded)
                {
                    _notify.Success($"{_localizer["Interests added"]}");
                }else
                {
                    _notify.Error($"{_localizer["Interests is`t added"]}\n {editUserInterts.Message}");
                }
               

            }
            else
            {
                userinterests.UserId = _authenticatedUser.UserId;
                userinterests.UserName = _authenticatedUser.Username;
                var html = await _viewRenderer.RenderViewToStringAsync("_AddOrEdit", userinterests);
                return new JsonResult(new { isValid = false, html = html });
            }
            return await ReturnResult();
        }
        private  async Task<UserInterestsCheckabelViewModel> GetUserInterestListModel()
        {
            var response = await _mediator.Send(new GetInterestsByUserIdQuery(_authenticatedUser.UserId));
            var responseInterest = await _mediator.Send(new GetAllInterestsQuery(0,0));
            var allInterstes = _mapper.Map<List<InterestViewModel>>(responseInterest.Data);
            var model = new UserInterestsCheckabelViewModel();
            if (response.Succeeded)
            {

                var viewUserInterestModels = _mapper.Map<List<UserInterestViewModel>>(response.Data);

                List<CheckableInterestViewModel> checkableInterestViewModels = new List<CheckableInterestViewModel>();
                foreach (var interest in allInterstes)
                {
                    if (viewUserInterestModels.Any(vm => vm.InterestId == interest.Id))

                        checkableInterestViewModels.Add(CheckableInterestViewModel.Create(interest, true));
                    else
                        checkableInterestViewModels.Add(CheckableInterestViewModel.Create(interest, false));

                }

                model.UserId = _authenticatedUser.UserId;
                model.UserName = _authenticatedUser.Username;
                model.Interestes = checkableInterestViewModels;


            }
            return model;
        }
        public async Task<JsonResult> GetUserInterestEdit()
        {
            
                var UserId = _authenticatedUser.UserId;
            var userViewModel = await GetUserInterestListModel();

            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_AddOrEdit", userViewModel) });
        }
        // GET: UserInterestController/Create
        public async Task<JsonResult> OnGetAddOrEdit(string userid,int interestid)
        {
            if (string.IsNullOrEmpty(userid))
            {

                _notify.Error($"User id is empty!");
                var userViewModel = new UserInterestViewModel();
                return new JsonResult(new { isValid = false, html = await _viewRenderer.RenderViewToStringAsync("_AddOrEdit", userViewModel)  }); ;
             }
            if (interestid == 0)
            {
                var userViewModel = new UserInterestViewModel() { UserId= _authenticatedUser.UserId};
                return new JsonResult(new {isValid=true, html = await _viewRenderer.RenderViewToStringAsync("_AddOrEdit", userViewModel) });
        }
            else
            {

                var response = await _mediator.Send(new GetUserInterestByQuery() { 
                    UserId=userid,
                    InterestId=interestid
                });
                if (response.Succeeded)
                {
                    var userinterestViewModel = _mapper.Map<UserInterestViewModel>(response.Data);
                    return new JsonResult(new { isValid=true, html = await _viewRenderer.RenderViewToStringAsync("_AddOrEdit", userinterestViewModel) });
                }
                return null;

            }
        }
        private async Task<JsonResult> ReturnResult( )
        {
            var response = await _mediator.Send(new GetInterestsByUserIdQuery(_authenticatedUser.UserId));
            if (response.Succeeded)
            {

                var viewUserInterestModels = _mapper.Map<List<UserInterestViewModel>>(response.Data);
                var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewUserInterestModels);
                
                return new JsonResult(new { isValid = true, html = html });
            } else
            {
                _notify.Error(response.Message);
                return null;
            }

        }
        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(string userid, int interestid, UserInterestViewModel userInterestModel)
        {
            if (ModelState.IsValid)
            {
                if (interestid == 0)
                {
                    var createUserInterestCommand = _mapper.Map<CreateUserInterestCommand>(userInterestModel);
                    var result = await _mediator.Send(createUserInterestCommand);
                    if (result.Succeeded)
                    {
                        var viewModel = _mapper.Map<UserInterestViewModel>(result.Data);
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
                        var viewModel = _mapper.Map<UserInterestViewModel>(result.Data);
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
        public async Task<JsonResult> OnPostDelete(string userid, int interestid)
        {
            var userinterest = await _mediator.Send(new GetUserInterestByQuery() { UserId =userid, InterestId = interestid });
            if (userinterest.Succeeded)
            {
                var userInterestModel = _mapper.Map<UserInterestViewModel>(userinterest.Data);
                var deleteCommand = await _mediator.Send(new DeleteUserInterestCommand { InterestId = interestid, UserId =userid });
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
