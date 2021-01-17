namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys
{
    public static class InterestCacheKeys
    {
        public static string ListKey => "InterestList";

        public static string SelectListKey => "InterestSelectList";

        public static string GetKey(int Id) => $"Interest-{Id}";

        public static string GetDetailsKey(int Id) => $"InterestDetails-{Id}";
    }
}