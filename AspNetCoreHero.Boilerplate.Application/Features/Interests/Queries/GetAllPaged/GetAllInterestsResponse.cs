using AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries;
using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllPaged
{
    public class GetAllInterestsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        

    }
}