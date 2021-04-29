using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Delete
{
    public class DeleteInterestCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }

        public class DeleteInterestCommandHandler : IRequestHandler<DeleteInterestCommand, Result<string>>
        {
            private readonly IInterestRepository _interestRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteInterestCommandHandler(IInterestRepository interestRepository, IUnitOfWork unitOfWork)
            {
                _interestRepository = interestRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<string>> Handle(DeleteInterestCommand command, CancellationToken cancellationToken)
            {
                var interest = await _interestRepository.GetByIdAsync(command.Id);
                await _interestRepository.DeleteAsync(interest);
                await _unitOfWork.Commit(cancellationToken);
                return Result<string>.Success(interest.Name);
            }
        }
    }
}