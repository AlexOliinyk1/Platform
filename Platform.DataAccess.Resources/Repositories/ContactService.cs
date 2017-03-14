using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Core.Models.Contacts;
using Platform.Core.Services;
using System.Linq;
using System.Data.Entity;
using Platform.DataAccess.Resources.Entities;
using System.Data.Entity.Migrations;

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

        public Task<bool> CreateContact(ContactModel contact)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                Contact newContact = new Contact
                {
                    Name = contact.Name,
                    ContatctType = contact.ContactType,
                    IsCompany = contact.IsCompany,
                    Email = contact.Email,
                    VatNumber = contact.VatNumber,
                    PhoneNumber = contact.PhoneNumber,
                    Title = contact.Title,
                    Address = new Address
                    {
                        AddressLine = contact.Street,
                        City = contact.City,
                        Country = contact.Country,
                        Zip = contact.Zip
                    }
                };

                try
                {
                    _context.Contacts.AddOrUpdate(newContact);
                    _context.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            });
        }

        public Task<List<ContactListModel>> GetAllContacts()
        {
            return _context.Contacts
                .Include(x => x.Address)
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
