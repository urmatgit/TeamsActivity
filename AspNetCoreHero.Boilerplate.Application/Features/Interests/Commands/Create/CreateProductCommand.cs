using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Create
{
    public partial class CreateInterestCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int BrandId { get; set; }
    }

    public class CreateInterestCommandHandler : IRequestHandler<CreateInterestCommand, Result<int>>
    {
        private readonly IInterestRepository _interestRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateInterestCommandHandler(IInterestRepository interestRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _interestRepository = interestRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateInterestCommand request, CancellationToken cancellationToken)
        {
            var interest = _mapper.Map<Interest>(request);
            await _interestRepository.InsertAsync(interest);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(interest.Id);
        }
    }
}