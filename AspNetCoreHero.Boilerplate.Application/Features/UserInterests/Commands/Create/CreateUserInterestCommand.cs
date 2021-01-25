using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Create
{
    public class CreateUserInterestCommand:IRequest<Result>
    {
        public string UserId { get; set; }
        public int InterestId { get; set; }
        public byte Level { get; set; }

    }
    public class CreateUserInterestCommmandHandler : IRequestHandler<CreateUserInterestCommand, Result>
    {
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateUserInterestCommmandHandler(IUserInterestRepository userInterestRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _userInterestRepository = userInterestRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateUserInterestCommand request, CancellationToken cancellationToken)
        {
            var userInterest = _mapper.Map<UserInterest>(request);
            await _userInterestRepository.AddAsync(userInterest);
            await _unitOfWork.Commit(cancellationToken);
            return (Result)Result.Success();
        }
    }

}
