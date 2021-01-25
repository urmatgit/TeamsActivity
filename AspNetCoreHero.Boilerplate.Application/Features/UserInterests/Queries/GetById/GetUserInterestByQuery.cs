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
    public class GetUserInterestByQuery : IRequest<Result<GetUserInterestResponse>>
    {
        public string UserId { get; set; }
        public int InterestId { get; set; }

        public class GetUserIntestByIdQueryHandler : IRequestHandler<GetUserInterestByQuery, Result<GetUserInterestResponse>>
        {
            IUserInterestRepository _userInterestRepository;
            IMapper _mapper;
            public async Task<Result<GetUserInterestResponse>> Handle(GetUserInterestByQuery request, CancellationToken cancellationToken)
            {
                var userInterest = await _userInterestRepository.GetByIdAsync(request.UserId, request.InterestId);
                var mappedUserInterest = _mapper.Map<GetUserInterestResponse>(userInterest);
                return Result<GetUserInterestResponse>.Success(mappedUserInterest);
            }
            public GetUserIntestByIdQueryHandler(IUserInterestRepository userInterestRepository, IMapper mapper)
            {
                _userInterestRepository = userInterestRepository;
                _mapper = mapper;
            }
        }

        
        }
    }


