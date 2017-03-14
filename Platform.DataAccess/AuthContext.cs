using Microsoft.AspNet.Identity.EntityFramework;

namespace Platform.DataAccess
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}
