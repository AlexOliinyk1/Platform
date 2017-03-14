using System;
using System.Threading.Tasks;
using Platform.Core.Models.Auth;

namespace Platform.Core.Services
{
    public interface IAuthService:IDisposable
    {
        Task<bool> RegisterUser(UserModel model);
        Task<bool> IsUserExist(string login, string password);
    }
}
