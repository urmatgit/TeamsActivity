using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries;
using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries.GetById;
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

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Edit
{
   public class EditUserInterestCommand: IRequest<Result<int>>
    {
        public string  UserId { get; set; }
        public IList<InterestCheckedCachedResponse> Interests { get; set; }
        
    }
    public class EditUserInterestCommandHandler : IRequestHandler<EditUserInterestCommand, Result<int>>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IMapper _mapper;
        readonly IMediator _mediator;
        
        public EditUserInterestCommandHandler(IUserInterestRepository userInterestRepository, IUnitOfWork unitOfWork,IMapper mapper,IMediator mediator  )
        {
            _userInterestRepository = userInterestRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Result<int>> Handle(EditUserInterestCommand request, CancellationToken cancellationToken)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (InterestCheckedCachedResponse interestChecked in request.Interests)
            {
                var check = await _mediator.Send(new GetUserInterestByQuery() { UserId = request.UserId, InterestId = interestChecked.Id });
                
                if (interestChecked.Check)
                {
                    if (check.Data == null)
                    {
                        var resultCreate = await _mediator.Send(new CreateUserInterestCommand() { UserId = request.UserId, InterestId = interestChecked.Id });
                        if (resultCreate.Failed)
                        {
                            stringBuilder.AppendLine($"{interestChecked.Name} {resultCreate.Message}");

                        }
                    }
                }else
                {
                    if (check.Data != null)
                    {
                        var resultDelete = await _mediator.Send(new DeleteUserInterestCommand() { UserId = request.UserId, InterestId = interestChecked.Id });
                        if (resultDelete.Failed)
                        {
                            stringBuilder.AppendLine($"{interestChecked.Name} {resultDelete.Message}");
                        }
                    }
                }
            }
            if (stringBuilder.Length > 0)
            {
                return   Result<int>.Fail(stringBuilder.ToString());
            }
            else
                return Result<int>.Success(1);
           
        }
    }
}
