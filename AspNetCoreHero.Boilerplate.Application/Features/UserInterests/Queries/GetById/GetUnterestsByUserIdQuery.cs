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
   public class GetUnterestsByUserIdQuery: IRequest<Result<List<GetUserInterestResponse>>>
    
    {
        public string UserId { get; set; }
        public class GetUnterestsByUserIdQueryHandler : IRequestHandler<GetUnterestsByUserIdQuery, Result<List<GetUserInterestResponse>>>        {
            IUserInterestRepository _userInterestRepository;
            IMapper _mapper;
            public async Task<Result<List<GetUserInterestResponse>>> Handle(GetUnterestsByUserIdQuery request, CancellationToken cancellationToken)
            {
                var userInterest = await _userInterestRepository.GetByIdAsync(request.UserId);
                var mappedUserInterest = _mapper.Map<List<GetUserInterestResponse>>(userInterest);
                return Result<List<GetUserInterestResponse>>.Success(mappedUserInterest);
            }
            public GetUnterestsByUserIdQueryHandler(IUserInterestRepository userInterestRepository, IMapper mapper)
            {
                _userInterestRepository = userInterestRepository;
                _mapper = mapper;
            }
        }
    }
}
