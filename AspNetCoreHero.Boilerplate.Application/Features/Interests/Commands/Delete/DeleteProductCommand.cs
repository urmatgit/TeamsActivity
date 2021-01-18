using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Delete
{
    public class DeleteInterestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteInterestCommandHandler : IRequestHandler<DeleteInterestCommand, Result<int>>
        {
            private readonly IInterestRepository _interestRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteInterestCommandHandler(IInterestRepository interestRepository, IUnitOfWork unitOfWork)
            {
                _interestRepository = interestRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteInterestCommand command, CancellationToken cancellationToken)
            {
                var interest = await _interestRepository.GetByIdAsync(command.Id);
                await _interestRepository.DeleteAsync(interest);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(interest.Id);
            }
        }
    }
}