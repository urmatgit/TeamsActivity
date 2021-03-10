namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys
{
    public static class UserInterestCacheKeys 
    {
        public static string ListKey => "UserInterestList";

        public static string SelectListKey => "UserInterestSelectList";

        public static string GetKey(int Id) => $"UserInterest-{Id}";

        public static string GetDetailsKey(int Id) => $"UserInterestDetails-{Id}";
    }
}