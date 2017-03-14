using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Platform.Core.Models.Auth;
using Platform.Core.Services;

namespace Platform.DataAccess.Repositories
{
    public class AuthService : IAuthService
    {
        private AuthContext _ctx;
        private UserManager<IdentityUser> _userManager;

        public AuthService()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<bool> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result.Succeeded;
        }

        public async Task<bool> IsUserExist(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);
            return user != null;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}