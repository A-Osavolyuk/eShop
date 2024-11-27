using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace eShop.Infrastructure
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddInfrastructureLayer(this IHostApplicationBuilder builder)
        {
            builder.AddDependencyInjection();
            builder.AddJwtAuthentication();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddHttpContextAccessor();
            return builder;
        }

        public static IHostApplicationBuilder AddJwtAuthentication(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["JwtOptions:Audience"],
                    ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]!))
                };
            });

            return builder;
        }

        private static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<AuthenticationStateProvider, ApplicationAuthenticationStateProvider>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddHttpClient<IBrandService, BrandSevice>();
            builder.Services.AddHttpClient<ICommentService, CommentService>();
            builder.Services.AddHttpClient<IReviewService, ReviewService>();
            builder.Services.AddHttpClient<ICartService, CartService>();
            builder.Services.AddHttpClient<IFavoritesService, FavoritesService>();
            builder.Services.AddHttpClient<IStoreService, StoreService>();
            builder.Services.AddHttpClient<ISellerService, SellerService>();

            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddScoped<ITokenProvider, TokenProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBrandService, BrandSevice>();
            builder.Services.AddScoped<ILocalDataAccessor, LocalDataAccessor>();
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IFavoritesService, FavoritesService>();
            builder.Services.AddScoped<ICookieManager, CookieManager>();
            builder.Services.AddScoped<ISellerService, SellerService>();

            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddSingleton<InputImagesStateContainer>();
            builder.Services.AddSingleton<NotificationsStateContainer>();

            return builder;
        }

    }
}
