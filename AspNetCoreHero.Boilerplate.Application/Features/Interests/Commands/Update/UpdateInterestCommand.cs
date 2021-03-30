using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Update
{
    public class UpdateInterestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class UpdateInterestCommandHandler : IRequestHandler<UpdateInterestCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IInterestRepository _interestRepository;

            public UpdateInterestCommandHandler(IInterestRepository interestRepository, IUnitOfWork unitOfWork)
            {
                _interestRepository = interestRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateInterestCommand command, CancellationToken cancellationToken)
            {
                var interest = await _interestRepository.GetByIdAsync(command.Id);

                if (interest == null)
                {
                    return Result<int>.Fail($"Interest Not Found.");
                }
                else
                {
                    interest.Name = command.Name ?? interest.Name;
                    interest.Description = command.Description;
                    await _interestRepository.UpdateAsync(interest);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(interest.Id);
                }
            }
        }
    }
}