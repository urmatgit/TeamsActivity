using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Specifications
{
    public class ProductFilterSpecification: Specification<Product>
    {
        public ProductFilterSpecification( )
        {
            Includes.Add(a => a.Brand);
            Criteria = p => p.Barcode != null;
             
        }
        public ProductFilterSpecification(string searchString)
        {
            Includes.Add(a => a.Brand);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Barcode != null && (p.Name.Contains(searchString) || p.Description.Contains(searchString) || p.Barcode.Contains(searchString) || p.Brand.Name.Contains(searchString));
            }
            else
            {
                Criteria = p => p.Barcode != null;
            }
        }
        public ProductFilterSpecification(int  brandId)
        {
            
                Criteria = p => p.Barcode != null && p.BrandId==brandId;
            
        }
        public void AddCriteriaAnd(Expression<Func<Product, bool>> expr1)
        {
            Criteria = Criteria.And(expr1);
        }
        public void AddCriteriaOr(Expression<Func<Product, bool>> expr1)
        {
            Criteria = Criteria.Or(expr1);
        }
    }
}
