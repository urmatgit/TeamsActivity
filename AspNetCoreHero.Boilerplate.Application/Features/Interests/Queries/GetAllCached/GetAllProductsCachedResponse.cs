﻿namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached
{
    public class GetAllInterestsCachedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int BrandId { get; set; }
    }
}