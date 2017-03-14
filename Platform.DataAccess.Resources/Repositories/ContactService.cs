using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Core.Models.Contacts;
using Platform.Core.Services;
using System.Linq;
using System.Data.Entity;

namespace Platform.DataAccess.Resources.Repositories
{
    class ContactService : IContactService
    {
        private ResourcesContext _context;

        /// <summary>
        ///     Ctor.
        /// </summary>
        public ContactService()
        {
            _context = new ResourcesContext();
        }

        public Task<bool> CreateUser(ContactModel contact)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                return true;
            });
        }

        public Task<List<ContactListModel>> GetAllContacts()
        {
            return _context.Contacts
                .Include(x=>x.Address)
                .Select(x => new ContactListModel
                {
                    Name = x.Name,
                    ZipCode = x.Address.Zip,
                    Address = x.Address.AddressLine
                })
                .ToListAsync();

        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
