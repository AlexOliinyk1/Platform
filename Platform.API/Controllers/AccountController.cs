using System.Threading.Tasks;
using System.Web.Http;
using Platform.Core.Models.Auth;
using Platform.Core.Services;

namespace Platform.API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IAuthService _authService;

        public AccountController(IAuthService auth)
        {
            _authService = auth;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool successed = await _authService.RegisterUser(userModel);
            if (!successed)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}