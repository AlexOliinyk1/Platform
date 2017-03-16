using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Core.Models.Contacts;
using Platform.Core.Services;
using System.Linq;
using System.Data.Entity;
using Platform.DataAccess.Resources.Entities;
using System.Data.Entity.Migrations;
using Platform.Core.Common;

namespace Platform.DataAccess.Resources.Repositories
{
    public class ContactService : IContactService
    {
        private readonly ResourcesContext _context;

        /// <summary>
        ///     Ctor.
        /// </summary>
        public ContactService()
        {
            _context = new ResourcesContext();
        }

        /// <summary>
        /// Creates the contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        public Task<bool> CreateContact(ContactModel contact)
        {
            Contact newContact = new Contact {
                Name = contact.Name,
                ContactType = contact.ContactType,
                IsCompany = contact.IsCompany,
                Email = contact.Email,
                VatNumber = contact.VatNumber,
                PhoneNumber = contact.PhoneNumber,
                Title = contact.Title,
                Address = new Address {
                    AddressLine = contact.Street,
                    City = contact.City,
                    Country = contact.Country,
                    Zip = contact.Zip
                }
            };

            return CreateContact(newContact);
        }

        /// <summary>
        /// Creates the contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        public Task<bool> CreateContact(FastContactModel contact)
        {
            Contact newContact = new Contact {
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber
            };

            return CreateContact(newContact);
        }

        /// <summary>
        /// Gets all contacts.
        /// </summary>
        /// <returns></returns>
        public Task<List<ContactListModel>> GetAllContacts()
        {
            return ToContactList(_context.Contacts.AsQueryable()).ToListAsync();
        }

        /// <summary>
        /// Gets the contacts.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public Task<List<ContactListModel>> GetContacts(ContactsPagingModel page)
        {
            int skip = page.CurrentPage * page.ByPage;

            var pagedQuery = _context.Contacts
                .OrderBy(x => x.Name)
                .Where(x =>
                    x.Name.Contains(page.SearchWord)
                    || x.Address.AddressLine.Contains(page.SearchWord)
                    || x.Address.Zip.Contains(page.SearchWord)
                );

            if(!ContactTypes.ALL.Equals(page.ContactType, StringComparison.InvariantCultureIgnoreCase))
            {
                pagedQuery = pagedQuery
                    .Where(x => x.ContactType.Equals(page.ContactType, StringComparison.InvariantCultureIgnoreCase));
            }

            //pagedQuery = pagedQuery
            //    .Skip(skip)
            //    .Take(page.ByPage);

            return ToContactList(pagedQuery).ToListAsync();
        }

        /// <summary>
        /// Creates the contacts.
        /// </summary>
        /// <param name="contacts">The contacts.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<bool> CreateContacts(IEnumerable<ContactModel> contacts)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Creates the contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        private Task<bool> CreateContact(Contact contact)
        {
            return Task<bool>.Factory.StartNew(() => {
                try
                {
                    _context.Contacts.AddOrUpdate(contact);
                    _context.SaveChanges();

                    return true;
                }
                catch(Exception)
                {
                    //todo: can handle error here
                    return false;
                }
            });
        }

        /// <summary>
        /// To the contact list.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private IQueryable<ContactListModel> ToContactList(IQueryable<Contact> query)
        {
            return query
                .Include(x => x.Address)
                .Select(x => new ContactListModel {
                    Name = x.Name,
                    ZipCode = x.Address.Zip,
                    Address = x.Address.AddressLine
                });
        }
    }
}
