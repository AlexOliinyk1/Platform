using System;

namespace Platform.Utilities.Parsers
{
    public static class SimpleTypes
    {
        public static DateTime? ParseDateTime(string value)
        {
            long buffer;
            DateTime? date = null;
            if (value != null)
            {
                long.TryParse(value, out buffer);
                if (buffer != 0)
                {
                    date = DateTime.FromOADate(buffer);
                }
            }
            return date;
        }

        public static int parseInt(string value)
        {
            int num;
            Int32.TryParse(value, out num);
            return num;
        }

        public static string ParsePhone(string value)
        {
            int buffer;
            int.TryParse(value, out buffer);
            return buffer != 0 ? string.Format("{0:(###) ###-####}", buffer) : "";
        }

        public static bool parseBool(string value)
        {
            bool buffer;
            bool.TryParse(value, out buffer);
            return buffer;
        }
    }
}
