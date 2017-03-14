using Platform.DataAccess.Resources.Entities;
using System.Data.Entity;

namespace Platform.DataAccess.Resources
{
    class ResourcesContext : DbContext
    {
        public ResourcesContext()
            : base("ResourcesContext")
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
