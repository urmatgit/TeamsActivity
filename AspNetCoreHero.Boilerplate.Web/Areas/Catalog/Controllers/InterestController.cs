using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetById;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using AspNetCoreHero.Boilerplate.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class InterestController : BaseController<InterestController>
    {
        public IActionResult Index()
        {
            var model = new InterestViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllInterestsCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<InterestViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

      //  [Authorize(Policy = Permissions.Users.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var brandsResponse = await _mediator.Send(new GetAllBrandsCachedQuery());

            if (id == 0)
            {
                var interestViewModel = new InterestViewModel();
                if (brandsResponse.Succeeded)
                {
                    var brandViewModel = _mapper.Map<List<BrandViewModel>>(brandsResponse.Data);
                    
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", interestViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetInterestByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var interestViewModel = _mapper.Map<InterestViewModel>(response.Data);
                    if (brandsResponse.Succeeded)
                    {
                        var brandViewModel = _mapper.Map<List<BrandViewModel>>(brandsResponse.Data);
                        
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", interestViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, InterestViewModel interest)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createInterestCommand = _mapper.Map<CreateInterestCommand>(interest);
                    var result = await _mediator.Send(createInterestCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Interest with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateInterestCommand = _mapper.Map<UpdateInterestCommand>(interest);
                    var result = await _mediator.Send(updateInterestCommand);
                    if (result.Succeeded) _notify.Information($"Interest with ID {result.Data} Updated.");
                }
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    var image = file.OptimizeImageSize(700, 700);
                    await _mediator.Send(new UpdateInterestImageCommand() { Id = id, Image = image });
                }
                var response = await _mediator.Send(new GetAllInterestsCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<InterestViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", interest);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteInterestCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Interest with Id {deleteCommand.Data} Deleted.");
                var response = await _mediator.Send(new GetAllInterestsCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<InterestViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}