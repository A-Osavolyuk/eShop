namespace eShop.AuthWebApi.Utilities
{
    public static class UserManagerExtensions
    {
        public static string GenerateRandomPassword(this UserManager<AppUser> userManager, int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(validChars.Length);
                sb.Append(validChars[randomIndex]);
            }

            return sb.ToString();

        }
    }
}
