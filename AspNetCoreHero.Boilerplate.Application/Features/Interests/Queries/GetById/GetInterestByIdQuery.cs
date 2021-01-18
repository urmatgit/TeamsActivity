using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetById
{
    public class GetInterestByIdQuery : IRequest<Result<GetInterestByIdResponse>>
    {
        public int Id { get; set; }

        public class GetInterestByIdQueryHandler : IRequestHandler<GetInterestByIdQuery, Result<GetInterestByIdResponse>>
        {
            private readonly IInterestCacheRepository _interestCache;
            private readonly IMapper _mapper;

            public GetInterestByIdQueryHandler(IInterestCacheRepository interestCache, IMapper mapper)
            {
                _interestCache = interestCache;
                _mapper = mapper;
            }

            public async Task<Result<GetInterestByIdResponse>> Handle(GetInterestByIdQuery query, CancellationToken cancellationToken)
            {
                var interest = await _interestCache.GetByIdAsync(query.Id);
                var mappedInterest = _mapper.Map<GetInterestByIdResponse>(interest);
                return Result<GetInterestByIdResponse>.Success(mappedInterest);
            }
        }
    }
}