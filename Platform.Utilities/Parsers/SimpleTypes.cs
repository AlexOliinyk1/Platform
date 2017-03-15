using System;

namespace Platform.Utilities.Parsers
{
    public static class SimpleTypes
    {
        /// <summary>
        /// Parses the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime? ParseDateTime(string value)
        {
            DateTime? date = null;
            if (value != null)
            {
                long buffer;
                long.TryParse(value, out buffer);
                if (buffer != 0)
                {
                    date = DateTime.FromOADate(buffer);
                }
            }
            return date;
        }

        /// <summary>
        /// Parses the int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ParseInt(string value)
        {
            int num;
            Int32.TryParse(value, out num);
            return num;
        }

        /// <summary>
        /// Parses the phone.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ParsePhone(string value)
        {
            int buffer;
            int.TryParse(value, out buffer);
            return buffer != 0 ? $"{buffer:(###) ###-####}" : "";
        }

        /// <summary>
        /// Parses the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ParseBool(string value)
        {
            bool buffer;
            bool.TryParse(value, out buffer);
            return buffer;
        }
    }
}
