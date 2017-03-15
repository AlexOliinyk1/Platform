using Microsoft.AspNet.Identity.EntityFramework;

namespace Platform.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso />
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthContext"/> class.
        /// </summary>
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}
