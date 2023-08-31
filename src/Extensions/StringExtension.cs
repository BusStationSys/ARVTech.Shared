namespace ARVTech.Shared.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class StringExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TreatStringWithAccent(this string value)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;

            byte[] utfBytes = utf8.GetBytes(
                value);

            byte[] isoBytes = Encoding.Convert(
                utf8,
                iso,
                utfBytes);

            return iso.GetString(
                isoBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan? ToTimeSpan(this string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 5)
                return null;

            var numberOfElements = value.Split(':').Length;

            var timeSpan = default(TimeSpan?);

            if (numberOfElements == 3)
            {
                timeSpan = new TimeSpan(
                    int.Parse(value.Split(':')[0]),                 // Hours
                    int.Parse(value.Split(':')[1]),                 // Minutes
                    int.Parse(value.Split(':')[2].Split('.')[0]));  // Seconds
            }
            else
            {
                timeSpan = new TimeSpan(
                    int.Parse(value.Split(':')[0]),                 // Hours
                    int.Parse(value.Split(':')[1]),
                    0);                                             // Minutes
            }

            return timeSpan;
        }
    }
}