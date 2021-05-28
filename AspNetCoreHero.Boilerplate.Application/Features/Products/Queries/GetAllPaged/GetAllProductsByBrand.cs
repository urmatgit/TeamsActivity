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
    public class GetAllProductsByBrand : GetAllProductsQueryBase
    {
        public int BrandId { get; set; }
        public GetAllProductsByBrand(int brandId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            BrandId = brandId;
        }

    }
    public class GetAllProductsQueryByBrandHandler : GetAllProductsQueryBaseHandler<GetAllProductsByBrand>
    {
        public GetAllProductsQueryByBrandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override ISpecification<Product> GetSpecification (GetAllProductsByBrand t)
        {
            return new ProductFilterSpecification(t.BrandId);
        }
    }
}
