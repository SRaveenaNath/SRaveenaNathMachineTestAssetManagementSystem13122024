using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRaveenaNathMachineTestAssetManagementSystem.Repository;

namespace SRaveenaNathMachineTestAssetManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            var token = await _loginRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
