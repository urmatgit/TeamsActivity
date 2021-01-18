using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached
{
    public class GetAllInterestsCachedQuery : IRequest<Result<List<GetAllInterestsCachedResponse>>>
    {
        public GetAllInterestsCachedQuery()
        {
        }
    }

    public class GetAllInterestsCachedQueryHandler : IRequestHandler<GetAllInterestsCachedQuery, Result<List<GetAllInterestsCachedResponse>>>
    {
        private readonly IInterestCacheRepository _interestCache;
        private readonly IMapper _mapper;

        public GetAllInterestsCachedQueryHandler(IInterestCacheRepository interestCache, IMapper mapper)
        {
            _interestCache = interestCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllInterestsCachedResponse>>> Handle(GetAllInterestsCachedQuery request, CancellationToken cancellationToken)
        {
            var interestList = await _interestCache.GetCachedListAsync();
            var mappedInterests = _mapper.Map<List<GetAllInterestsCachedResponse>>(interestList);
            return Result<List<GetAllInterestsCachedResponse>>.Success(mappedInterests);
        }
    }
}