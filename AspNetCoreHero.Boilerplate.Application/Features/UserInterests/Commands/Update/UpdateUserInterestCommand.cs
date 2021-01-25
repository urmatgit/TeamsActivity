using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Update
{
   public class UpdateUserInterestCommand: IRequest<Result>
    {
        public string  UserId { get; set; }
        public int InterestId { get; set; }
        public byte Level { get; set; }
    }
    public class UpdateUserInterestCommandHandler : IRequestHandler<UpdateUserInterestCommand, Result>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInterestRepository _userInterestRepository;
        public UpdateUserInterestCommandHandler(IUserInterestRepository userInterestRepository, IUnitOfWork unitOfWork)
        {
            _userInterestRepository = userInterestRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<Result> Handle(UpdateUserInterestCommand request, CancellationToken cancellationToken)
        {
            var userInterest = await _userInterestRepository.GetByIdAsync(request.UserId, request.InterestId);
            if (userInterest == null)
            {
                return (Result)Result.Fail($"User has`t current interest");
            }
            else
            {
                if (userInterest.UserId != request.UserId 
                    || userInterest.InterestId != request.InterestId
                    || userInterest.Level != request.Level) {
                    await _userInterestRepository.DeleteAsync(userInterest);
                    await _unitOfWork.Commit(cancellationToken);
                    var newUserInterest = new UserInterest
                    {
                        UserId = request.UserId,
                        InterestId = request.InterestId,
                        Level = request.Level
                    };
                    try
                    {
                        await _userInterestRepository.AddAsync(newUserInterest);
                        await _unitOfWork.Commit(cancellationToken);
                    }catch (Exception er)
                    {
                        await _unitOfWork.Rollback();
                        return (Result)Result.Fail($"Can`t update");
                    }
                    return (Result)Result.Success();
                }
                return (Result)Result.Fail($"Nothin data to change");
            }
        }
    }
}
