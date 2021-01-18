using AspNetCoreHero.Boilerplate.API.Controllers;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class InterestController : BaseApiController<InterestController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var interests = await _mediator.Send(new GetAllInterestsQuery(pageNumber, pageSize));
            return Ok(interests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var interest = await _mediator.Send(new GetInterestByIdQuery() { Id = id });
            return Ok(interest);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateInterestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateInterestCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteInterestCommand { Id = id }));
        }
    }
}