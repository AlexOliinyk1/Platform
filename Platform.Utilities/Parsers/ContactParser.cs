using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using Platform.Core.Models.Contacts;
using Platform.Core.Utilities;

namespace Platform.Utilities.Parsers
{
    public class ContactParser:IExcelParser<ContactModel>
    {
        public IEnumerable<ContactModel> ParseFromStream(Stream stream)
        {
            List<ContactModel> spreedsheet = new List<ContactModel>();
            using (var package = new ExcelPackage(stream))
            {
                var currentSheet = package.Workbook.Worksheets;
                var workSheet = currentSheet[1];
                var noOfRow = workSheet.Dimension.End.Row;
                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    if (workSheet.Cells[rowIterator, 1].Value == null || workSheet.Cells[rowIterator, 1].Value.ToString() == "") break;
                    var contact = new ContactModel();
                    contact.Name = workSheet.Cells[rowIterator, 1].Value?.ToString();
                    contact.Email = workSheet.Cells[rowIterator, 2].Value?.ToString();
                    contact.PhoneNumber = workSheet.Cells[rowIterator, 3].Value?.ToString();
                    contact.ContactType = workSheet.Cells[rowIterator, 4].Value?.ToString();
                    contact.IsCompany = SimpleTypes.parseBool(workSheet.Cells[rowIterator, 5].Value?.ToString());
                    contact.Title = workSheet.Cells[rowIterator, 6].Value?.ToString();
                    contact.CustomerType = workSheet.Cells[rowIterator, 7].Value?.ToString();
                    contact.Zip = workSheet.Cells[rowIterator, 8].Value?.ToString();
                    contact.Street = workSheet.Cells[rowIterator, 9].Value?.ToString();
                    contact.City = workSheet.Cells[rowIterator, 10].Value?.ToString();
                    contact.Country = workSheet.Cells[rowIterator, 11].Value?.ToString();
                    contact.VatNumber = workSheet.Cells[rowIterator, 12].Value?.ToString();
                    spreedsheet.Add(contact);
                }
            }
            return spreedsheet;
        }
        public MemoryStream ParseToStream(IList<ContactModel> model)
        {
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Contacts");
                var noOfRow = workSheet.Dimension.End.Row;
                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    workSheet.Cells[rowIterator, 1].Value = model[rowIterator - 2].Name;
                    workSheet.Cells[rowIterator, 2].Value = model[rowIterator - 2].Email;
                    workSheet.Cells[rowIterator, 3].Value = model[rowIterator - 2].PhoneNumber;
                    workSheet.Cells[rowIterator, 4].Value = model[rowIterator - 2].ContactType;
                    workSheet.Cells[rowIterator, 5].Value = model[rowIterator - 2].IsCompany;
                    workSheet.Cells[rowIterator, 6].Value = model[rowIterator - 2].Title;
                    workSheet.Cells[rowIterator, 7].Value = model[rowIterator - 2].CustomerType;
                    workSheet.Cells[rowIterator, 8].Value = model[rowIterator - 2].Zip;
                    workSheet.Cells[rowIterator, 9].Value = model[rowIterator - 2].Street;
                    workSheet.Cells[rowIterator, 10].Value = model[rowIterator - 2].City;
                    workSheet.Cells[rowIterator, 11].Value = model[rowIterator - 2].Country;
                    workSheet.Cells[rowIterator, 12].Value = model[rowIterator - 2].VatNumber;
                }
                package.Save();
                var stream = new MemoryStream(package.GetAsByteArray());
                return stream;
            }
        }
    }
}
