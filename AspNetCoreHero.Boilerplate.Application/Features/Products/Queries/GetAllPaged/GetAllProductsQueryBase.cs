using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Specifications;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged
{
    public class GetAllProductsQueryBase : IGetAllWithPageBaseQuery<PaginatedResult<GetAllPagedProductsResponse>> 
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        

        public GetAllProductsQueryBase(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetAllProductsQueryBaseHandler<T>   : IRequestHandler<T, PaginatedResult<GetAllPagedProductsResponse>>  where T : IGetAllWithPageBaseQuery<PaginatedResult<GetAllPagedProductsResponse>>
    {
        protected readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryBaseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedProductsResponse>> Handle(T request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetAllPagedProductsResponse>> expression = e => new GetAllPagedProductsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Rate = e.Rate,
                Barcode = e.Barcode,
                Brand = e.Brand.Name,
                BrandId = e.BrandId
            };
            var productFilterSpec = GetSpecification(request);
            var data = await _unitOfWork.Repository<Product>().Entities
               .Specify(productFilterSpec)
               .Select(expression)
               .OrderBy(GetSortCollection(request))
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data;
        }
        protected virtual ISpecification<Product> GetSpecification(T t)
        {
            return new ProductFilterSpecification();
        }
        protected virtual Dictionary<string,string> GetSortCollection(T t)
        {
            return new Dictionary<string, string>();
        }
    }
}