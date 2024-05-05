namespace eShop.ProductWebApi
{
    public static class Utitlites
    {
        public static long ArticleGenerator()
        {
            var article = new Random().NextInt64(100_000_000, 100_000_000_000);
            return article;
        }
    }
}
