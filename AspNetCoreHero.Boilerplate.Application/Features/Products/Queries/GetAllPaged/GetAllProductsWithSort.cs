using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Specifications;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged
{
   public class GetAllProductsWithSort: GetAllProductsByString
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public GetAllProductsWithSort(int pageNumber, int pageSize, string searchString,string sortColumn,string sortColumnDirection)
            : base (pageNumber,pageSize,searchString)
        {

            SortColumn = sortColumn;
            SortColumnDirection = sortColumnDirection;
            

        }
    }
    public class GetAllProductsWithSortHandler : GetAllProductsQueryBaseHandler<GetAllProductsWithSort>
    {
        public GetAllProductsWithSortHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        protected override ISpecification<Product> GetSpecification(GetAllProductsWithSort t)
        {
            var spec=new  ProductFilterSpecification(t.SearchString);
            
            return spec;
        }
        protected override Dictionary<string, string> GetSortCollection(GetAllProductsWithSort t)
        {
            if (string.IsNullOrEmpty(t.SortColumn)) return base.GetSortCollection(t);
            var sortModel = new Dictionary<string, string>
            {
                { t.SortColumn,t.SortColumnDirection }
            };
            return sortModel;
        }
    }
}
