using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Platform.Core.Common;
using Platform.Core.Models.Contacts;
using Platform.Core.Services;
using Platform.Core.Utilities;

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
        public async Task<IEnumerable<ContactListModel>> Get([FromUri]ContactsPagingModel page)
        {
            if(string.IsNullOrEmpty(page.ContactType))
            {
                page.ContactType = ContactTypes.ALL;
            }

            try
            {
                return await _contactService.GetContacts(page);
            }
            catch(Exception)
            {
                //  todo: handle error
                return new List<ContactListModel>();
            }
        }

        [HttpGet]
        [Route("GetAllContactModels")]
        public HttpResponseMessage GetAllContactModels()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _contactService.GetAllContactModels().Result);
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
            if(!ModelState.IsValid)
            {
                //Todo: Make base api controller with method to handle validation error
                return BadRequest("Invalid model");
            }

            try
            {
                var result = await _contactService.CreateContact(contact);
                if(!result)
                {
                    return this.BadRequest();
                }
            }
            catch(Exception)
            {
                return BadRequest("Save fail");
            }

            return Ok("success");
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
            if(!ModelState.IsValid)
            {
                //Todo: Make base api controller with method to handle validation error
                return BadRequest("Invalid model");
            }

            var result = await _contactService.CreateContact(contact);

            if(!result)
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
        public async Task<HttpResponseMessage> GetContactsDocument()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/octet-stream");

            List<ContactModel> contacts = await _contactService.GetAllContactModels();

            response.Content = new StreamContent(_excelParser.ParseToStream(contacts));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("fileName") { FileName = "Contacts.xlsx" };

            return response;
        }

        /// <summary>
        /// Sets the contacts from document.
        /// </summary>
        /// <param name="postedFile">The posted file.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetContactsFromDocument")]
        public async Task<IHttpActionResult> SetContactsFromDocument()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            IEnumerable<ContactModel> model = null;
            if(file != null && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                model = _excelParser.ParseFromStream(file.InputStream);

                foreach(var contactModel in model)
                {
                    await _contactService.CreateContact(contactModel);
                }
            }
            return Ok();
        }
    }
}