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
            var newContact = ParseContactFromModel(contact);

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
        /// Get full models of all contacts 
        /// </summary>
        /// <returns></returns>
        public Task<List<ContactModel>> GetAllContactModels()
        {
            return ToContactModelList(_context.Contacts.AsQueryable()).ToListAsync();
        }

        /// <summary>
        /// Gets the contacts.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public Task<List<ContactListModel>> GetContacts(ContactsPagingModel page)
        {
            var pagedQuery = _context.Contacts.AsQueryable();

            if(!string.IsNullOrEmpty(page.SearchWord))
            {
                pagedQuery = pagedQuery.Where(x => x.Name.Contains(page.SearchWord) || x.Address.AddressLine.Contains(page.SearchWord) || x.Address.Zip.Contains(page.SearchWord));
            }

            if(!ContactTypes.ALL.Equals(page.ContactType, StringComparison.InvariantCultureIgnoreCase))
            {
                pagedQuery = pagedQuery.Where(x => x.ContactType.Equals(page.ContactType, StringComparison.InvariantCultureIgnoreCase));
            }

            //int skip = page.CurrentPage * page.ByPage;
            //pagedQuery = pagedQuery
            //    .OrderBy(x => x.Name)
            //    .Skip(skip)
            //    .Take(page.ByPage);

            return ToContactList(pagedQuery).ToListAsync();
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

        /// <summary>
        /// Convert contact esntities to Ui models.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private IQueryable<ContactModel> ToContactModelList(IQueryable<Contact> query)
        {
            return query
                .Include(x => x.Address)
                .Select(x => new ContactModel {
                    Name = x.Name,
                    Zip = x.Address.Zip,
                    Street = x.Address.AddressLine,
                    City = x.Address.City,
                    ContactType = x.ContactType,
                    Country = x.Address.Country,
                    Email = x.Email,
                    IsCompany = x.IsCompany,
                    PhoneNumber = x.PhoneNumber,
                    Title = x.Title,
                    VatNumber = x.VatNumber
                });
        }

        /// <summary>
        /// Convert UI contact model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Contact ParseContactFromModel(ContactModel model)
        {
            return new Contact {
                Name = model.Name,
                ContactType = model.ContactType,
                IsCompany = model.IsCompany,
                Email = model.Email,
                VatNumber = model.VatNumber,
                PhoneNumber = model.PhoneNumber,
                Title = model.Title,
                Address = new Address {
                    AddressLine = model.Street,
                    City = model.City,
                    Country = model.Country,
                    Zip = model.Zip
                }
            };
        }
    }
}
