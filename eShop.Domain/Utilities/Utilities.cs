namespace eShop.ProductWebApi
{
    public static class Utilities
    {
        public static long ArticleGenerator()
        {
            var article = new Random().NextInt64(100_000_000, 100_000_000_000);
            return article;
        }
    }
}
