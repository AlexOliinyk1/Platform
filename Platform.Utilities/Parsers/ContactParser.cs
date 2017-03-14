using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using Platform.Core.Utilities;

namespace Platform.Utilities.Parsers
{
    public class ContactParser:IExcelParser<object>
    {
        public IEnumerable<object> ParseFromStream(Stream stream)
        {
            List<object> spreedsheet = new List<object>();
            using (var package = new ExcelPackage(stream))
            {
                var currentSheet = package.Workbook.Worksheets;
                var workSheet = currentSheet[1];
                var noOfRow = workSheet.Dimension.End.Row;
                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    if (workSheet.Cells[rowIterator, 1].Value == null || workSheet.Cells[rowIterator, 1].Value.ToString() == "") break;
                    var contact = new object();
                    //contact.Name = workSheet.Cells[rowIterator, 1].Value?.ToString();
                    spreedsheet.Add(contact);
                }
            }
            return spreedsheet;
        }
    }
}
