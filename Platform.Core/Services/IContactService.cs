using Platform.Core.Models.Contacts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Core.Services
{
    public interface IContactService : IDisposable
    {
        Task<IEnumerable<ContactListModel>> GetAllContacts();
        Task<IEnumerable<ContactListModel>> GetContacts(ContactsPagingModel page);
        Task<bool> CreateContact(ContactModel contact);
        Task<bool> CreateContact(FastContactModel contact);
        Task<bool> CreateContacts(IEnumerable<ContactModel> contacts);
    }
}
