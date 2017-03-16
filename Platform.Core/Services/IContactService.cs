using Platform.Core.Models.Contacts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IContactService : IDisposable
    {
        Task<List<ContactModel>> GetAllContactModels();
        Task<List<ContactListModel>> GetAllContacts();
        Task<List<ContactListModel>> GetContacts(ContactsPagingModel page);
        Task<bool> CreateContact(ContactModel contact);
        Task<bool> CreateContact(FastContactModel contact);
    }
}
