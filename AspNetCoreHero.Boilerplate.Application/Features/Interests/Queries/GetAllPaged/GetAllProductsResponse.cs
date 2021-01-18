﻿namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllPaged
{
    public class GetAllInterestsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
}