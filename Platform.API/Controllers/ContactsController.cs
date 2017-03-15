using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using Platform.Core.Models.Contacts;
using Platform.Core.Services;
using Platform.Core.Utilities;
using System.Threading.Tasks;
using System.Web;

namespace Platform.API.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactsController : ApiController
    {
        private readonly IContactService _contactService;
        private readonly IExcelParser<ContactModel> _excelParser;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="excelParser"></param>
        /// <param name="contactService"></param>
        public ContactsController(IExcelParser<ContactModel> excelParser, IContactService contactService)
        {
            _excelParser = excelParser;
            _contactService = contactService;
        }

        /// <summary>
        /// Gets the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ContactListModel> Get(ContactsPagingModel page)
        {
            return new List<ContactListModel> {
                new ContactListModel { Name = "John Doe", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe1", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe2", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe3", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe4", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe5", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe6", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe7", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe8", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe9", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe10", Address = "Some street", ZipCode="0000" },
                new ContactListModel { Name = "John Doe11", Address = "Some street", ZipCode="0000" },
            }; //await _contactService.GetContacts(page);
        }

        /// <summary>
        /// Saves the contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveContact")]
        public async Task<IHttpActionResult> SaveContact(ContactModel contact)
        {
            if (!ModelState.IsValid)
            {
                //Todo: Make base api controller with method to handle validation error
                return BadRequest("Invalid model");
            }

            var result = await _contactService.CreateContact(contact);

            if (!result)
            {
                return this.BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Saves the fast contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveFastContact")]
        public async Task<IHttpActionResult> SaveFastContact(FastContactModel contact)
        {
            if (!ModelState.IsValid)
            {
                //Todo: Make base api controller with method to handle validation error
                return BadRequest("Invalid model");
            }

            var result = await _contactService.CreateContact(contact);

            if (!result)
            {
                return this.BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Gets the contacts document.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetContactsDocument")]
        public HttpResponseMessage GetContactsDocument()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content = new StreamContent(_excelParser.ParseToStream(new List<ContactModel>()
            {
                new ContactModel()
                {
                    Name = "Test"
                }
            }));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("fileName") {FileName = "Contacts.xlsx"};
            return response;
        }

        /// <summary>
        /// Sets the contacts from document.
        /// </summary>
        /// <param name="postedFile">The posted file.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetContactsFromDocument")]
        public IHttpActionResult SetContactsFromDocument()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            IEnumerable<ContactModel> model = null;
            if (file != null && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                model = _excelParser.ParseFromStream(file.InputStream);
                //TODO save to db
            }
            return Ok();
        }
    }
}