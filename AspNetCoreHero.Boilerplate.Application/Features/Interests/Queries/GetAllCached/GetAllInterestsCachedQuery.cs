using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached
{
    public class GetAllInterestsCachedQuery : IRequest<Result<List<InterestsCachedResponse>>>
    {
        public GetAllInterestsCachedQuery()
        {
        }
    }

    public class GetAllInterestsCachedQueryHandler : IRequestHandler<GetAllInterestsCachedQuery, Result<List<InterestsCachedResponse>>>
    {
        private readonly IInterestCacheRepository _interestCache;
        private readonly IMapper _mapper;

        public GetAllInterestsCachedQueryHandler(IInterestCacheRepository interestCache, IMapper mapper)
        {
            _interestCache = interestCache;
            _mapper = mapper;
        }

        public async Task<Result<List<InterestsCachedResponse>>> Handle(GetAllInterestsCachedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var interestList = await _interestCache.GetCachedListAsync();
                var mappedInterests = _mapper.Map<List<InterestsCachedResponse>>(interestList);
                return Result<List<InterestsCachedResponse>>.Success(mappedInterests);
            }
            catch (Exception er)
            {
                return Result<List<InterestsCachedResponse>>.Fail(er.Message);
            }
        }
    }
}