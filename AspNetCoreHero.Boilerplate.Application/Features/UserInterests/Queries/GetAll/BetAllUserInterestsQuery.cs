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

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries.GetAll
{
    public class GetAllUserInterestsQuery : IRequest<Result<List<GetUserInterestResponse>>>
    {
        public GetAllUserInterestsQuery(){
            }

    }
    public class GetAllUserInterestQueryHandler : IRequestHandler<GetAllUserInterestsQuery, Result<List<GetUserInterestResponse>>{
        IUserInterestRepository _userInterestRepository;
        IMapper _mapper;
        public GetAllUserInterestQueryHandler(IUserInterestRepository userInterestRepository,IMapper mapper)
        {
            _userInterestRepository = userInterestRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<GetUserInterestResponse>>> Handle(GetAllUserInterestsQuery request, CancellationToken cancellationToken)
        {
            var userInterests = await _userInterestRepository.GetAllAsync();
            var mappedUserInterest = _mapper.Map<List<GetUserInterestResponse>>(userInterests);
            return Result < List<GetUserInterestResponse>>.Success(mappedUserInterest);
        }
    }
}
