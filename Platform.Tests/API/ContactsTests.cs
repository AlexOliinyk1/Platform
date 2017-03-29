using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Platform.API.Controllers;
using Platform.Core.Models.Contacts;
using Platform.Core.Services;
using Platform.Core.Utilities;
using Platform.DataAccess.Resources.Repositories;
using Platform.Utilities.Parsers;

namespace Platform.Tests.API
{
    public class ContactsTests
    {
        private IExcelParser<ContactModel> _contactParser;
        private IContactService _contactService;
        private List<ContactModel> _contacts;
        private const string ServiceBaseURL = "http://localhost:57090/";

        [TestFixtureSetUp]
        public void Setup()
        {
            _contacts = SetUpContacts();
            _contactParser = new ContactParser();
            _contactService = SetUpContactService();
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            _contactParser = null;
            _contactService = null;
        }

        private IContactService SetUpContactService()
        {
            var contactServiceMock = new Mock<IContactService>();
            contactServiceMock.Setup(x => x.CreateContact(It.IsAny<ContactModel>())).Callback<ContactModel>(newContact =>
            {
                int maxID = _contacts.Last().Id;
                int nextID = maxID + 1;
                newContact.Id = nextID;
                _contacts.Add(newContact);
            });

            contactServiceMock.Setup(x => x.GetAllContactModels()).Returns(Task.FromResult(_contacts));
            return contactServiceMock.Object;
        }

        private List<ContactModel> SetUpContacts()
        {
            var Id = new int();
            var contacts = new List<ContactModel>()
            {
                new ContactModel()
                {
                    Name = "John Dou",
                    Email = "JohnDou@mail.com",
                    ContactType = "customer"
                },
                                new ContactModel()
                {
                    Name = "Jain Dou",
                    Email = "JainDou@mail.com",
                    ContactType = "customer"
                }
            };
            foreach (var item in contacts)
                item.Id = ++Id;
            return contacts;
        }

        //tests
        [Test]
        public void GetAllContactsTest()
        {
            //ACT
            var contactController = new ContactsController(_contactParser,_contactService)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "api/Contacts/GetAllContactModels")
                }
            };
            contactController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            HttpResponseMessage response = contactController.GetAllContactModels();

            var responseResult = JsonConvert.DeserializeObject<List<ContactModel>>(response.Content.ReadAsStringAsync().Result);


            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);
            bool result = CompareContacts(responseResult.OrderBy(x => x.Id).ToList(),
                _contacts.OrderBy(x => x.Id).ToList());
            Assert.AreEqual(result, true);
        }

        private bool CompareContacts(IList<ContactModel> arr1, IList<ContactModel> arr2)
        {
            bool result = true;
            for (int i = 0; i < arr1.Count; i++)
            {
                if (arr1[i].Id != arr2[i].Id)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
