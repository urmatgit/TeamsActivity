using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Specifications;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged
{
    public class GetAllProductsByBrand : GetAllProductsWithSort
    {
        public int BrandId { get; set; }
        public GetAllProductsByBrand(int brandId, int pageNumber, int pageSize, string searchString, string sortColumn, string sortColumnDirection) 
            : base(pageNumber, pageSize,searchString,sortColumn,sortColumnDirection)
        {
            BrandId = brandId;
        }

    }
    public class GetAllProductsQueryByBrandHandler : GetAllProductsQueryBaseHandler<GetAllProductsByBrand>
    {
        public GetAllProductsQueryByBrandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override ISpecification<Product> GetSpecification(GetAllProductsByBrand t)
        {
            var filterSpec= new ProductFilterSpecification( t.SearchString );
            filterSpec.AddCriteriaAnd(p => p.BrandId == t.BrandId);
            return filterSpec;
        }
        protected override Dictionary<string, string> GetSortCollection(GetAllProductsByBrand t)
        {
            if (string.IsNullOrEmpty(t.SortColumn)) return base.GetSortCollection(t);
            var sortModel = new Dictionary<string, string>
            {
                {t .SortColumn,t.SortColumnDirection }
            };
            return sortModel;
        }
    }
}
