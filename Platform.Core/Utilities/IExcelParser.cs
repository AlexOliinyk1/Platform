using System.Collections.Generic;
using System.IO;

namespace Platform.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IExcelParser<T>
    {
        IEnumerable<T> ParseFromStream(Stream stream);
        MemoryStream ParseToStream(IList<T> model);
    }
}
