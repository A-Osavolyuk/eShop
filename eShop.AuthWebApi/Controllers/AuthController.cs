using eShop.AuthWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.AuthWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
    }
}
