namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetById
{
    public class GetInterestByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        
    }
}