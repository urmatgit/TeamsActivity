using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllPaged
{
    public class GetAllInterestsQuery : IRequest<PaginatedResult<GetAllInterestsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllInterestsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllInterestsQueryHandler : IRequestHandler<GetAllInterestsQuery, PaginatedResult<GetAllInterestsResponse>>
    {
        private readonly IInterestRepository _repository;

        public GGetAllInterestsQueryHandler(IInterestRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllInterestsResponse>> Handle(GetAllInterestsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Interest, GetAllInterestsResponse>> expression = e => new GetAllInterestsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description=e.Description
            };
            var paginatedList = await _repository.Entities // Interests
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}