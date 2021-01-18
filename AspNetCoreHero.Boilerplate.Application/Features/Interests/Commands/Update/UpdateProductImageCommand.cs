using AspNetCoreHero.Boilerplate.Application.Exceptions;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Update
{
    public class UpdateInterestImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        public class UpdateInterestImageCommandHandler : IRequestHandler<UpdateInterestImageCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IInterestRepository _interestRepository;

            public UpdateInterestImageCommandHandler(IInterestRepository interestRepository, IUnitOfWork unitOfWork)
            {
                _interestRepository = interestRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateInterestImageCommand command, CancellationToken cancellationToken)
            {
                var interest = await _interestRepository.GetByIdAsync(command.Id);

                if (interest == null)
                {
                    throw new ApiException($"Interest Not Found.");
                }
                else
                {
                    interest.Image = command.Image;
                    await _interestRepository.UpdateAsync(interest);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(interest.Id);
                }
            }
        }
    }
}