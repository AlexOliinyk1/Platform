using System.Collections.Generic;
using System.IO;

namespace Platform.Core.Utilities
{
    public interface IExcelParser<T>
    {
        IEnumerable<T> ParseFromStream(Stream stream);
        MemoryStream ParseToStream(IList<T> model);
    }
}
