using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Delete
{
   public class DeleteUserInterestCommand: IRequest<Result>
    {
        public string UserId { get; set; }
        public int InterestId { get; set; }
    }
    public class DeleteUserInterestCommandHandler : IRequestHandler<DeleteUserInterestCommand, Result>
    {
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserInterestCommandHandler(IUserInterestRepository userInterestRepository,IUnitOfWork unitOfWork)
        {
            _userInterestRepository = userInterestRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteUserInterestCommand request, CancellationToken cancellationToken)
        {
            var userinterest = await _userInterestRepository.GetByIdAsync(request.UserId, request.InterestId);
            await _userInterestRepository.DeleteAsync(userinterest);
            await _unitOfWork.Commit(cancellationToken);
            return (Result)Result.Success();
        }
    }
}
