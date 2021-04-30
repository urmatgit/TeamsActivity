using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries.GetById
{
     
    public class GetUserAllInterestCheckedQuery : IRequest<Result<List<InterestCheckedCachedResponse>>>

    {
        public string UserId { get; set; }
        
        public GetUserAllInterestCheckedQuery(string userid) : base()
        {
            UserId = userid;
        }

        public class GetUserAllInterestCheckedHandler : IRequestHandler<GetUserAllInterestCheckedQuery, Result<List<InterestCheckedCachedResponse>>>
        {
            IUserInterestRepository _userInterestRepository;
            IMapper _mapper;
            IInterestRepository _interestRepository;
            public async Task<Result<List<InterestCheckedCachedResponse>>> Handle(GetUserAllInterestCheckedQuery request, CancellationToken cancellationToken)
            {
                var userInterest = await _userInterestRepository.GetByIdAsync(request.UserId);
                var mappedUserInterest = _mapper.Map<List<GetUserInterestResponse>>(userInterest);

                var checkableAllinterest = new  List<InterestCheckedCachedResponse>();
                var allInterests = await _interestRepository.GetAllAsync();
                if (allInterests?.Count > 0)
                {
                    foreach(var interest in allInterests)
                    {
                        checkableAllinterest.Add(InterestCheckedCachedResponse.Create(interest, mappedUserInterest.Any(i => i.InterestId == interest.Id)));
                        
                    }
                }
                return Result<List<InterestCheckedCachedResponse>>.Success(checkableAllinterest);
            }
            public GetUserAllInterestCheckedHandler(IUserInterestRepository userInterestRepository,IInterestRepository interestRepository,  IMapper mapper)
            {
                _userInterestRepository = userInterestRepository;
                _mapper = mapper;
                _interestRepository = interestRepository;
            }
        }
    }


}
