using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using Platform.Core.Models.Contacts;
using Platform.Core.Utilities;
using Platform.Core.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Platform.API.Controllers
{
    [RoutePrefix("Contacts")]
    public class ContactsController : ApiController
    {
        private IContactService _contactService;
        private IExcelParser<ContactModel> _excelParser;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="excelParser"></param>
        public ContactsController(IExcelParser<ContactModel> excelParser, IContactService contactService)
        {
            _excelParser = excelParser;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactListModel>> Get(ContactsPagingModel page)
        {
            return await _contactService.GetContacts(page);
        }

        [HttpPost]
        [Route("SaveContact")]
        public async Task<IHttpActionResult> SaveContact(ContactModel contact)
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
        
        [HttpGet]
        [Route("GetContactsDocument")]
        public HttpResponseMessage GetContactsDocument()
        {
            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK);
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content = new StreamContent(_excelParser.ParseToStream(new List<ContactModel>()
            {
                new ContactModel()
                {
                    Name = "Test"
                }
            }));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("fileName");
            response.Content.Headers.ContentDisposition.FileName = "Contacts.xlsx";
            return response;
        }
    }
}