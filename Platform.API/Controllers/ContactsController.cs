using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using Platform.Core.Models.Contacts;
using Platform.Core.Utilities;

namespace Platform.API.Controllers
{
    [RoutePrefix("Contacts")]
    public class ContactsController : ApiController
    {
        private IExcelParser<ContactModel> _excelParser;
        public ContactsController(IExcelParser<ContactModel> excelParser)
        {
            _excelParser = excelParser;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
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