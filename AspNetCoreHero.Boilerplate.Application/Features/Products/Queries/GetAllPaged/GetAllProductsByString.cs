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
    public class GetAllProductsByString : GetAllProductsQueryBase
    {
        public string SearchString { get; set; }
        public GetAllProductsByString(int pageNumber, int pageSize ,string searchString) : base(pageNumber, pageSize)
        {
            SearchString = searchString;
        }

    }
    public class GetAllProductsByStringHandler : GetAllProductsQueryBaseHandler<GetAllProductsByString>
    {
        public GetAllProductsByStringHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override ISpecification<Product> GetSpecification (GetAllProductsByString t)
        {
            return new ProductFilterSpecification(t.SearchString);
        }
    }
}
