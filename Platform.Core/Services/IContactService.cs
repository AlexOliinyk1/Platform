using Platform.Core.Models.Contacts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Core.Services
{
    public interface IContactService : IDisposable
    {
        Task<List<ContactListModel>> GetAllContacts();
        Task<bool> CreateContact(ContactModel contact);
        Task<bool> CreateContact(FastContactModel contact);
    }
}
