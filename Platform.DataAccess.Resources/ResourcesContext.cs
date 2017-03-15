using Platform.DataAccess.Resources.Entities;
using System.Data.Entity;

namespace Platform.DataAccess.Resources
{
    class ResourcesContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesContext"/> class.
        /// </summary>
        public ResourcesContext()
            : base("ResourcesContext")
        {

        }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public DbSet<Address> Addresses { get; set; }

    }
}
