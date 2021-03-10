using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries;
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

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Update
{
   public class UpdateUserInterestCommand: IRequest<Result<GetUserInterestResponse>>
    {
        public string  UserId { get; set; }
        public int InterestId { get; set; }
        public byte Level { get; set; }
    }
    public class UpdateUserInterestCommandHandler : IRequestHandler<UpdateUserInterestCommand, Result<GetUserInterestResponse>>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IMapper _mapper;
        public UpdateUserInterestCommandHandler(IUserInterestRepository userInterestRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _userInterestRepository = userInterestRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetUserInterestResponse>> Handle(UpdateUserInterestCommand request, CancellationToken cancellationToken)
        {
            var userInterest = await _userInterestRepository.GetByIdAsync(request.UserId, request.InterestId);
            if (userInterest == null)
            {
                return  Result<GetUserInterestResponse>.Fail($"User has`t current interest");
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
                        return  Result<GetUserInterestResponse>.Fail($"Can`t update");
                    }
                    return  Result<GetUserInterestResponse>.Success(_mapper.Map< GetUserInterestResponse>(newUserInterest));
                }
                return Result<GetUserInterestResponse>.Fail($"Nothin data to change");
            }
        }
    }
}
