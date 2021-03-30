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
   public class GetInterestsByUserIdQuery: IRequest<Result<List<GetUserInterestResponse>>>
    
    {
        public string UserId { get; set; }
        public GetInterestsByUserIdQuery(string userid):base()
        {
            UserId = userid;
        }
        
        public class GetInterestsByUserIdQueryHandler : IRequestHandler<GetInterestsByUserIdQuery, Result<List<GetUserInterestResponse>>>        {
            IUserInterestRepository _userInterestRepository;
            IMapper _mapper;
            public async Task<Result<List<GetUserInterestResponse>>> Handle(GetInterestsByUserIdQuery request, CancellationToken cancellationToken)
            {
                var userInterest = await _userInterestRepository.GetByIdAsync(request.UserId);
                var mappedUserInterest = _mapper.Map<List<GetUserInterestResponse>>(userInterest);
                return Result<List<GetUserInterestResponse>>.Success(mappedUserInterest);
            }
            public GetInterestsByUserIdQueryHandler(IUserInterestRepository userInterestRepository, IMapper mapper)
            {
                _userInterestRepository = userInterestRepository;
                _mapper = mapper;
            }
        }
    }
}
