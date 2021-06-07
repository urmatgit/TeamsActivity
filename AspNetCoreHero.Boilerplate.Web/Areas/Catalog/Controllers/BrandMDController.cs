using AspNetCoreHero.Boilerplate.Application.Constants;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class BrandMDController : BaseController<BrandMDController>
    {

        public IActionResult Index()
        {
            var model = new BrandViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadBrandAll()
        {
            var response = await _mediator.Send(new GetAllBrandsCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<BrandViewModel>>(response.Data);
                return PartialView("_ViewBrandAll", viewModel);
            }
            return null;
        }
        //[Authorize(Policy = Permissions.Products.View)]
        //[HttpPost]
        public async Task<IActionResult> GetProduct(int brandId)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int BrandId = brandId;
                var response = await _mediator.Send(new GetAllProductsByBrand(BrandId,skip, pageSize, searchValue, sortColumn, sortColumnDirection));
                if (response.Succeeded)
                {
                    var viewModel = response.Data;
                    var jsonData = new { draw = draw, recordsFiltered = response.TotalCount, recordsTotal = response.TotalCount, data = viewModel };
                    return Ok(jsonData);
                }
            }
            catch (Exception er)
            {
                throw;
            }
            return null;
        }
    }
}
