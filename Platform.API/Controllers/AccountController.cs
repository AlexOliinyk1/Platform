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

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        public AccountController(IAuthService auth)
        {
            _authService = auth;
        }

        // POST api/Account/Register
        /// <summary>
        /// Registers the specified user model.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
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